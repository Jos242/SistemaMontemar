using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReservacion : IServiceReservacion
    {
        public Reservacion GetReservacionById(int id)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.GetReservacionById(id);
        }

        public IEnumerable<Reservacion> GetReservacions()
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.GetReservacions();
        }

        public bool RevisarFechas(DateTime start, DateTime end, int area)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.RevisarFechas(start, end, area);
        }

        public Reservacion Save(Reservacion reservacion)
        {
            IRepositoryReservacion repository = new RepositoryReservacion();
            return repository.Save(reservacion);
        }
    }
}
