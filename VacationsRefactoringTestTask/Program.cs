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
            var vacationRules = new VacationRules() as IVacationRules;

            var distributor = new RandomVacationsDistributor() as IVacationsDistributor;
            var vacationsByEmployees = distributor.DistributeVacations(
                employeesNames, vacationRules, year);

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