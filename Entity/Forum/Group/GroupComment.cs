﻿using Slush.Data.Entity.Profile;
using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community
{
    public class GroupComment
    {
        public GroupComment(Guid id, String groupId, String content, Guid userId, DateTime? createdAt)
        {
            this.id = id;
            this.groupId = groupId;
            this.content = content;
            this.userId = userId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public String groupId { get; set; }
        public String content { get; set; }
        public Guid userId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}