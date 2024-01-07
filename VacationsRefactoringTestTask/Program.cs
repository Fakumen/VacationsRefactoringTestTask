using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var vacationsByEmployees = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new(),
                ["Петров Петр Петрович"] = new(),
                ["Юлина Юлия Юлиановна"] = new(),
                ["Сидоров Сидор Сидорович"] = new(),
                ["Павлов Павел Павлович"] = new(),
                ["Георгиев Георг Георгиевич"] = new()
            };
            // Список отпусков сотрудников
            var allVacations = new List<DateTime>();

            var gen = new Random();
            var year = DateTime.Today.Year;
            var firstDayOfYear = new DateTime(year, 1, 1);
            var yearDaysRange = (DateTime.IsLeapYear(year) ? 366 : 365) - 1;
            int[] vacationDurations = { 7, 14 };
            var vacationCount = 28;

            foreach (var employee in vacationsByEmployees.Keys)
            {
                var employeeVacations = vacationsByEmployees[employee];
                while (vacationCount > 0)
                {
                    var startDate = firstDayOfYear.AddDays(gen.Next(yearDaysRange));

                    if (IsWorkingDay(startDate.DayOfWeek))
                    {
                        var minVacationLength = vacationDurations.Min();
                        var vacationLength = vacationCount <= minVacationLength
                            ? minVacationLength
                            : vacationDurations[gen.Next(vacationDurations.Length)];

                        var endDate = startDate.AddDays(vacationLength);

                        if (CanCreateVacation(startDate, endDate, employeeVacations, allVacations))
                        {
                            for (var dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                allVacations.Add(dt);
                                employeeVacations.Add(dt);
                            }
                            vacationCount -= vacationLength;
                        }
                    }
                }
            }
            foreach (var employee in vacationsByEmployees.Keys)
            {
                Console.WriteLine($"Дни отпуска {employee} : ");
                foreach (var date in vacationsByEmployees[employee]) 
                    Console.WriteLine($"{date:d}"); 
            }
            Console.ReadKey();
        }

        private static bool CanCreateVacation(
                DateTime startDate, DateTime endDate,
                List<DateTime> employeeVacationDays,
                List<DateTime> allEmployeesVacationDays)
        {
            var existStart = false;
            var existEnd = false;
            if (!allEmployeesVacationDays
                .Any(element => element >= startDate && element <= endDate))
            {
                if (!allEmployeesVacationDays
                    .Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate))
                {
                    existStart = employeeVacationDays
                        .Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
                    existEnd = employeeVacationDays
                        .Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
                    if (!existStart || !existEnd)
                        return true;
                }
            }
            return false;
        }

        private static bool IsWorkingDay(DayOfWeek dayOfWeek)
        {
            var workingDaysOfWeek = new HashSet<DayOfWeek>()
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
            return workingDaysOfWeek.Contains(dayOfWeek);
        }
    }
}