﻿using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Store.Product;

namespace Slush.DAO.GameInShopDao
{
    public class GameBundleCollectionDao
    {
        private readonly DataContext _context;

        public GameBundleCollectionDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameBundleCollection>> GetAll()
        {
            return await _context.dbGameBundleCollections
                .Where(g => g.deletedAt == null)
                .Select(g => new GameBundleCollection
                {
                    id = g.id,
                    gameId = g.gameId,
                    dlcId = g.dlcId,
                    bundleId = g.bundleId
                }).ToListAsync();
        }

        public async Task<GameBundleCollection> UpdateGameBundleCollection(GameBundleCollection gameBundleCollection)
        {
            var existing = await _context.dbGameBundleCollections.FindAsync(gameBundleCollection.id);
            if (existing != null)
            {
                existing.gameId = gameBundleCollection.gameId;
                existing.dlcId = gameBundleCollection.dlcId;
                existing.bundleId = gameBundleCollection.bundleId;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task DeleteGameBundleCollection(Guid id)
        {
            var existing = await _context.dbGameBundleCollections.FindAsync(id);
            if(existing != null)
            {
                existing.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(GameBundleCollection gameBundleCollection)
        {
            await _context.dbGameBundleCollections.AddAsync(gameBundleCollection);
            await _context.SaveChangesAsync();
        }

        public async Task<GameBundleCollection?> GetById(Guid id)
        {
            var response = await _context.dbGameBundleCollections
                .Where(x => x.id == id)
                .Select(g => new GameBundleCollection
                {
                    id = g.id,
                    gameId = g.gameId,
                    dlcId = g.dlcId,
                    bundleId = g.bundleId,
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

        public async Task<List<GameBundleCollection?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGameBundleCollections
                .Where(x => x.gameId == id)
                .Where(d => d.deletedAt == null)
                .Select(g => new GameBundleCollection
                {
                    id = g.id,
                    gameId = g.gameId,
                    dlcId = g.dlcId,
                    bundleId = g.bundleId,
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
    }
}
