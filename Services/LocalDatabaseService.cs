using SQLite;
using Shuntghada.Models;

namespace Shuntghada.Services
{
    public class LocalDatabaseService
    {
        private SQLiteAsyncConnection _database;

        public LocalDatabaseService()
        {
        }

        // دالة لتهيئة الاتصال وإنشاء الجداول إذا لم تكن موجودة
        private async Task InitAsync()
        {
            if (_database != null)
                return;

            // إنشاء الاتصال باستخدام المسار والـ Flags المعرفة في Constants
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            // إنشاء الجداول الستة
            await _database.CreateTableAsync<KitchenType>();
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<Meal>();
            await _database.CreateTableAsync<Ingredient>();
            await _database.CreateTableAsync<MealIngredients>();
            await _database.CreateTableAsync<MealHistory>();

            // تعبئة البيانات الأولية فوراً بعد إنشاء الجداول
            await SeedDatabaseAsync();
        }

        // دالة لتعبئة البيانات الأولية (Seed Data) بالكامل وبشكل مترابط
        private async Task SeedDatabaseAsync()
        {
            // 1. تعبئة أنواع المطابخ (KitchenTypes)
            var kitchenCount = await _database.Table<KitchenType>().CountAsync();
            if (kitchenCount == 0)
            {
                var initialKitchens = new List<KitchenType>
                {
                    new KitchenType { Name = "عراقي", CountryCode = "IQ" },  // Id = 1
                    new KitchenType { Name = "شامي", CountryCode = "SY" },   // Id = 2
                    new KitchenType { Name = "مغربي", CountryCode = "MA" }   // Id = 3
                };
                await _database.InsertAllAsync(initialKitchens);
            }

            // 2. تعبئة الأقسام (Categories)
            var categoryCount = await _database.Table<Category>().CountAsync();
            if (categoryCount == 0)
            {
                var initialCategories = new List<Category>
                {
                    new Category { Name = "تمن طباخ" },     // Id = 1
                    new Category { Name = "مرق وتغماس" },   // Id = 2
                    new Category { Name = "نواشف ومقالي" }, // Id = 3
                    new Category { Name = "بحريات" }        // Id = 4
                };
                await _database.InsertAllAsync(initialCategories);
            }

            // 3. تعبئة الأكلات (Meals) مع الحقول الجديدة: Description و AltName
            var mealCount = await _database.Table<Meal>().CountAsync();
            if (mealCount == 0)
            {
                var initialMeals = new List<Meal>
                {
                    new Meal
                    {
                        Name = "دولمة عراقية",
                        AltName = "ورق عنب عراقي / محاشي مشكلة",
                        Description = "سلطانة المائدة العراقية، عبارة عن خضروات مشكلة محشوة بخلطة الرز واللحم الغنية بالنكهات الحامضة واللاذعة.",
                        CategoryId = 3,
                        KitchenTypeId = 1,
                        DifficultyLevel = "صعب",
                        RecipeImageUrl = "dolma.jpg",
                        RecipeInstructions = "حفر الخضروات (بصل، باذنجان، كوسا)، حشوها بالرز واللحم المفروم والبهارات، وطبخها على نار هادئة."
                    },
                    new Meal
                    {
                        Name = "برياني دجاج",
                        AltName = "تمن برياني",
                        Description = "طبق الأعياد والمناسبات، رز مبهر يمزج بين الشعرية المحموسة، المكسرات، قطع البطاطا، والكشمش مع الدجاج المشوي.",
                        CategoryId = 1,
                        KitchenTypeId = 1,
                        DifficultyLevel = "متوسط",
                        RecipeImageUrl = "biryani.jpg",
                        RecipeInstructions = "طبخ الرز مع بهارات البرياني، وقلي البطاطا، البزاليا، الجزر، الكشمش، اللوز، وشوي الدجاج وخلطهم معاً."
                    },
                    new Meal
                    {
                        Name = "تشريب لحم أصفر",
                        AltName = "ثريد لحم / فتشة",
                        Description = "طبق تراثي مغذٍّ يعتمد على مرق اللحم النقي والمبهر بالكركم ونومي البصرة، يسكب فوق خبز الرقاق العراقي.",
                        CategoryId = 2,
                        KitchenTypeId = 1,
                        DifficultyLevel = "متوسط",
                        RecipeImageUrl = "tashreeb.jpg",
                        RecipeInstructions = "سلق اللحم مع البصل ونومي البصرة والكركم حتى النضوج، ثم يصب المرق فوق خبز الرقاق."
                    },
                    new Meal
                    {
                        Name = "سمك مسكوف",
                        AltName = "مسكوف بغدادي",
                        Description = "أشهر طريقة عراقية لشوي السمك (غالباً الشبوط أو البني) على أعواد خشب الصفصاف ليعطيه نكهة التدخين المميزة.",
                        CategoryId = 4,
                        KitchenTypeId = 1,
                        DifficultyLevel = "متوسط",
                        RecipeImageUrl = "masgouf.jpg",
                        RecipeInstructions = "شق السمكة من الظهر، تمليحها، وشيها على حطب واقداً من الجانب، وتتبيلها بصلصة الطماطم والثمر هندي."
                    },
                    new Meal
                    {
                        Name = "قيمة نجفية مع تمن",
                        AltName = "مرقة القيمة",
                        Description = "مرق غليظ وقوام متجانس جداً يطبخ في المناسبات، يدمج اللحم والحمص المهروسين تماماً مع بهارات القيمة الخاصة.",
                        CategoryId = 2,
                        KitchenTypeId = 1,
                        DifficultyLevel = "صعب",
                        RecipeImageUrl = "gheema.jpg",
                        RecipeInstructions = "سلق الحمص واللحم (بنسب متساوية) ثم هرسهم جيداً حتى يندمجوا، وإضافة المعجون والبهارات ونومي البصرة."
                    },
                    new Meal
                    {
                        Name = "مرقة بامية باللحم",
                        AltName = "بامية وتمن",
                        Description = "المرقة الأكثر شعبية وعشقاً في العراق، تمتاز بصلصتها الكثيفة المسبكة بالثوم ولحم الغنم الصغار.",
                        CategoryId = 2,
                        KitchenTypeId = 1,
                        DifficultyLevel = "سهل",
                        RecipeImageUrl = "bamia.jpg",
                        RecipeInstructions = "حمس اللحم والثوم والبامية الطازجة، إضافة عصير الطماطم والمعجون، وتركها تتسبك على نار هادئة."
                    },
                    new Meal
                    {
                        Name = "كبة حلب (رز)",
                        AltName = "كبة رز مقلية",
                        Description = "أقراص مقرمشة من عجينة الرز المتبلة بالكركم، محشوة باللحم المفروم المفرقع مع البصل والمعدنوس.",
                        CategoryId = 3,
                        KitchenTypeId = 1,
                        DifficultyLevel = "صعب",
                        RecipeImageUrl = "kubba_halab.jpg",
                        RecipeInstructions = "عجن الرز المسلوق مع الكركم، حشوه باللحم المفروم المحموس مع البصل والمعدنوس، ثم قليها بزيت غزير."
                    },
                    new Meal
                    {
                        Name = "مقلوبة دجاج",
                        AltName = "مقلوبة باذنجان",
                        Description = "طبق مشهور يجمع طبقات مقلية من الباذنجان والبطاطا والدجاج تحت طبقة رز متبل، يقلب بحذر ليقدم كقالب رائع.",
                        CategoryId = 1,
                        KitchenTypeId = 1,
                        DifficultyLevel = "متوسط",
                        RecipeImageUrl = "maqluba.jpg",
                        RecipeInstructions = "صف طبقات من الباذنجان والبطاطا والطماطم المقلية مع الدجاج، وضع الرز فوقها، ثم قلب القدر عند التقديم."
                    },
                    new Meal
                    {
                        Name = "شيخ المحشي",
                        AltName = "محشي باذنجان باللحم / مخشي",
                        Description = "أقراص باذنجان أو شجر محشوة بالكامل باللحم المفروم والبصل المطبوخ، ومسبكة داخل مرق الطماطم الخفيف.",
                        CategoryId = 2,
                        KitchenTypeId = 1,
                        DifficultyLevel = "متوسط",
                        RecipeImageUrl = "sheikh_mahshi.jpg",
                        RecipeInstructions = "حشو الباذنجان أو الشجر باللحم المفروم والبصل، قليه خفيفاً، ثم طبخه بصلصة الطماطم."
                    },
                    new Meal
                    {
                        Name = "كباب طاوه",
                        AltName = "كباب عروق",
                        Description = "أقراص لحم مفروم مخلوطة بالخضار الطازجة والطحين، تقلى بالطاوة بسرعة لتقدم كوجبة عشاء أو غداء خفيفة وسريعة.",
                        CategoryId = 3,
                        KitchenTypeId = 1,
                        DifficultyLevel = "سهل",
                        RecipeImageUrl = "kebab_tawa.jpg",
                        RecipeInstructions = "خلط اللحم المفروم مع البصل والطماطم المفرومة، الطحين والبهارات، وتشكيلها أقراص وقليها."
                    }
                };

                await _database.InsertAllAsync(initialMeals);
            }
        }

