using CafeService.Api.Context;
using CafeService.Api.Entities;
using CafeService.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace CafeService.Api.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetAllEmployees (string search);
        Task<Employee> GetEmployeesById(string id);
        Task<List<Employee>> GetEmployeesByCafeId(Guid id);
        Task<string?> Insert(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(Employee employee);
    }
    public class EmployeeServiceImplement : IEmployeeService
    {
        private readonly AppDbContext _dbContext;

        public EmployeeServiceImplement(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EmployeeViewModel>> GetAllEmployees(string search)
        {
            return _dbContext.Employees.Include(e => e.Cafe).AsEnumerable().Select(d => new EmployeeViewModel
            {
                Id = d.Id,
                Name = d.Name,
                EmailAddress = d.EmailAddress,
                PhoneNumber = d.PhoneNumber,
                DaysWorked = d.StartDate.HasValue ? (int)Math.Ceiling((DateTime.UtcNow - d.StartDate.Value).TotalDays): 0,
                Cafe = d.Cafe == null ? "" : d.Cafe.Name,
            }).Where(d => string.IsNullOrEmpty(search) || d.Cafe.ToLower().Contains(search.ToLower())).OrderByDescending(c => c.DaysWorked).ToList();          
        }

        public async Task<Employee> GetEmployeesById(string id)
        {
            return await _dbContext.Employees.SingleOrDefaultAsync(d => d.Id == id); 
        }

        public async Task<List<Employee>> GetEmployeesByCafeId(Guid id)
        {
            return await _dbContext.Employees.Where(d => d.FK_CafeId == id).ToListAsync();
        }

        public async Task<string?> Insert(Employee employee)
        {
            try
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return employee.Id;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<bool> Update(Employee employee)
        {
            try
            {
                _dbContext.Update(employee);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Employee employee)
        {
            try
            {
                _dbContext.Remove(employee);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Error");
            }
        }

        public bool DeleteList(List<Employee> employeeList)
        {
            try
            {
                _dbContext.RemoveRange(employeeList);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Error");
            }
        }
    }
}
