using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp
{
    public readonly struct WorkSchedule
    {
        public int Start { get; }
        public int End { get; }

        public WorkSchedule(int start, int end)
        {
            Start = start;
            End = end;
        }

        // Обчислювані властивості згідно зі специфікацією
        public int HoursPerDay => End - Start;

        public string Display => $"{Start:D2}:00-{End:D2}:00";

        public bool IsNow => Contains(DateTime.Now.Hour);

        // Метод перевірки входження години в графік
        public bool Contains(int hour) => hour >= Start && hour < End;

        // Перевизначений ToString()
        public override string ToString() => $"{Display} ({HoursPerDay} год)";
    }
}
