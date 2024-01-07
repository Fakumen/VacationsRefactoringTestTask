using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationsRefactoringTestTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var vacationsByEmployees = new Dictionary<string, List<DateInterval>>()
            {
                ["Иванов Иван Иванович"] = new(),
                ["Петров Петр Петрович"] = new(),
                ["Юлина Юлия Юлиановна"] = new(),
                ["Сидоров Сидор Сидорович"] = new(),
                ["Павлов Павел Павлович"] = new(),
                ["Георгиев Георг Георгиевич"] = new()
            };
            // Список отпусков сотрудников
            var allVacations = new List<DateInterval>();

            var gen = new Random();
            var year = DateTime.Today.Year;
            var firstDayOfYear = new DateTime(year, 1, 1);
            var daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            int[] vacationDurations = { 7, 14 }; //Duration in days
            var minVacationDuration = vacationDurations.Min();
            var vacationDaysPerYear = 28;

            foreach (var employee in vacationsByEmployees.Keys)
            {
                var employeeVacations = vacationsByEmployees[employee];
                var vacationDaysLeft = vacationDaysPerYear;
                while (vacationDaysLeft > 0)
                {
                    var startDate = firstDayOfYear.AddDays(gen.Next(daysInYear - 1));

                    if (IsWorkingDay(startDate.DayOfWeek))
                    {
                        var vacationDuration = vacationDaysLeft <= minVacationDuration
                            ? minVacationDuration
                            : vacationDurations[gen.Next(vacationDurations.Length)];

                        var endDate = startDate.AddDays(vacationDuration);
                        var interval = new DateInterval(startDate, endDate);

                        if (CanCreateVacation(interval, employeeVacations, allVacations))
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

        private static bool CanCreateVacation(
            DateInterval vacationInterval,
            List<DateInterval> employeeVacations,
            List<DateInterval> allEmployeesVacations)
        {
            var vacationMonthCooldown = 1;//vacation cooldown = 1 month//Replace with 30 days TimeSpan
            var vacationSafeExtent = 3;//days new TimeSpan(3, 0, 0, 0);

            var safeInterval = new DateInterval(
                vacationInterval.Start.AddDays(-vacationSafeExtent),
                vacationInterval.End.AddDays(vacationSafeExtent));
            if (allEmployeesVacations.Any(vac => vac.Intersects(safeInterval, true)))
                return false;
            var cooldownInterval = new DateInterval(
                vacationInterval.Start.AddMonths(-vacationMonthCooldown),
                vacationInterval.End.AddMonths(vacationMonthCooldown));
            //Проверка дня через месяц может перескочить через отпускной
            //Поэтому проверяется пересечение
            if (employeeVacations.Any(vac => vac.Intersects(cooldownInterval, true)))
                return false;
            return true;
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