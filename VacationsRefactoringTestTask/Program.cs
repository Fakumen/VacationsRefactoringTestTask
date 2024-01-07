using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
    class Program
    {
        static void Main(string[] args)
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
            var workingDaysOfWeek 
                = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
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

                    if (workingDaysOfWeek.Contains(startDate.DayOfWeek.ToString()))
                    {
                        int[] vacationSteps = { 7, 14 };
                        var minVacationLength = vacationSteps.Min();
                        var vacationLength = vacationCount <= minVacationLength
                            ? minVacationLength
                            : vacationSteps[gen.Next(vacationSteps.Length)];

                        var endDate = startDate.AddDays(vacationLength);

                        // Проверка условий по отпуску
                        var canCreateVacation = false;
                        var existStart = false;
                        var existEnd = false;
                        if (!vacations.Any(element => element >= startDate && element <= endDate))
                        {
                            if (!vacations.Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate))
                            {
                                existStart = dateList.Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
                                existEnd = dateList.Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
                                if (!existStart || !existEnd)
                                    canCreateVacation = true;
                            }
                        }

                        if (canCreateVacation)
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
    }
}