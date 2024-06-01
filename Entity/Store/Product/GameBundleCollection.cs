﻿namespace Slush.Entity.Store.Product
{
    public class GameBundleCollection
    {
        public GameBundleCollection() { }

        public GameBundleCollection(Guid id, Guid gameId, Guid bundleId, DateTime? createdAt) 
        {
            this.id = id;
            this.gameId = gameId;
            this.bundleId = bundleId;
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid bundleId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
