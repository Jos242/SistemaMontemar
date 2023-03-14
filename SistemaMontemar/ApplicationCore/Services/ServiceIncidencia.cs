using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceIncidencia : IServiceIncidencia
    {
        public Incidencia GetIncidenciaById(int id)
        {
            IRepositoryIncidencia repository = new RepositoryIncidencia();
            return repository.GetIncidenciaById(id);
        }

        public IEnumerable<Incidencia> GetIncidencias()
        {
            IRepositoryIncidencia repository = new RepositoryIncidencia();
            return repository.GetIncidencias();
        }

        public Incidencia Save(Incidencia incidencia)
        {
            IRepositoryIncidencia repository = new RepositoryIncidencia();
            return repository.Save(incidencia);
        }
    }
}
