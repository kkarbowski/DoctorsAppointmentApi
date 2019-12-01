using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.DataAccess;
using AppointmentModel.Model;

namespace AppointmentApi.Business
{
    public class ReasonBusiness : IReasonBusiness
    {
        private readonly IReasonDataAccess _reasonDataAccess;

        public ReasonBusiness(IReasonDataAccess reasonDataAccess)
        {
            _reasonDataAccess = reasonDataAccess;
        }

        public Reason UpdateReason(Reason reason)
        {
            return _reasonDataAccess.UpdateReason(reason);
        }

        public IEnumerable<Reason> GetReasons()
        {
            return _reasonDataAccess.GetReasons();
        }

        public Reason GetReason(int reasonId)
        {
            return _reasonDataAccess.GetReason(reasonId);
        }

        public Reason AddReason(Reason reason)
        {
            return UpdateReason(reason);
        }
    }
}
