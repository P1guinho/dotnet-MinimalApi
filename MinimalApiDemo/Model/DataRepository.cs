using Microsoft.EntityFrameworkCore;

namespace MinimalApiDemo.Model
{
    public class DataRepository : IDataRepository
    {
        private readonly EmployeeDbContext _db;

        public DataRepository(EmployeeDbContext db)
        {
            _db = db;
        }

        public List<Employee> GetEmployees() => _db.Employee.AsNoTracking().ToList();

        public Employee? GetEmployeeById(string id) => _db.Employee.FirstOrDefault(x => x.ID == id);

        public Employee AddEmployee(Employee employee)
        {
            _db.Employee.Add(employee);
            _db.SaveChanges();

            return employee;
        }

        public Employee? AlterEmployee(Employee employee)
        {
            if (!string.IsNullOrWhiteSpace(employee.ID))
            {
                _db.Employee.Update(employee);
                _db.SaveChanges();

                return _db.Employee.FirstOrDefault(x => x.ID == employee.ID);
            }

            return null;
        }
    }
}
