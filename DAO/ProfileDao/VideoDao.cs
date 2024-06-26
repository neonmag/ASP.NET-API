﻿using Slush.Data.Entity.Profile;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity;

namespace Slush.DAO.ProfileDao
{
    public class VideoDao
    {
        private readonly DataContext _context;

        public VideoDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Video>> GetAllVideos()
        {
            return await _context.dbVideos
                .Where(v => v.deleteAt == null)
                .Select(v => new Video {
                id = v.id,
                title = v.title,
                description = v.description,
                likesCount = v.likesCount,
                gameId = v.gameId,
                authorId = v.authorId,
                videoUrl = v.videoUrl,
                createdAt = v.createdAt}).ToListAsync();

        }
        public async Task UpdateVideo(Video video)
        {
            var existing = await _context.dbVideos.FindAsync(video.id);
            if (existing != null)
            {
                existing.title = video.title;
                existing.description = video.description;
                existing.likesCount = video.likesCount;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Video video)
        {
            await _context.dbVideos.AddAsync(video);
            _context.SaveChanges();
        }

        public async Task DeleteVideo(Guid id)
        {
            var requirement = await _context.dbVideos.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Video?> GetById(Guid id)
        {
            var response = await _context.dbVideos
                .Where(x => x.id == id)
                .Select(v => new Video
                {
                    id = v.id,
                    title = v.title,
                    description = v.description,
                    likesCount = v.likesCount,
                    gameId = v.gameId,
                    authorId = v.authorId,
                    videoUrl = v.videoUrl,
                    createdAt = v.createdAt
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
