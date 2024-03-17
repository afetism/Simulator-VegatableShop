using f_1.Models;
using System.IO;
using System.Text.Json;

namespace f_1.DB
{
    internal class MarketDB<T>
    {
        string Path { get; set; }
        public MarketDB(string path)
        {
            Path= path;
            if (File.Exists(Path))
            {
                var productJson = File.ReadAllText(Path);
                GetReports = JsonSerializer.Deserialize<List<T>>(productJson) ?? new();
            }
            else
                GetReports = new();
        }

        public List<T> GetReports { get; set; }

        public void SaveChanges()
        {
            var ReportJson = JsonSerializer.Serialize(GetReports);
            File.WriteAllText(Path, ReportJson);
        }

    }
}
