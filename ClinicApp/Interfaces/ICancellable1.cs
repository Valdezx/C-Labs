using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Interfaces
{
    internal interface ICancellable1
    {
        public bool IsCancelled { get; }

        public string CancellationReason { get; }

        public bool Cancel(string reason = " ");
    }
}
