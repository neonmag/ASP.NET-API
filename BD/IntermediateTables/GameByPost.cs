﻿using Ice_Hot_Tea.Entity.Abstract;

namespace Ice_Hot_Tea.BD.IntermediateTables
{
    public class GameByPost : DBRecord
    {
        public string gameId {  get; set; }
        public string postId { get; set; }
    }
}
