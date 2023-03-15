using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRubro : IServiceRubro
    {
        public Rubro GetRubroById(int id)
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.GetRubroById(id);
        }

        public IEnumerable<Rubro> GetRubros()
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.GetRubros();
        }

        public Rubro Save(Rubro rubro)
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.Save(rubro);
        }
    }
}
