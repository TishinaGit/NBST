using System.IO;
using UnityEngine;

namespace Tools
{
    public class JsonSerialization
    {
        private string _path;

        public JsonSerialization()
        {
        }
        
        public JsonSerialization(string fileName)
        {
            _path = CreatePath(fileName);
        }
        
        public string CreatePath(string fileName)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return Path.Combine(Application.persistentDataPath, fileName);
#else
            fileName = "JsonEditorLoader/" + fileName + ".json";
            return Path.Combine(Application.dataPath, fileName);
#endif
        }

        public (bool, T) DeSerialization<T>()
        {
            T data = default;
            
            if (File.Exists(_path))
            {
                data = JsonUtility.FromJson<T>(File.ReadAllText(_path));
                return (true, data);
            }

            return (false, data);
        }
        
        public void Serialization<T>(T data)
        {
            var textJson = JsonUtility.ToJson(data, true);
            textJson = JsonReducer.ReduceResources(textJson, new string[] { });
            File.WriteAllText(_path, textJson);
        }
        
        public void WriteToTextFile<T>(T group, string path)
        {
            var textJson = JsonUtility.ToJson(group, true);
            textJson = JsonReducer.ReduceResources(textJson, new string[] { });
            File.WriteAllText(path, textJson);
        }
        
        public string DeSerialization(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }
        
        public void WriteToTextFile(string json, string path)
        {
            json = JsonReducer.ReduceResources(json, new string[] { });
            File.WriteAllText(path, json);
        }
    }
}