﻿namespace BookLibraryAPI.Models
{
    public class BookLibraryDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> Collections { get; set; }
    }
}
