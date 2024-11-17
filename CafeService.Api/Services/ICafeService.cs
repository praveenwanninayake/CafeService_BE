using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CafeService.Api.Context;
using CafeService.Api.Entities;
using CafeService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeService.Api.Services
{
    public interface ICafeService
    {
        Task<List<CafeViewModel>> GetAllCafes(string search);
        Task<Cafe> GetCafeById(Guid id);
        Task<List<Cafe>> GetCafeByName(string name);
        Task<Guid?> Insert(Cafe cafe);
        Task<bool> Update(Cafe cafe);
        Task<bool> Delete(Cafe cafe);
        Task<List<DDListModel>> DDListGetCafe();
    }
    public class CafeServiceImplement : ICafeService
    {
        private readonly AppDbContext _dbContext;

        public CafeServiceImplement(AppDbContext dbContext)
        {
            _dbContext = dbContext;          
        }
        public async Task<List<CafeViewModel>> GetAllCafes(string search)
        {

            IQueryable<CafeViewModel> query = (from d in _dbContext.Cafes.Where(d => string.IsNullOrEmpty(search) || d.Location.ToLower().Contains(search.ToLower()))
                                               join d1 in _dbContext.Employees on d.Id equals d1.FK_CafeId into employeeGroup
                                               select new CafeViewModel
                                               {
                                                   Id = d.Id,
                                                   Name = d.Name,
                                                   Description = d.Description,
                                                   Location = d.Location,
                                                   Logo = d.Logo,
                                                   EmployeeCount = employeeGroup.Count()

                                               });

            return await query.OrderByDescending(c => c.EmployeeCount).ToListAsync();

        }

        public async Task<Cafe> GetCafeById(Guid id)
        {
            return await _dbContext.Cafes.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Cafe>> GetCafeByName(string name)
        {
            return await _dbContext.Cafes.Where(d => d.Name == name).ToListAsync();
        }

        public async Task<Guid?> Insert(Cafe cafe)
        {
            try
            {
                _dbContext.Cafes.Add(cafe);
                _dbContext.SaveChanges();
                return cafe.Id;
            }
            catch (Exception ex) {
               return null;      
            }
        }

        public async Task<bool> Update(Cafe cafe)
        {
            try
            {
                _dbContext.Update(cafe);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Cafe cafe)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var employeeListObject = _dbContext.Employees.Where(d => d.FK_CafeId == cafe.Id);
                    _dbContext.RemoveRange(employeeListObject);
                    await _dbContext.SaveChangesAsync();

                    _dbContext.Remove(cafe);
                    await _dbContext.SaveChangesAsync();                   

                    transaction.Commit();
                    return true;

                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<DDListModel>> DDListGetCafe()
        {
            return (from d in _dbContext.Cafes
                    select new DDListModel()
                    {
                        key = d.Id.ToString(),
                        value = d.Name,
                    }).ToList();
        }
    }
}
