using System;
using System.Collections.Generic;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class BillingManager
    {
        private readonly AppointmentManager _appointmentManager;

        public BillingManager(AppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager;
        }

        public IPayable[] GetAllUnpaid()
        {
            var all = _appointmentManager.GetAll();
            var list = new List<IPayable>();

            foreach (var app in all)
            {
                if (app is IPayable payable && !payable.IsPaid && !app.IsCancelled)
                {
                    list.Add(payable);
                }
            }
            return list.ToArray();
        }

        public IPayable[] GetUnpaidByPatient(int patientId)
        {
            var allUnpaid = GetAllUnpaid();
            var list = new List<IPayable>();

            foreach (var item in allUnpaid)
            {
                if (item is Appointment app && app.PatientId == patientId)
                {
                    list.Add(app);
                }
            }
            return list.ToArray();
        }

        public decimal GetTotalDebt()
        {
            var unpaid = GetAllUnpaid();
            decimal total = 0;
            foreach (var item in unpaid)
            {
                total += item.GetCost();
            }
            return total;
        }

        public decimal GetPatientDebt(int patientId)
        {
            var unpaid = GetUnpaidByPatient(patientId);
            decimal total = 0;
            foreach (var item in unpaid)
            {
                total += item.GetCost();
            }
            return total;
        }

        public bool PayAppointment(int appointmentId)
        {
            var appointment = _appointmentManager.FindById(appointmentId);
            if (appointment == null || appointment.IsCancelled)
            {
                return false;
            }

            if (appointment is IPayable payable)
            {
                if (payable.IsPaid) return false;
                payable.MarkPaid();
                return true;
            }

            return false;
        }

        // Виводить кожен елемент із сумою у форматі F2 та «грн»; порожній масив — повідомлення «Немає неоплачених записів.»
        public void DisplayUnpaid(IPayable[] items)
        {
            if (items == null || items.Length == 0)
            {
                Console.WriteLine("Немає неоплачених записів.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Запис: {item}, Сума: {item.GetCost():F2} грн");
            }
        }
    }
}