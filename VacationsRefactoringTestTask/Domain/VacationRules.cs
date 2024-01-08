using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationsRefactoringTestTask
{
    public class VacationRules : IVacationRules
    {
        private HashSet<DayOfWeek> _workingDaysOfWeek = new()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public int VacationDaysPerYear { get; } = 28;

        public IReadOnlyList<int> AvailableVacationDurationsInDays { get; } 
            = new List<int>() { 7, 14 };

        public bool CanCreateVacation(
            DatedTimeSpan vacationInterval, 
            IEnumerable<DatedTimeSpan> employeeVacations, 
            IEnumerable<DatedTimeSpan> allEmployeesVacations)
        {
            var vacationMonthCooldown = 1;//vacation cooldown = 1 month//Replace with 30 days TimeSpan
            var vacationSafeExtent = 3;//days new TimeSpan(3, 0, 0, 0);

            var safeInterval = new DatedTimeSpan(
                vacationInterval.Start.AddDays(-vacationSafeExtent),
                vacationInterval.End.AddDays(vacationSafeExtent));
            if (allEmployeesVacations.Any(vac => vac.Intersects(safeInterval, true)))
                return false;
            var cooldownInterval = new DatedTimeSpan(
                vacationInterval.Start.AddMonths(-vacationMonthCooldown),
                vacationInterval.End.AddMonths(vacationMonthCooldown));
            //Проверка дня через месяц может перескочить через отпускной
            //Поэтому проверяется пересечение
            if (employeeVacations.Any(vac => vac.Intersects(cooldownInterval, true)))
                return false;
            return true;
        }

        public bool IsWorkingDay(DayOfWeek dayOfWeek)
            => _workingDaysOfWeek.Contains(dayOfWeek);
    }
}
