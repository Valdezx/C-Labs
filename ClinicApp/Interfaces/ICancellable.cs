using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Utils;


namespace ClinicApp.Interfaces
{
    public interface ICancellable
    {
        public bool IsCancelled { get; }

        public string CancellationReason { get; }

        public bool Cancel(string reason = " ");
    }
}
