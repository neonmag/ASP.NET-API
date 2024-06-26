﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class UserCategoryDao
    {
        private readonly DataContext _context;
        public UserCategoryDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserCategory>> GetAllUserCategories()
        {
            return await _context.dbUserCategories
                .Where(u => u.deletedAt == null)
                .Select(u => new UserCategory 
                {
                    id = u.id,
                    userId = u.userId,
                    ownedGameId = u.ownedGameId,
                    categoryId = u.categoryId,
                    createdAt = u.createdAt
                }).ToListAsync();
        }

        public async Task<List<UserCategory>> GetAllCategoriesByUser(Guid id)
        {
            return await _context.dbUserCategories
                .Where(x => x.userId == id)
                .Select(x => new UserCategory
                {
                    id = x.id,
                    userId = x.userId,
                    ownedGameId = x.ownedGameId,
                    categoryId = x.categoryId,
                    createdAt = x.createdAt
                }).ToListAsync();
        }

        public async Task UpdateUserCategory(UserCategory userCategory)
        {
            var existing = await _context.dbUserCategories.FindAsync(userCategory.id);

            if (existing != null)
            {
                existing.userId = userCategory.id;
                existing.ownedGameId = userCategory.ownedGameId;
                existing.categoryId = userCategory.categoryId;
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(UserCategory userCategory)
        {
            await _context.dbUserCategories.AddAsync(userCategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var existing = await _context.dbUserCategories.FindAsync(id);

            if (existing != null)
            {
                existing.deletedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserCategory?> GetById(Guid id)
        {
            var response = await _context.dbUserCategories
                .Where(c => c.id == id)
                .Select(c => new UserCategory
                {
                    id = c.id,
                    userId = c.id,
                    ownedGameId = c.ownedGameId,
                    categoryId = c.categoryId,
                    createdAt = c.createdAt
                }).FirstOrDefaultAsync();

            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
