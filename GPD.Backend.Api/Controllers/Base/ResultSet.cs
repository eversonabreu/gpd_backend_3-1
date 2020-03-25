using GPD.Backend.Domain.Entities.Base;
using System.Collections.Generic;

namespace GPD.Backend.Api.Controllers.Base
{
    public class ResultSet<TListEntity> where TListEntity : IEnumerable<Entity>
    {
        public IEnumerable<Entity> Data { get; private set; }
        public int Count { get; private set; }

        public ResultSet(IEnumerable<Entity> data, int count)
        {
            Data = data;
            Count = count;
        }
    }
}
