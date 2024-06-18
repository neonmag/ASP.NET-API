﻿namespace Slush.Entity.Abstract
{
    public abstract class Profile : Author
    {
        public string name { get; set; }
        public string description { get; set; }
        public string avatar { get; set; }
        public string backgroundImage { get; set; }
        public List<object> showcases { get; set; } // изменить тип данных который будет отвечать за показ контента в витрине
    }
}