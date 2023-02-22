using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ServiceResidencia : IServiceResidencia
    {
        public Residencia GetResidenciaById(int id)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaById(id);
        }

        public IEnumerable<Residencia> GetResidencias()
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidencias();
        }

        public Residencia Save(Residencia residencia)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.Save(residencia);
        }
    }
}
