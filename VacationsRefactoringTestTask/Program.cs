using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationsRefactoringTestTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Input data
            var employeesNames = new List<string>()
            {
                "Иванов Иван Иванович",
                "Петров Петр Петрович",
                "Юлина Юлия Юлиановна",
                "Сидоров Сидор Сидорович",
                "Павлов Павел Павлович",
                "Георгиев Георг Георгиевич"
            };
            var year = DateTime.Today.Year;
            var vacationRules = new VacationRules();

            //Helper variables
            var gen = new Random();
            var firstDayOfYear = new DateTime(year, 1, 1);
            var daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            var vacationDurations = vacationRules.AvailableVacationDurationsInDays;
            var minVacationDuration = vacationDurations.Min();

            var vacationsByEmployees = employeesNames
                .ToDictionary(e => e, e => new List<DatedTimeSpan>());
            var allVacations = new List<DatedTimeSpan>();

            foreach (var employee in vacationsByEmployees.Keys)
            {
                var employeeVacations = vacationsByEmployees[employee];
                var vacationDaysLeft = vacationRules.VacationDaysPerYear;
                while (vacationDaysLeft > 0)
                {
                    var startDate = firstDayOfYear.AddDays(gen.Next(daysInYear - 1));

                    if (vacationRules.IsWorkingDay(startDate.DayOfWeek))
                    {
                        var vacationDuration = vacationDaysLeft <= minVacationDuration
                            ? minVacationDuration
                            : vacationDurations[gen.Next(vacationDurations.Count)];

                        var endDate = startDate.AddDays(vacationDuration);
                        var interval = new DatedTimeSpan(startDate, endDate);

                        if (vacationRules.CanCreateVacation(interval, employeeVacations, allVacations))
                        {
                            allVacations.Add(interval);
                            employeeVacations.Add(interval);
                            vacationDaysLeft -= vacationDuration;
                        }
                    }
                }
            }
            foreach (var employee in vacationsByEmployees.Keys)
            {
                Console.WriteLine($"Дни отпуска {employee} : ");
                foreach (var vac in vacationsByEmployees[employee]) 
                    Console.WriteLine($"{vac.Start:d} - {vac.End:d} ({vac.TimeSpan.Days})"); 
            }
            Console.ReadKey();
        }
    }
}