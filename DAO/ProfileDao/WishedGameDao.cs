﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class WishedGameDao
    {
        private readonly DataContext _context;

        public WishedGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<WishedGame>> GetAllWishedGames()
        {
            return await _context.dbWishedGames
                .Where(s => s.deleteAt == null)
                .Select(s => new WishedGame {
                id = s.id,
                ownedGameId = s.ownedGameId,
                userId = s.userId,
                createdAt = s.createdAt}).ToListAsync();
        }

        public async Task<WishedGame> UpdateWishedGame(WishedGame wishedGame)
        {
            var existing = await _context.dbWishedGames.FindAsync(wishedGame.id);
            if (existing != null)
            {
                existing.ownedGameId = wishedGame.id;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(WishedGame game)
        {
            await _context.dbWishedGames.AddAsync(game);
            _context.SaveChanges();
        }

        public async Task DeleteWishedGame(Guid id)
        {
            var requirement = await _context.dbWishedGames.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WishedGame?> GetById(Guid id)
        {
            var response = await _context.dbWishedGames
                .Where(x => x.id == id)
                .Select(s => new WishedGame
                {
                    id = s.id,
                    ownedGameId = s.ownedGameId,
                    userId = s.userId,
                    createdAt = s.createdAt
                }).FirstOrDefaultAsync();
            if(response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<WishedGame?> GetByUserAndGameId(Guid id, Guid gameId)
        {
            var response = await _context.dbWishedGames
                .Where(x => x.userId == id)
                .Where(x => x.ownedGameId == gameId)
                .Select(s => new WishedGame
                {
                    id = s.id,
                    ownedGameId = s.ownedGameId,
                    userId = s.userId,
                    createdAt = s.createdAt
                }).FirstOrDefaultAsync();
            if(response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<WishedGame?>> GetIds(List<Guid> ids)
        {
            List<WishedGame> response = new List<WishedGame>();

            foreach (var id in ids)
            {
                var result = await _context.dbWishedGames
                    .Where(x => x.id == id)
                    .Select(s => new WishedGame
                    {
                        id = s.id,
                        ownedGameId = s.ownedGameId,
                        userId = s.userId,
                        createdAt = s.createdAt
                    }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }

            if(response != null)
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
