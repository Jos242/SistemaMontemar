﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryReservacion
    {
        IEnumerable<Reservacion> GetReservacions();
        Reservacion GetReservacionById(int id);
        Reservacion Save(Reservacion reservacion);
    }
}
