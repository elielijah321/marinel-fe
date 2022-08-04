using SchoolDraftWebsite.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SchoolDraftWebsite.Data.SeedData
{
    public static class SeedHelper
    {
        const string seedDataFolder = "Data/SeedData";

        public static IEnumerable<Class> GetClasses(string contentRootPath)
        {
            string filePath = Path.Combine(contentRootPath, $"{seedDataFolder}/Classes.json");
            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<Class>>(json);
        }

        public static IEnumerable<Student> GetStudents(string contentRootPath)
        {
            string filePath = Path.Combine(contentRootPath, $"{seedDataFolder}/Students.json");
            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<Student>>(json);
        }

        public static IEnumerable<ExpenseType> GetExpenseTypes(string contentRootPath)
        {
            string filePath = Path.Combine(contentRootPath, $"{seedDataFolder}/ExpenseType.json");
            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<ExpenseType>>(json);
        }

        public static IEnumerable<InventoryType> GetInventoryTypes(string contentRootPath)
        {
            string filePath = Path.Combine(contentRootPath, $"{seedDataFolder}/InventoryTypes.json");
            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<InventoryType>>(json);
        }

        public static IEnumerable<InventoryItem> GetInventoryItems(string contentRootPath)
        {
            string filePath = Path.Combine(contentRootPath, $"{seedDataFolder}/InventoryItems.json");
            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<InventoryItem>>(json);
        }
    }
}
