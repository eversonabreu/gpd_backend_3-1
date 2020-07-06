using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Exceptions;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class IndicadorRepository : BaseRepository<Indicador>, IIndicadorRepository
    {
        private readonly IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository;

        public IndicadorRepository(IServiceProvider serviceProvider,
            IProjetoEstruturaOrganizacionalRepository projetoEstruturaOrganizacionalRepository) : base(serviceProvider) 
        {
            this.projetoEstruturaOrganizacionalRepository = projetoEstruturaOrganizacionalRepository;
        }

        public IEnumerable<IndicadoresCorporativos> ObterIndicadoresCorporativos(long idProjeto)
        {
            var result = new List<IndicadoresCorporativos>();
            Exception excSql = null;
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
            connection.Open();

            string sql = @"select a.Id, a.Identificador, a.Nome, a.TipoCalculo, case when b.IdIndicador is null then 0 else 1 end Vinculado, B.Id ID2 from Indicador a 
                           left join (select IdIndicador, Id from ProjetoEstruturaOrganizacional where IdProjeto = @p1 and Tipo = 2) b on (a.Id = b.IdIndicador) 
                           where a.Corporativo = 1 order by a.Nome";
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(sql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("p1", idProjeto);
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new IndicadoresCorporativos
                        {
                            Id = reader.GetInt64(0),
                            Identificador = reader.GetString(1),
                            Nome = reader.GetString(2),
                            TipoCalculo = reader.GetInt32(3),
                            Vinculado = reader.GetInt32(4) == 1,
                            IdProjetoEstruturaOrganizacional = reader.IsDBNull(5) ? null : (long?) reader.GetInt64(5)
                        });
                    }
                }
                catch (Exception exc)
                {
                    excSql = exc;
                }
            }

            connection.Close();
            if (excSql != null) throw excSql;

            return result;
        }
		
		protected override void BeforeDelete(Indicador entity)
        {
            string termoPesquisa = $"[{entity.Identificador}]";
            if (FirstOrDefault(item => item.Formula.Contains(termoPesquisa), loadDependencies: false) != null)
            {
                throw new BusinessException("Não é possível excluir este indicador porque ele é usado como indicador base em outro(s) indicador(es).");
            }
        }

        protected override void BeforeUpdate(Indicador oldValue, Indicador newValue)
        {
            if (oldValue.TipoCalculo == TipoCalculo.NaoCalculado && newValue.TipoCalculo != TipoCalculo.NaoCalculado)
            {
                string termoPesquisa = $"[{newValue.Identificador}]";
                if (FirstOrDefault(item => item.TipoCalculo != TipoCalculo.NaoCalculado && item.Formula.Contains(termoPesquisa), loadDependencies: false) != null)
                {
                    throw new BusinessException("Não é possível alterar o tipo de cálculo deste indicador porque ele é usado como indicador base em outro(s) indicador(es).");
                }
            }
        }

        protected override void AfterUpdate(Indicador oldValue, Indicador newValue)
        {
			if (oldValue.Identificador != newValue.Identificador && oldValue.TipoCalculo == TipoCalculo.NaoCalculado)
			{
                Exception excSql = null;
				string termoPesquisa = $"[{oldValue.Identificador}]";
                string novoIdentificador = $"[{newValue.Identificador}]";
                using var connection = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
                connection.Open();
                var transaction = connection.BeginTransaction();
                string sqlUpdate = $"update Indicador set Formula = replace(Formula, '{termoPesquisa}', '{novoIdentificador}') where TipoCalculo <> 1 and Formula like '%{termoPesquisa}%'";
                using (var command = new Microsoft.Data.SqlClient.SqlCommand(sqlUpdate, connection, transaction))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception exc)
                    {
                        excSql = exc;
                        transaction.Rollback();
                    }
                }

                connection.Close();
                if (excSql != null) throw excSql;
            }
			
            if (oldValue.Corporativo && !newValue.Corporativo)
            {
                var lista = projetoEstruturaOrganizacionalRepository.Filter(item => item.Tipo == TipoProjetoEstruturaOrganizacional.Corporativo && item.IdIndicador == newValue.Id);
                var listaIds = new List<long>();
                lista?.ToList()?.ForEach(item => listaIds.Add(item.Id));
                projetoEstruturaOrganizacionalRepository.DeleteMany(listaIds);
            }
        }
    }


}
