﻿namespace FullStackBrist.Server.Models.Creators
{
    public class PublisherModel
    {
        public Guid id { get; set; }
        public int subscribersCount { get; set; }
        public String? name { get; set; }
        public String? description { get; set; }
        public String? avatar {  get; set; }
        public String? backgroundImage { get; set; }
        public String? urlForNewsPage { get; set; }
        public DateTime createdAt { get; set; }
    }
}
