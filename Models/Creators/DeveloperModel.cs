﻿namespace FullStackBrist.Server.Models.Creators
{
    public class DeveloperModel
    {
        public Guid id { get; set; }  
        public String name { get; set; }
        public String description { get; set; } 
        public String avatar {  get; set; }
        public String? backgroundImage { get; set; }
    }
}