using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shuntghada
{
    public static class Constants
    {
        public const string DatabaseFilename = "ShuntghadaDatabase.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // فتح قاعدة البيانات في وضع القراءة والكتابة
            SQLite.SQLiteOpenFlags.ReadWrite |
            // إنشاء الملف إذا لم يكن موجوداً تلقائياً
            SQLite.SQLiteOpenFlags.Create |
            // تمكين الوصول الآمن من خيوط معالجة متعددة (Multi-threaded access)
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
