using System;
using System.Collections.Generic;
using VacationsRefactoringTestTask.Domain;

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
            var employees = new List<Employee>();
            for (var i = 0; i < employeesNames.Count; i++)
                employees.Add(new Employee(i, employeesNames[i]));
            var year = DateTime.Today.Year;

            //Dependencies
            var vacationRules = new VacationRules() as IVacationRules;
            var distributor = new RandomVacationsDistributor() as IVacationsDistributor;

            //Distribution
            var vacationsByEmployees = distributor.DistributeVacations(
                employees, vacationRules, year);

            //Output
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