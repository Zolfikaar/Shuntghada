using System;
using SQLite;

namespace Shuntghada.Models
{
    [Table("KitchenTypes")]
    public class KitchenType
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; } = string.Empty; // عراقي، شامي، مغربي...

        public string CountryCode { get; set; } = string.Empty; // IQ, SY, MA...

        public string FlagIconUrl { get; set; } = string.Empty; // رابط صورة علم الدولة
    }
}
