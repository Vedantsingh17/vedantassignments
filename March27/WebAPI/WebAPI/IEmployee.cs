using System.Threading.Tasks;
using WebAPI.Models;
namespace WebAPI
{
    public interface IEmployee
    {
        Task<Employee> AddEmployeeAsync(Employee employee, IFormFile? image);
        Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image);
        Task<Employee?> DeleteEmployeeAsync(int id);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);

        Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize, string? searchTerm);
    }
}