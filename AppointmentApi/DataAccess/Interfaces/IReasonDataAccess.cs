using AppointmentModel.Model;
using System.Collections.Generic;

namespace AppointmentApi.DataAccess
{
    public interface IReasonDataAccess
    {
        public IEnumerable<Reason> GetReasons();
        public Reason UpdateReason(Reason reason);
        public Reason GetReason(int reasonId);
    }
}