using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationsRefactoringTestTask
{
    public interface IVacationRules
    {
        public int VacationDaysPerYear { get; }

        public IReadOnlyList<int> AvailableVacationDurationsInDays { get; }

        public bool IsWorkingDay(DayOfWeek dayOfWeek);

        public bool CanCreateVacation(
            DatedTimeSpan vacationInterval,
            IEnumerable<DatedTimeSpan> employeeVacations,
            IEnumerable<DatedTimeSpan> allEmployeesVacations);
    }
}
