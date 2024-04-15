﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameComment
    {
        public GameComment(Guid id, String gamePostId, String content, DateTime? createdAt)
        {
            this.id = id;
            this.gamePostId = gamePostId;
            this.content = content;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public String gamePostId { get; set; }
        public String content { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
        public virtual Author Author { get; set; } = null!;
    }
}