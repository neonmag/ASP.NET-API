﻿namespace FullStackBrist.Server.Models.GameGroup
{
    public class GameTopicModel
    {
        public Guid id { get; set; }
        public String attachedId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
}