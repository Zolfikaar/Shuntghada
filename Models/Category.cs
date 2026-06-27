using System;
using SQLite;

namespace Shuntghada.Models
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique] // لمنع التكرار
        public string Name { get; set; } = string.Empty;

        public string IconUrl { get; set; } = string.Empty; // أيقونة أو صورة صغيرة للقسم مستقبلاً
    }
}
