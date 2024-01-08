using System.Collections.Generic;
using VacationsRefactoringTestTask.Infrastructure;

namespace VacationsRefactoringTestTask.Domain
{
    public interface IVacationsDistributor
    {
        /// <summary>
        /// Pre-checks if distribution according to rules even possible
        /// </summary>
        public bool CanDistributeVacations(
            IEnumerable<Employee> employees,
            IVacationRules vacationRules,
            int overYear);

        /// <summary>
        /// Distributes vacations over employees according to vacationRules
        /// </summary>
        public IReadOnlyDictionary<Employee, List<DatedTimeSpan>> DistributeVacations(
            IEnumerable<Employee> employees,
            IVacationRules vacationRules,
            int overYear);
    }
}
