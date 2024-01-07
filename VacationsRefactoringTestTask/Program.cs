using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            var VacationDictionary = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new(),
                ["Петров Петр Петрович"] = new(),
                ["Юлина Юлия Юлиановна"] = new(),
                ["Сидоров Сидор Сидорович"] = new(),
                ["Павлов Павел Павлович"] = new(),
                ["Георгиев Георг Георгиевич"] = new()
            };
            var AviableWorkingDaysOfWeekWithoutWeekends 
                = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            // Список отпусков сотрудников
            var Vacations = new List<DateTime>();
            var AllVacationCount = 0;
            var dateList = new List<DateTime>();
            var SetDateList = new List<DateTime>();
            foreach (var VacationList in VacationDictionary)
            {
                var gen = new Random();
                var step = new Random();
                var start = new DateTime(DateTime.Now.Year, 1, 1);
                var end = new DateTime(DateTime.Today.Year, 12, 31);
                string workerName;
                dateList = VacationList.Value;
                var vacationCount = 28;
                while (vacationCount > 0)
                {
                    int range = (end - start).Days;
                    var startDate = start.AddDays(gen.Next(range));

                    if (AviableWorkingDaysOfWeekWithoutWeekends.Contains(startDate.DayOfWeek.ToString()))
                    {
                        string[] vacationSteps = { "7", "14" };
                        var vacIndex = gen.Next(vacationSteps.Length);
                        var endDate = new DateTime(DateTime.Now.Year, 12, 31);
                        var difference = 0;
                        if (vacationSteps[vacIndex] == "7")
                        {
                            endDate = startDate.AddDays(7);
                            difference = 7;
                        }
                        if (vacationSteps[vacIndex] == "14")
                        {
                            endDate = startDate.AddDays(14);
                            difference = 14;
                        }

                        if (vacationCount <= 7)
                        {
                            endDate = startDate.AddDays(7);
                            difference = 7;
                        }

                        // Проверка условий по отпуску
                        var CanCreateVacation = false;
                        var existStart = false;
                        var existEnd = false;
                        if (!Vacations.Any(element => element >= startDate && element <= endDate))
                        {
                            if (!Vacations.Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate))
                            {
                                existStart = dateList.Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
                                existEnd = dateList.Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
                                if (!existStart || !existEnd)
                                    CanCreateVacation = true;
                            }
                        }

                        if (CanCreateVacation)
                        {
                            for (var dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                Vacations.Add(dt);
                                dateList.Add(dt);
                            }
                            AllVacationCount++;
                            vacationCount -= difference;
                        }
                    }
                }
            }
            foreach (var VacationList in VacationDictionary)
            {
                SetDateList = VacationList.Value;
                Console.WriteLine("Дни отпуска " + VacationList.Key + " : ");
                for (int i = 0; i < SetDateList.Count; i++) { Console.WriteLine(SetDateList[i]); }
            }
            Console.ReadKey();
        }
    }
}