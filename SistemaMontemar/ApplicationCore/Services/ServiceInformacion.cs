using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceInformacion : IServiceInformacion
    {
        public Informacion GetInformacionById(int id)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacionById(id);
        }

        public IEnumerable<Informacion> GetInformacions()
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.GetInformacions();
        }

        public Informacion Save(Informacion residencia)
        {
            IRepositoryInformacion repository = new RepositoryInformacion();
            return repository.Save(residencia);
        }
    }
}
