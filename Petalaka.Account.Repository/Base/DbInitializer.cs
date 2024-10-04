using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.DataSeeding;

namespace Petalaka.Account.Repository.Base
{
    public class DbInitializer
    {
        private readonly PetalakaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DbInitializer(PetalakaDbContext dbContext, 
            ILogger<DbInitializer> logger,
            IUnitOfWork unitOfWork
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task InitializeAsync()
        {
            try
            {
                await _dbContext.Database.MigrateAsync();
                /*
                await SeedDataAsync();
            */
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in initialize database ", ex);
                throw;
            }    
        }

        public async Task SeedDataAsync()
        {
            try
            {
                await _dbContext.Database.EnsureCreatedAsync();
                if (!_unitOfWork.ApplicationRoleRepository.AsQueryable().Any())
                {
                    await _unitOfWork.ApplicationRoleRepository.InsertRangeAsync(RoleDataSeeding.DefaultRoles);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error in seed data: " + e.Message);
                throw;
            }
        }
    }
}
