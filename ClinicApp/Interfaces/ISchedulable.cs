using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Interfaces
{
    public interface ISchedulable
    {
        bool CanSchedule(DateTime at);
        DateTime[] GetAvailableSlots(DateTime date, int slotCount);
    }
}
