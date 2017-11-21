using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tdd.model.entity
{
    public interface IPerfilRepository
    {
        IQueryable<perfis> Getperfis();
        perfis Getperfis(int id);
        void Putperfis(int id, perfis perfis);
        void Postperfis(perfis perfis);
        perfis Deleteperfis(int id);
    }
}
