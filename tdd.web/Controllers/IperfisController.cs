using System.Linq;
using System.Web.Http;
using tdd.model.entity;

namespace tdd.web.Controllers
{
    public interface IperfisController
    {
        IHttpActionResult Deleteperfis(int id);
        IQueryable<perfis> Getperfis();
        IHttpActionResult Getperfis(int id);
        IHttpActionResult Postperfis(perfis perfis);
        IHttpActionResult Putperfis(int id, perfis perfis);
    }
}