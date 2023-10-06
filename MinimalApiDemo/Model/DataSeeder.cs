namespace MinimalApiDemo.Model
{
    public class DataSeeder
    {
        private readonly EmployeeDbContext employeeDbContext;

        public DataSeeder(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        public void Seed()
        {
            if (!employeeDbContext.Employee.Any())
            {
                var employees = new List<Employee>()
                {
                    new Employee()
                    {
                        ID = "1",
                        Citizenship = "Brazil",
                        Name = "Guilherme"
                    },
                     new Employee()
                    {
                        ID = "2",
                        Citizenship = "Portugal",
                        Name = "João"
                    },
                      new Employee()
                    {
                        ID = "3",
                        Citizenship = "Brazil",
                        Name = "Marcelo"
                    },
                       new Employee()
                    {
                        ID = "4",
                        Citizenship = "Brazil",
                        Name = "Luiz"
                    },
                };

                employeeDbContext.Employee.AddRange(employees);
                employeeDbContext.SaveChanges();
            }
        }
    }
}
