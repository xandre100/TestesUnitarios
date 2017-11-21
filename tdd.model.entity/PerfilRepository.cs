using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tdd.model.entity
{
    public class PerfilRepository : IPerfilRepository
    {
        private icatuDBEntities db = new icatuDBEntities();

        public PerfilRepository()
        {

        }

        public IQueryable<perfis> Getperfis()
        {
            return db.perfis;
        }

        public perfis Getperfis(int id)
        {
            perfis perfil = db.perfis.Find(id);
            if (perfil == null)
            {
                return null;
            }

            return perfil;
        }

        public void Putperfis(int id, perfis perfis)
        {   
            db.Entry(perfis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!perfisExists(id))
                {
                    throw ex;
                }
                else
                {
                    throw;
                }
            }
        }

        public void Postperfis(perfis perfis)
        {
            db.perfis.Add(perfis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (perfisExists(perfis.id_perfil))
                {
                    throw ex;
                }
                else
                {
                    throw;
                }
            }
        }

        public perfis Deleteperfis(int id)
        {
            perfis perfil = db.perfis.Find(id);

            if (perfil == null)
                throw null;

            db.perfis.Remove(perfil);
            db.SaveChanges();

            return perfil;
        }

        private bool perfisExists(int id)
        {
            return db.perfis.Count(e => e.id_perfil == id) > 0;
        }
    }
}
