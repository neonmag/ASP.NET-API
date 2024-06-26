﻿using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.Repositories.GameGroupRepository
{
    public class GameGuideRepository
    {
        private readonly DataContext _context;

        public GameGuideRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameGuide>> GetAllGameGuides()
        {
            return await _context.dbGameGuides
                .Where(g => g.deleteAt == null)
                .Select(g => new GameGuide{ id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                gameId = g.gameId,
                authorId = g.authorId,
                gameGroupId = g.gameGroupId,
                content = g.content, 
                contentUrl = g.contentUrl, 
                createdAt = g.createdAt }).ToListAsync();

        }

        public async Task<GameGuide> UpdateGameGuide(GameGuide guide)
        {
            var existing = await _context.dbGameGuides.FindAsync(guide.id);
            if (existing != null)
            {
                existing.title = guide.title;
                existing.description = guide.description;
                existing.likesCount = guide.likesCount;
                existing.gameId = guide.gameId;
                existing.authorId = guide.authorId;
                existing.gameGroupId = guide.gameGroupId;
                existing.content = guide.content;
                existing.contentUrl = guide.contentUrl;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameGuide guide)
        {
            await _context.dbGameGuides.AddAsync(guide);
            _context.SaveChanges();
        }

        public async Task DeleteGameGuide(Guid id)
        {
            var requirement = await _context.dbGameGuides.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameGuide?> GetById(Guid id)
        {
            var response = await _context.dbGameGuides
                .Where(x => x.id == id)
                .Select(g => new GameGuide
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    gameGroupId = g.gameGroupId,
                    content = g.content,
                    contentUrl = g.contentUrl,
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

        public async Task<List<GameGuide?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGameGuides
                .Where(x => x.gameId == id)
                .Where(c => c.deleteAt == null)
                .Select(g => new GameGuide
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    gameGroupId = g.gameGroupId,
                    content = g.content,
                    contentUrl = g.contentUrl,
                    createdAt = g.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GameGuide?>> GetByIds(List<Guid> id)
        {
            List<GameGuide> response = new List<GameGuide> ();

            foreach(var i in id)
            {
                var result = await _context.dbGameGuides
                .Where(x => x.id == i)
                .Where(c => c.deleteAt == null)
                .Select(g => new GameGuide
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    gameGroupId = g.gameGroupId,
                    content = g.content,
                    contentUrl = g.contentUrl,
                    createdAt = g.createdAt
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