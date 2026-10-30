using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Interfaces
{
    public interface IPayable
    {
        bool IsPaid { get; }
        decimal GetCost();
        void MarkPaid();
    }
}