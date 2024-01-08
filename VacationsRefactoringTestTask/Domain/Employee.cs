namespace VacationsRefactoringTestTask.Domain
{
    public class Employee
    {
        public Employee(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string? Name { get; }

        public override bool Equals(object? obj)
            => Equals(obj as Employee);

        public bool Equals(Employee? employee)
        {
            if (employee is null) return false;
            if (ReferenceEquals(employee, this)) return true;
            if (GetType() != employee.GetType()) return false;
            return Id == employee.Id;
        }

        public override int GetHashCode()
            => Id.GetHashCode();

        public static bool operator ==(Employee employee1, Employee employee2)
        {
            if (employee1 is null && employee2 is null)
                return true;
            return employee1 is not null 
                ? employee1.Equals(employee2) 
                : employee2.Equals(employee1);
        }

        public static bool operator !=(Employee employee1, Employee employee2)
            => !(employee1 == employee2);
    }
}
