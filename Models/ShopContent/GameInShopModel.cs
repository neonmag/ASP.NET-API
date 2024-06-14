﻿namespace FullStackBrist.Server.Models.ShopContent
{
    public class GameInShopModel
    {
        public String? name { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public DateTime? discountFinish { get; set; }
        public String? previeImage { get; set; }
        public String? description { get; set; }
        public DateTime dateOfRelease { get; set; }
        public Guid developerId { get; set; }
        public Guid publisherId { get; set; }
        public String? urlForContent { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
