using AppointmentModel.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppointmentApi.Business
{
    public interface IReasonBusiness
    {
        Reason AddReason(Reason reason);
        Reason UpdateReason(Reason reason);
        IEnumerable<Reason> GetReasons();
        Reason GetReason(int reasonId);
    }
}