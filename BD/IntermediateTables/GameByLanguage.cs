﻿using Slush.Entity.Abstract;

namespace Slush.BD.IntermediateTables
{
    public class GameByLanguage : DBRecord
    {
        public String gameId { get; set; }
        public String languageId { get; set; }
        public bool IsSubtitles { get; set; } = false;
        public bool IsAudio { get; set; } = false;
        public bool IsInterface { get; set; } = false;
    }
}
