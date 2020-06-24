using System.Collections.Generic;

namespace GpdFrontEnd.Infraestructure
{
    public class ResultSet<TValue>
    {
        public IList<TValue> Data {get; set;}

        public int Count {get; set;}
    }
}