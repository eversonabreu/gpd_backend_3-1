using GPD.Backend.Domain.Entities;
using GPD.Backend.Domain.Repositories;
using GPD.Backend.Infrastructure.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace GPD.Backend.Infrastructure.Database.Repositories
{
    public sealed class ProjetoEstruturaOrganizacionalRepository : BaseRepository<ProjetoEstruturaOrganizacional>, IProjetoEstruturaOrganizacionalRepository
    {
        public ProjetoEstruturaOrganizacionalRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        private void CorrigirOrdens(long idSuperior)
        {
            var itens = Filter(item => item.IdSuperior == idSuperior).OrderBy(it => it.Ordem).ToList();
            var strBuilder = new StringBuilder();
            short ordem = 0;
            foreach (var it in itens)
            {
                ordem++;
                if (ordem != it.Ordem)
                {
                    strBuilder.AppendLine($"update ProjetoEstruturaOrganizacional set Ordem = {ordem} where Id = {it.Id};");
                }
            }

            if (strBuilder.Length > 0)
            {
                Exception excSql = null;
                using var connection = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
                connection.Open();
                var transaction = connection.BeginTransaction();
                using (var command = new Microsoft.Data.SqlClient.SqlCommand(strBuilder.ToString(), connection, transaction))
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
        }

        public void RetrocederNivel(long id, long idSuperior)
        {
            CorrigirOrdens(idSuperior);
            var itens = Filter(item => item.IdSuperior == idSuperior).OrderBy(it => it.Ordem).ToList();
            if (itens.First().Id == id)
            {
                throw new Exception("Item não pode retroceder.");
            }
            short ordem = itens.First(it => it.Id == id).Ordem;
            long idBeforeElement = itens.First(it => it.Ordem == ordem - 1).Id;

            Exception excSql = null;
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            string sql = $"update ProjetoEstruturaOrganizacional set Ordem = {ordem - 1} where Id = {id}; update ProjetoEstruturaOrganizacional set Ordem = {ordem} where Id = {idBeforeElement};";
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(sql, connection, transaction))
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

        public void AvancarNivel(long id, long idSuperior)
        {
            CorrigirOrdens(idSuperior);
            var itens = Filter(item => item.IdSuperior == idSuperior).OrderBy(it => it.Ordem).ToList();
            if (itens.Last().Id == id)
            {
                throw new Exception("Item não pode avançar.");
            }
            short ordem = itens.First(it => it.Id == id).Ordem;
            long idAfterElement = itens.First(it => it.Ordem == ordem + 1).Id;

            Exception excSql = null;
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(databaseContext.Database.GetDbConnection().ConnectionString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            string sql = $"update ProjetoEstruturaOrganizacional set Ordem = {ordem + 1} where Id = {id}; update ProjetoEstruturaOrganizacional set Ordem = {ordem} where Id = {idAfterElement};";
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(sql, connection, transaction))
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

        protected override void AfterDelete(ProjetoEstruturaOrganizacional entity)
        {
            CorrigirOrdens(entity.IdSuperior.Value);
        }
    }
}