        // دالة جلب كل الأكلات للتأكد من نجاح العملية
        public async Task<List<Meal>> GetMealsAsync()
        {
            await InitAsync();
            return await _database.Table<Meal>().ToListAsync();
        }

        // دالة للحصول على اقتراح طبخة 
        public async Task<Meal> GetRandomMealSuggestionAsync(int coolDownDays = 7)
        {
            await InitAsync();

            // 1. حساب تاريخ الحد الأدنى لمنع التكرار (تاريخ اليوم ناقصاً عدد أيام الـ coolDown)
            DateTime thresholdDate = DateTime.Today.AddDays(-coolDownDays);

            // 2. جلب الـ IDs الخاصة بالأكلات التي طُبخت في هذه الفترة من جدول الـ History
            var recentHistories = await _database.Table<MealHistory>()
                                     .Where(h => h.CookedDate >= thresholdDate)
                                     .ToListAsync();
            var recentMealIds = new List<int>();
            foreach (var h in recentHistories)
            {
                recentMealIds.Add(h.MealId);
            }

            // 3. جلب الأكلات المتاحة التي لم تُطبخ مؤخراً
            var availableMeals = await _database.Table<Meal>()
                                                 .Where(m => !recentMealIds.Contains(m.Id))
                                                 .ToListAsync();

            // 4. حماية من الـ Edge Case: إذا طُبخت كل الأكلات ولم يتبقَ شيء، نكسر القاعدة ونجلب كل الأكلات
            if (availableMeals.Count == 0)
            {
                availableMeals = await _database.Table<Meal>().ToListAsync();
            }

            // 5. اختيار أكلة واحدة عشوائياً من القائمة المتاحة
            if (availableMeals.Count > 0)
            {
                var random = new Random();
                int randomIndex = random.Next(availableMeals.Count);
                return availableMeals[randomIndex];
            }

            return null; // في حال كانت قاعدة البيانات فارغة تماماً (وهذا لن يحدث بسبب الـ Seed)
        }

        // دالة لتسجيل الطبخة في السجل عند الموافقة عليها 
        public async Task SaveMealToHistoryAsync(int mealId)
        {
            await InitAsync();

            var historyEntry = new MealHistory
            {
                MealId = mealId,
                CookedDate = DateTime.Today // تسجيلها بتاريخ اليوم الحالي
            };

            await _database.InsertAsync(historyEntry);
        }


    }
}