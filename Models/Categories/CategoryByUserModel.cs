﻿namespace FullStackBrist.Server.Models.Categories
{
    public class CategoryByUserModel
    {
        public Guid id { get; set; }
        public Guid authorId { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public DateTime createdAt { get; set; }

    }
}
