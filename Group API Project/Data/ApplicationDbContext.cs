using System.Text.Json;
using Group_API_Project.Models;
using System.Collections.Concurrent;
//Emily Schweitzer

namespace Group_API_Project.Data
{
    public class JsonDataService : IDisposable
    {
        private readonly string _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly ConcurrentDictionary<Type, object> _collections = new();

        public JsonDataService()
        {
            Directory.CreateDirectory(_dataPath);
            LoadAllData();
        }

        // Generic collection accessor
        public List<T> GetCollection<T>() where T : class
        {
            return (_collections.GetOrAdd(typeof(T), _ => new List<T>()) as List<T>)!;
        }

        // Save all data to JSON files
        public void SaveChanges()
        {
            foreach (var collection in _collections)
            {
                var filePath = Path.Combine(_dataPath, $"{collection.Key.Name}.json");
                File.WriteAllText(filePath, JsonSerializer.Serialize(collection.Value));
            }
        }

        // Load all data from JSON files
        private void LoadAllData()
        {
            if (!Directory.Exists(_dataPath)) return;

            foreach (var file in Directory.GetFiles(_dataPath, "*.json"))
            {
                var typeName = Path.GetFileNameWithoutExtension(file);
                var type = Type.GetType($"Group_API_Project.Models.{typeName}");

                if (type != null)
                {
                    var json = File.ReadAllText(file);
                    var listType = typeof(List<>).MakeGenericType(type);
                    var data = JsonSerializer.Deserialize(json, listType);
                    _collections[type] = data ?? Activator.CreateInstance(listType)!;
                }
            }
        }

        public void Dispose() => SaveChanges();
    }
}