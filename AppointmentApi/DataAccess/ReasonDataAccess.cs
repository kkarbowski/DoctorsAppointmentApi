using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Database;
using AppointmentModel.Model;

namespace AppointmentApi.DataAccess
{
    public class ReasonDataAccess : IReasonDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public ReasonDataAccess(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Reason UpdateReason(Reason reason)
        {
            var newReason = _appDbContext.Reasons.Update(reason);
            _appDbContext.SaveChanges();

            return newReason.Entity;
        }

        public IEnumerable<Reason> GetReasons()
        {
            return _appDbContext.Reasons.ToList();
        }

        public Reason GetReason(int reasonId)
        {
            return _appDbContext.Reasons.Find(reasonId);
        }
    }
}
