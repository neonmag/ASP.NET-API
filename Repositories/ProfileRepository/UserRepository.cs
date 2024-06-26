﻿using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity;

namespace Slush.Repositories.ProfileRepository
{
    public class UserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.dbUsers
                .Where(u => u.deleteAt == null)
                .Select(u => new User {
                id = u.id,
                name = u.name,
                passwordSalt = u.passwordSalt,
                email = u.email,
                description = u.description,
                image = u.image,
                verified = u.verified,
                amountOfMoney = u.amountOfMoney,
                amountOfXp = u.amountOfXp,
                createdAt = u.createdAt}).ToListAsync();
        }
        public async Task<User> UpdateUser(User user)
        {
            var existing = await _context.dbUsers.FindAsync(user.id);
            if (existing != null)
            {
                existing.name = user.name;
                existing.passwordSalt = user.passwordSalt;
                existing.email = user.email;
                existing.description = user.description;
                existing.image = user.image;
                existing.verified = user.verified;
                existing.amountOfMoney = user.amountOfMoney;
                existing.amountOfXp = user.amountOfXp;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(User user)
        {
            await _context.dbUsers.AddAsync(user);
            _context.SaveChanges();
        }

        public async Task DeleteUser(Guid id)
        {
            var requirement = await _context.dbUsers.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetById(Guid id)
        {
            var response = await _context.dbUsers
                .Where(x => x.id == id)
                .Select(u => new User
                {
                    id = u.id,
                    name = u.name,
                    passwordSalt = u.passwordSalt,
                    email = u.email,
                    description = u.description,
                    image = u.image,
                    verified = u.verified,
                    amountOfMoney = u.amountOfMoney,
                    amountOfXp = u.amountOfXp,
                    createdAt = u.createdAt
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
        public async Task<User?> GetByEmail(String name)
        {
            var response = await _context.dbUsers
                .Where(x => x.name == name)
                .Select(u => new User
                {
                    id = u.id,
                    name = u.name,
                    passwordSalt = u.passwordSalt,
                    email = u.email,
                    description = u.description,
                    image = u.image,
                    verified = u.verified,
                    amountOfMoney = u.amountOfMoney,
                    amountOfXp = u.amountOfXp,
                    createdAt = u.createdAt
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
        public async Task<User?> GetByUserId(Guid id)
        {
            var response = await _context.dbUsers
                .Where(x => x.id == id)
                .Select(u => new User
                {
                    id = u.id,
                    name = u.name,
                    image = u.image,
                    amountOfXp = u.amountOfXp,
                    createdAt = u.createdAt
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

        public async Task<List<User?>> GetByIds(List<Guid> ids)
        {
            List<User> response = new List<User>();

            foreach (var id in ids)
            {
                var result = await _context.dbUsers
                    .Where(x => x.id == id)
                    .Where(c => c.deleteAt == null)
                    .Select(u => new User
                    {
                        id = u.id,
                        name = u.name,
                        image = u.image,
                        amountOfXp = u.amountOfXp,
                        createdAt = u.createdAt
                    }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }

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
