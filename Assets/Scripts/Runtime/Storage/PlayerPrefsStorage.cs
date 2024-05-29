using System;
using UnityEngine;

namespace HGtest.Storage
{
    public class PlayerPrefsStorage : IStorage
    {
        public void Save<T>(string key, T data)
        {
            try
            {
                if (data == null)
                {
                    PlayerPrefs.DeleteKey(key);
                    PlayerPrefs.Save();

                    return;
                }

                var json = JsonUtility.ToJson(data);
                
                PlayerPrefs.SetString(key, json);
                PlayerPrefs.Save();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"[{nameof(PlayerPrefsStorage)}] exception: {exception}");
                throw;
            }
        }

        public T Load<T>(string key)
        {
            var json = PlayerPrefs.GetString(key, string.Empty);

            var result = JsonUtility.FromJson<T>(json);

            return result;
        }

        public bool IsExists(string key)
        {
            var isExists = !string.IsNullOrWhiteSpace(PlayerPrefs.GetString(key, string.Empty));

            return isExists;
        }
    }
}