using System;
using SQLite;

namespace Shuntghada.Models
{
    [Table("MealHistories")]
    public class MealHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int MealId { get; set; }

        // نصيحة للهواتف: SQLite
        // لا يحتوي على نوع
        // DateTime
        // ،يفضل حفظه كـ
        // DateTime حقيقي
        // والمكتبة تحوله تلقائياً خلف الكواليس،
        // أو كـ String بصيغة ثابته. سنعتمد DateTime
        // هنا والمكتبة ستتكفل بالباقي.
        public DateTime CookedDate { get; set; }
    }
}
