﻿namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameGuideModel
    {
        public Guid id { get; set; }
        public String title { get; set; }
        public String? description { get; set; }
        public int likesCount { get; set; }
        public int dislikesCount { get; set; }
        public Guid discussionId { get; set; }
        public Guid gameId { get; set; }
        public Guid authorId { get; set; }
        public Guid gameGroupId { get; set; }
        public String content { get; set; }
    }
}
