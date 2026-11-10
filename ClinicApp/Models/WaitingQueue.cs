using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Models
{
     public class WaitingQueue
    {
        public readonly Queue<Patient> _queue = new Queue<Patient>();

        public int Count => _queue.Count;

        public bool isEmpty => _queue.Count == 0;

        public void Enqueue(Patient patient)
        {
            _queue.Enqueue(patient);
        }
        public Patient Dequeue()
        {
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Черга порожня.");
            }
            return _queue.Dequeue();
        }
        public Patient Peek()
        {
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Черга порожня.");
            }
            return _queue.Peek();
        }
        public Patient[] ToArray()
        {
            return _queue.ToArray();
        }
    }
}
