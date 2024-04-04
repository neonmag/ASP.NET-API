﻿using Slush.Entity.Abstract;

namespace Slush.Entity.Profile
{
    public class WishedGame :ProfileGame
    {
        public WishedGame(string id, string ownedGameId, string userId)
        {
            this.id = id;
            this.ownedGameId = ownedGameId;
            this.userId = userId;
        }

        public String id { get; set; }
        public String ownedGameId { get; set; }
        public String userId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}
