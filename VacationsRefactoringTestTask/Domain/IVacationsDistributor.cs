using System.Collections.Generic;

namespace VacationsRefactoringTestTask
{
    public interface IVacationsDistributor
    {
        /// <summary>
        /// Pre-checks if distribution according to rules even possible
        /// </summary>
        public bool CanDistributeVacations(
            IEnumerable<string> employees,
            IVacationRules vacationRules,
            int overYear);

        /// <summary>
        /// Distributes vacations over employees according to vacationRules
        /// </summary>
        public IReadOnlyDictionary<string, List<DatedTimeSpan>> DistributeVacations(
            IEnumerable<string> employees,
            IVacationRules vacationRules,
            int overYear);
    }
}
