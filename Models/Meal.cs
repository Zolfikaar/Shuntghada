using System;
using SQLite;

namespace Shuntghada.Models
{
    [Table("Meals")]
    public class Meal 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed] // لتسريع البحث بالاسم لاحقاً
        public string Name { get; set; } = string.Empty;

        [Indexed]
        public string AltName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Indexed]
        public int CategoryId { get; set; } 

        [Indexed]
        public int KitchenTypeId { get; set; }

        public string RecipeInstructions { get; set; } = string.Empty;

        public string RecipeVideoUrl { get; set; } = string.Empty;

        public string RecipeImageUrl { get; set; } = string.Empty;

        public int PreparationTimeInMinutes { get; set; }

        public string DifficultyLevel { get; set; } = string.Empty;
    }
}
