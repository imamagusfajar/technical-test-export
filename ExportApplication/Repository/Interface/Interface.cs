using ExportApplication.Models;

namespace ExportApplication.Repository.Interface
{
    public interface Interface
    {
        public interface IEmployeeRepository
        {
            List<Employee> GetAllEmployees();
        }
    }
}
