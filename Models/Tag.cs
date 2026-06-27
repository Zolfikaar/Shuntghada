using System;
using SQLite;

namespace Shuntghada.Models
{
    [Table("Tags")]
    public class Tag
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
