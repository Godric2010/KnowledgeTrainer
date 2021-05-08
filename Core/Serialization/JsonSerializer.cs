using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Serialization
{
    internal class JsonSerializer
    {

        private const string dbName = "db.json";

        public static void SaveCardsToDisk(IEnumerable<Cards.Card> cards, string path)
        {
            var jsonString = JsonConvert.SerializeObject(cards);
            System.IO.File.WriteAllText(path + '\\' + dbName, jsonString);
        }

        public static IEnumerable<Cards.Card> LoadCardsFromDisk(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var filePath = path + '\\' + dbName;

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }
          
            var jsonString = System.IO.File.ReadAllText(filePath);
            var cards = JsonConvert.DeserializeObject<IEnumerable<Cards.Card>>(jsonString);
            return cards;
        }

        public static void SaveIniFileToDisk(string path)
        {

        }

        public static void LoadIniFileFromDisk(string path)
        {

        }
    }
}
