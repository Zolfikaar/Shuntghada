using SQLite;
using System;


namespace Shuntghada.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique] // لمنع تكرار نفس المكون
        public string Name { get; set; } = string.Empty;
    }
}
