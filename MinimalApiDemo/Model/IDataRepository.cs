namespace MinimalApiDemo.Model
{
    public interface IDataRepository
    {
        Employee AddEmployee(Employee employee);
        Employee? AlterEmployee(Employee employee);
        Employee? GetEmployeeById(string id);
        List<Employee> GetEmployees();
    }
}