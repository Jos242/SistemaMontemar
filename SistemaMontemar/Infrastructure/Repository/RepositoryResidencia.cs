using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryResidencia : IRepositoryResidencia
    {
        public Residencia GetResidenciaById(int id)
        {
            throw new NotImplementedException();
        }

        public Residencia GetResidenciaByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Residencia> GetResidencias()
        {
            throw new NotImplementedException();
        }
    }
}
