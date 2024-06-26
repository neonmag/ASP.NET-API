﻿using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.Repositories.GameGroupRepository
{
    public class GamePostsRepository
    {
        private readonly DataContext _context;

        public GamePostsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GamePosts>> GetAllGamePosts()
        {
            return await _context.dbGamePosts
                .Where(g => g.deleteAt == null)
                .Select(g => new GamePosts {
                id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                gameId = g.gameId,
                authorId = g.authorId,
                content = g.content,
                contentUrl = g.contentUrl,
                createdAt = g.createdAt}).ToListAsync();
        }
        public async Task<GamePosts> UpdateGamePosts(GamePosts post)
        {
            var existing = await _context.dbGamePosts.FindAsync(post.id);
            if (existing != null)
            {
                existing.title = post.title;
                existing.description = post.description;
                existing.likesCount = post.likesCount;
                existing.gameId = post.gameId;
                existing.authorId = post.authorId;
                existing.content = post.content;
                existing.contentUrl = post.contentUrl;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GamePosts posts)
        {
            await _context.dbGamePosts.AddAsync(posts);
            _context.SaveChanges();
        }

        public async Task DeleteGamePosts(Guid id)
        {
            var requirement = await _context.dbGamePosts.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GamePosts?> GetById(Guid id)
        {
            var response = await _context.dbGamePosts
                .Where(x => x.id == id)
                .Select(g => new GamePosts
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
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

        public async Task<List<GamePosts?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGamePosts
                .Where(x => x.gameId == id)
                .Where(c => c.deleteAt == null)
                .Select(g => new GamePosts
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
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

        public async Task<List<GamePosts?>> GetByIds(List<Guid> id)
        {
            List<GamePosts> response = new List<GamePosts> ();

            foreach(var i in id)
            {
                var result = await _context.dbGamePosts
                .Where(x => x.id == i)
                .Where(c => c.deleteAt == null)
                .Select(g => new GamePosts
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
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