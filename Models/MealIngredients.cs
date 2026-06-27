using SQLite;
using System;


namespace Shuntghada.Models
{
    [Table("MealIngredients")]
    public class MealIngredients
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int MealId { get; set; }

        [Indexed]
        public int IngredientId { get; set; }
    }
}
