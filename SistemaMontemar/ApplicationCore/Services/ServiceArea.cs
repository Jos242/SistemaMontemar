using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceArea : IServiceArea
    {
        public Area GetAreaById(int id)
        {
            IRepositoryArea repository = new RepositoryArea();
            return repository.GetAreaById(id);
        }

        public IEnumerable<Area> GetAreas()
        {
            IRepositoryArea repository = new RepositoryArea();
            return repository.GetAreas();
        }

    }
}
