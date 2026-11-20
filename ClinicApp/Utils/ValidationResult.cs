using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Utils
{
    public class ValidationResult
    {
        public bool IsValid { get; set; } = true;

        public IReadOnlyList<string> Errors { get; } = new List<string>();

        public void AddError(string error)
        {
            IsValid = false;
            ((List<string>)Errors).Add(error);
        }
        public void Print()
        {
            foreach (var error in Errors)
            {
                Console.WriteLine($"Error: {error}[!]");
            }
        }

    }
}
