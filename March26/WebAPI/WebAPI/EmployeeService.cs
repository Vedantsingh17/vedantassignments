using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI
{
    public class EmployeeService : IEmployee
    {
        private readonly EmpContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(EmpContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ ADD EMPLOYEE
        public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                employee.ImagePath = SaveImageToUploads(image);
            }
            else
            {
                employee.ImagePath = "/uploads/default.jpg";
            }

            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.ImagePath = GetBaseUrl() + employee.ImagePath;
            return employee;
        }

        // ✅ DELETE EMPLOYEE
        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null) return null;

            DeleteImageFile(employee.ImagePath);

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            employee.ImagePath = null;
            return employee;
        }

        // ✅ GET ALL (PAGINATION)
        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // ✅ GET BY ID
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees.FindAsync(id);
        }

        // ✅ UPDATE EMPLOYEE
        public async Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image)
        {
            var existing = await _context.employees.FindAsync(employee.Id);
            if (existing == null) return null;

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.Email = employee.Email;
            existing.Age = employee.Age;

            if (image != null && image.Length > 0)
            {
                DeleteImageFile(existing.ImagePath);
                existing.ImagePath = SaveImageToUploads(image);
            }

            await _context.SaveChangesAsync();

            return existing;
        }

        // ✅ SEARCH + PAGINATION + DTO
        public async Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(
            int pageNumber, int pageSize, string? searchTerm)
        {
            var query = _context.employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(searchTerm) ||
                    e.LastName.Contains(searchTerm) ||
                    e.Email.Contains(searchTerm));
            }

            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            string baseUrl = GetBaseUrl();

            return employees.Select(e => new EmployeeBasicDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                ImageUrl = string.IsNullOrEmpty(e.ImagePath)
                    ? baseUrl + "/uploads/default.jpg"
                    : baseUrl + e.ImagePath
            }).ToList();
        }

        // ✅ SAVE IMAGE
        private string SaveImageToUploads(IFormFile image)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fullPath = Path.Combine(uploadPath, imageName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(stream);

            return "/uploads/" + imageName;
        }

        // ✅ DELETE IMAGE
        private void DeleteImageFile(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;

            var fullPath = Path.Combine(
                _env.WebRootPath,
                imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        // ✅ BASE URL
        private string GetBaseUrl()
        {
            return "https://localhost:5001"; // change if needed
        }
    }
}