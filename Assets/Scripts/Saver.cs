using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Saver<T>
    {
        public static void TryLoad(string filename, ref T data)
        {
            var path = FileHandler.Path(filename);

            if(File.Exists(path))
            {
                Debug.Log($"loading from {path}");
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.m_data;
            }
            else
            {
                Debug.Log($"no file at {path}");
            }
        }

        public static void Save(string filename, T data)
        {
            var wrapper = new Saver<T> { m_data = data};
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(FileHandler.Path(filename), dataString);
        }

        public T m_data;
    }

    public static class FileHandler
    {
        public static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        public static void Reset(string filename)
        {
            var path = Path(filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool HasFile(string filename)
        {
            return File.Exists(Path(filename));
        }
    }
}
