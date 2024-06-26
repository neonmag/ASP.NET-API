﻿using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Slush.Data.Entity.Community;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.DAO.GroupDao
{
    public class GroupDao
    {
        private readonly DataContext _context;

        public GroupDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.dbGroups
                .Where(g => g.deleteAt == null)
                .Select(g => new Group
            {
                id = g.id,
                attachedId = g.attachedId,
                name = g.name,
                description = g.description,
                createdAt = g.createdAt
            }).ToListAsync();

        }
        public async Task UpdateGroup(Group group)
        {
            var existing = await _context.dbGroups.FindAsync(group.id);
            if (existing != null)
            {
                existing.name = group.name;
                existing.description = group.description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Group group)
        {
            await _context.dbGroups.AddAsync(group);
            _context.SaveChanges();
        }

        public async Task DeleteGroup(Guid id)
        {
            var requirement = await _context.dbGroups.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Group?> GetById(Guid id)
        {
            var response = await _context.dbGroups
                .Where(x => x.id == id)
                .Select(g => new Group
                {
                    id = g.id,
                    attachedId = g.attachedId,
                    name = g.name,
                    description = g.description,
                    createdAt = g.createdAt
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
