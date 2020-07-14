using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Game.Data.Persistance
{
    static class GameStatePersistance
    {
        #region GETTERS

        static string FilePath => Path.Combine(Application.persistentDataPath, "GameState.json");

        public static bool HasSaving()
        {
            return File.Exists(FilePath);
        }

        public static GameState LoadGameState()
        {
            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<GameState>(json);
        }

        #endregion

        #region SETTER

        public static void SaveGameState(GameState gameState)
        {
            string json = JsonConvert.SerializeObject(gameState, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Debug.Log("File Saved to: " + FilePath);
        }

        #endregion
    }
}