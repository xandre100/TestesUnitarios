using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tdd.model
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
