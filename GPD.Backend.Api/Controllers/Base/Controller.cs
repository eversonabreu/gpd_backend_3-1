using GPD.Backend.Api.Identity;
using GPD.Backend.Domain.Entities.Base;
using GPD.Backend.Domain.Repositories.Base;
using GPD.Commom.Models.Base;
using GPD.Commom.UserInformations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GPD.Backend.Api.Controllers.Base
{
    public class Controller<TModel, TEntity> : ControllerBase where TEntity : Entity where TModel : Model
    {
        protected new User User { get; private set; } = null!;
        
        protected UserIdentity UserIdentity { get; private set; } = null!;

        private readonly IBaseRepository<TEntity> repository;

        public Controller(IBaseRepository<TEntity> repository, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            UserIdentity = new UserIdentity(httpContextAccessor.HttpContext, GetType().Name);
            User = UserIdentity.GetUser();
        }

        [HttpPost]
        public virtual long Post([FromBody] TModel model)
        {
            UserIdentity.VerifyPermission(UserIdentity.ActionPermission.Post, User);
            model.Validate();
            var entity = model.ToEntity<TEntity>();
            return repository.Add(entity, User.Id);
        }

        [HttpPut]
        public virtual void Put([FromBody] TModel model)
        {
            UserIdentity.VerifyPermission(UserIdentity.ActionPermission.Put, User);
            model.Validate(validateId: true);
            var entity = model.ToEntity<TEntity>(model.Id!.Value);
            repository.Update(entity, User.Id);
        }

        [HttpDelete]
        public virtual void Delete(long id)
        {
            UserIdentity.VerifyPermission(UserIdentity.ActionPermission.Delete, User);
            repository.DeleteById(id, User.Id);
        }

        [Route("itens/{ids}"), HttpDelete]
        public virtual void DeleteMany(string ids)
        {
            UserIdentity.VerifyPermission(UserIdentity.ActionPermission.Delete, User);
            string[] list = ids.Split(',');
            var longList = new List<long>();
            list.ToList().ForEach(item => longList.Add(long.Parse(item)));
            repository.DeleteMany(longList);
        }

        [Route("id/{id:long}"), HttpGet]
        public virtual TEntity GetById(long id) => repository.GetById(id);

        [HttpGet]
        public virtual ResultSet<IEnumerable<TEntity>> Get(string filter, string sort = null!, uint page = 0, uint limit = 10, bool loadDependencies = false)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new ResultSet<IEnumerable<TEntity>>(null!, 0);
            }

            var result = repository.FilterWithPagination(filter, sort, page, limit, loadDependencies);
            return new ResultSet<IEnumerable<TEntity>>(result.Data, result.Count); ;
        }
    }
}
