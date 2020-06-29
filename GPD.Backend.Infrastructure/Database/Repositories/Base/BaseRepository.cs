using GPD.Backend.Domain.Attributes;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GPD.Backend.Domain.Entities;

namespace GPD.Backend.Infrastructure.Database.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity 
    {
        private readonly IServiceProvider serviceProvider;
        protected readonly DatabaseContext databaseContext;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(IServiceProvider serviceProvider)
        {
            databaseContext = serviceProvider.GetService(typeof(DatabaseContext)) as DatabaseContext;
            dbSet = databaseContext.Set<TEntity>();
            this.serviceProvider = serviceProvider;
        }

        private void LoadPropertiesEntities(object entity)
        {
            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<LoadEntityAttribute>();
                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.NameForeignKey) && attribute.TypeRepository != null)
                {
                    var foreignKey = entity.GetType().GetProperty(attribute.NameForeignKey);
                    if (foreignKey != null)
                    {
                        var valueObject = foreignKey.GetValue(entity);
                        if (valueObject != null)
                        {
                            long idLoad = (long)valueObject;

                            var repository = serviceProvider.GetService(attribute.TypeRepository);
                            var entityObject = repository.GetType().GetMethod(nameof(GetById)).Invoke(repository, new object[] { idLoad, false });

                            if (entityObject != null)
                            {
                                property.SetValue(entity, entityObject);
                            }
                        }
                    }
                }
            }
        }

        public TEntity GetById(long id, bool loadDependencies = true)
        {
            var entity = dbSet.FirstOrDefault($"Id == {id}");

            if (entity is null)
            {
                string nameEntity = typeof(TEntity).Name;
                throw new Exception($"Nenhum resultado encontrado para o id '{id}' na entidade '{nameEntity}'.");
            }

            if (loadDependencies)
            {
                LoadPropertiesEntities(entity);
            }

            return entity;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, TEntity defaultResult = null, bool loadDependencies = true)
        {
            var entity = dbSet.FirstOrDefault(expression);
            if (entity is null)
            {
                return defaultResult;
            }

            if (loadDependencies)
            {
                LoadPropertiesEntities(entity);
            }

            return entity;
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool loadDependencies = false)
        {
            var entities = dbSet.Where(expression)?.ToList();

            if (entities?.Any() ?? false && loadDependencies)
            {
                foreach (var entity in entities)
                {
                    LoadPropertiesEntities(entity);
                }
            }

            return entities;
        }

        public (IEnumerable<TEntity> Data, int Count) FilterWithPagination(string expression, string sort = null, uint page = 0, uint limit = 10, bool loadDependencies = false)
        {
            var data = dbSet.Where(expression);
            int count = data?.Count() ?? 0;

            if (!string.IsNullOrWhiteSpace(sort) && count > 0)
            {
                data = data.OrderBy($"{sort}");
            }

            if (limit > 0)
            {
                int skip = (int)(page * limit);
                data = data?.Skip(skip).Take((int)limit);
            }

            if (data != null)
            {
                var dataList = data.ToList();
                if (loadDependencies)
                {
                    foreach (var entity in dataList)
                    {
                        LoadPropertiesEntities(entity);
                    }
                }
            }

            return (data, count);
        }

        public long Add(TEntity entity, long? idUserAudit = null)
        {
            BeforeAdd(entity);
            dbSet.Add(entity);
            databaseContext.SaveChanges();

            if (idUserAudit.HasValue)
            {
                CreateAudit(entity.Id, TipoAuditoria.Insercao, idUserAudit.Value);
            }

            AfterAdd(entity);
            return entity.Id;
        }

        public void Update(TEntity entity, long? idUserAudit = null)
        {
            TEntity entityDb = GetById(entity.Id, false);
            BeforeUpdate(entityDb, entity);
            databaseContext.Entry(entityDb).State = EntityState.Modified;
            databaseContext.Entry(entityDb).CurrentValues.SetValues(entity);
            databaseContext.SaveChanges();

            if (idUserAudit.HasValue)
            {
                CreateAudit(entity.Id, TipoAuditoria.Alteracao, idUserAudit.Value, entityDb);
            }

            AfterUpdate(entityDb, entity);
        }

        public void DeleteById(long id, long? idUserAudit = null)
        {
            TEntity entity = GetById(id, false);
            BeforeDelete(entity);
            databaseContext.Entry(entity).State = EntityState.Deleted;
            dbSet.Remove(entity);
            databaseContext.SaveChanges();

            if (idUserAudit.HasValue)
            {
                CreateAudit(entity.Id, TipoAuditoria.Exclusao, idUserAudit.Value, entity);
            }

            AfterDelete(entity);
        }

        public void DeleteMany(IEnumerable<long> ids)
        {
            using var context = DatabaseContext.CreateContext(databaseContext.Database);
            using var transaction = context.Database.BeginTransaction();
            foreach (long id in ids)
            {
                var entity = GetById(id, false);
                context.Remove(entity);
                context.SaveChanges();
            }
            transaction.Commit();
        }
		
        protected virtual void BeforeAdd(TEntity entity) { }

        protected virtual void AfterAdd(TEntity entity) { }

        protected virtual void BeforeUpdate(TEntity oldValue, TEntity newValue) { }

        protected virtual void AfterUpdate(TEntity oldValue, TEntity newValue) { }

        protected virtual void BeforeDelete(TEntity entity) { }

        protected virtual void AfterDelete(TEntity entity) { }

        private void CreateAudit(long idRegistro, TipoAuditoria tipoAuditoria, long idUsuario, TEntity obj = null)
        {
            var databaseFacade = databaseContext.Database;
            string nomeTabela = typeof(TEntity).Name;
            Task.Run(async () => await CreateAuditAsync(databaseFacade, nomeTabela, idRegistro, tipoAuditoria, idUsuario, obj));
        }

        private async Task CreateAuditAsync(DatabaseFacade databaseFacade, string nomeTabela, long idRegistro, TipoAuditoria tipoAuditoria, long idUsuario, TEntity obj = null)
        {
            switch (tipoAuditoria)
            {
                case TipoAuditoria.Insercao:
                    {
                        using var context = DatabaseContext.CreateContext(databaseFacade);
                        context.Auditorias.Add(new Auditoria
                        {
                            NomeTabela = nomeTabela,
                            IdRegistro = idRegistro,
                            DataRegistro = DateTime.UtcNow,
                            Tipo = TipoAuditoria.Insercao,
                            IdUsuario = idUsuario
                        });
                        await context.SaveChangesAsync();
                        break;
                    }
                case TipoAuditoria.Exclusao:
                    {
                        string json = obj != null ? System.Text.Json.JsonSerializer.Serialize(obj) : null;
                        using var context = DatabaseContext.CreateContext(databaseFacade);
                        context.Auditorias.Add(new Auditoria
                        {
                            NomeTabela = nomeTabela,
                            IdRegistro = idRegistro,
                            DataRegistro = DateTime.UtcNow,
                            Tipo = TipoAuditoria.Exclusao,
                            IdUsuario = idUsuario,
                            Objeto = json
                        });
                        await context.SaveChangesAsync();
                        break;
                    }
                case TipoAuditoria.Alteracao:
                    {
                        const int limitAuditUpdate = 5;
                        string json = obj != null ? System.Text.Json.JsonSerializer.Serialize(obj) : null;
                        using var context = DatabaseContext.CreateContext(databaseFacade);
                        var auditorias = context.Auditorias.Where(item => item.NomeTabela == nomeTabela && item.IdRegistro == idRegistro && item.Tipo == TipoAuditoria.Alteracao)?.OrderBy(subItem => subItem.DataRegistro)?.ToList();
                        int count = auditorias?.Count ?? 0;
                        int index = 0;

                        while (count >= limitAuditUpdate)
                        {
                            var aud = auditorias[index];
                            context.Auditorias.Remove(aud);
                            index++;
                            count--;
                        }

                        context.Auditorias.Add(new Auditoria
                        {
                            NomeTabela = nomeTabela,
                            IdRegistro = idRegistro,
                            DataRegistro = DateTime.UtcNow,
                            Tipo = TipoAuditoria.Alteracao,
                            IdUsuario = idUsuario,
                            Objeto = json
                        });
                        await context.SaveChangesAsync();
                        break;
                    }
            }
        }
    }
}
