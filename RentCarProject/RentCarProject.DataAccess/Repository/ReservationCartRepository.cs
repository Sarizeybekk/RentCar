﻿using RentCarProject.Data;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.DataAccess.Repository
{
    public class ReservationCartRepository : Repository<ReservationCart>, IReservationCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservationCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(ReservationCart nesne)
        {
            _db.Update(nesne);
        }
    }
}
