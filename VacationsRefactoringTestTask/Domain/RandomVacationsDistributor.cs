using System;
using System.Collections.Generic;
using System.Linq;
using VacationsRefactoringTestTask.Infrastructure;

namespace VacationsRefactoringTestTask.Domain
{
    public class RandomVacationsDistributor : IVacationsDistributor
    {
        public bool CanDistributeVacations(
            IEnumerable<Employee> employees,
            IVacationRules vacationRules,
            int overYear)
        {
            //TODO: implement more complex check that considers rule intervals
            return employees.Count() * vacationRules.VacationDaysPerYear <= GetDaysInYear(overYear);
        }

        public IReadOnlyDictionary<Employee, List<DatedTimeSpan>> DistributeVacations(
            IEnumerable<Employee> employees,
            IVacationRules vacationRules,
            int overYear)
        {
            if (!CanDistributeVacations(employees, vacationRules, overYear))
                throw new ArgumentException("Distribution is not possible");

            var gen = new Random();
            var firstDayOfYear = new DateTime(overYear, 1, 1);
            var daysInYear = GetDaysInYear(overYear);
            var vacationDurations = vacationRules.AvailableVacationDurationsInDays;
            var minVacationDuration = vacationDurations.Min();

            var vacationsByEmployees = employees
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
            return vacationsByEmployees;
        }

        private static int GetDaysInYear(int year)
            => DateTime.IsLeapYear(year) ? 366 : 365;
    }
}
