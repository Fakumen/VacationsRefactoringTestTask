using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var vacationDictionary = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new(),
                ["Петров Петр Петрович"] = new(),
                ["Юлина Юлия Юлиановна"] = new(),
                ["Сидоров Сидор Сидорович"] = new(),
                ["Павлов Павел Павлович"] = new(),
                ["Георгиев Георг Георгиевич"] = new()
            };
            // Список отпусков сотрудников
            var vacations = new List<DateTime>();
            foreach (var VacationList in vacationDictionary)
            {
                var gen = new Random();
                var start = new DateTime(DateTime.Now.Year, 1, 1);
                var end = new DateTime(DateTime.Today.Year, 12, 31);
                var dateList = VacationList.Value;
                var vacationCount = 28;
                while (vacationCount > 0)
                {
                    int range = (end - start).Days;
                    var startDate = start.AddDays(gen.Next(range));

                    if (IsWorkingDay(startDate.DayOfWeek))
                    {
                        int[] vacationSteps = { 7, 14 };
                        var minVacationLength = vacationSteps.Min();
                        var vacationLength = vacationCount <= minVacationLength
                            ? minVacationLength
                            : vacationSteps[gen.Next(vacationSteps.Length)];

                        var endDate = startDate.AddDays(vacationLength);

                        if (CanCreateVacation(startDate, endDate, dateList, vacations))
                        {
                            for (var dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                vacations.Add(dt);
                                dateList.Add(dt);
                            }
                            vacationCount -= vacationLength;
                        }
                    }
                }
            }
            foreach (var vacationList in vacationDictionary)
            {
                var setDateList = vacationList.Value;
                Console.WriteLine("Дни отпуска " + vacationList.Key + " : ");
                for (int i = 0; i < setDateList.Count; i++) { Console.WriteLine(setDateList[i]); }
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