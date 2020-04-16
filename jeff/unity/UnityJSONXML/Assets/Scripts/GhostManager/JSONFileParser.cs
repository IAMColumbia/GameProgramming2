using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JSONOjbectMap
{
    public class JSONFileParser<T> 
    {
        public bool ShowDebugLog { get; set; }

        public JSONFileParser()
        {
        
            ShowDebugLog = true;
        }

        public void SaveToJSON( string jsonFileName, string Path, string json)
        {
            var sr = File.CreateText(Path + jsonFileName);
            sr.Write(json);
            sr.Close();
            
        }

        public string Json { get { return json; } }
        string json;

        public T LoadFromJSON(string jsonFile, string Path)
        {
            //string Path = System.AppDomain.CurrentDomain.DynamicDirectory;
            json = this.ReadTextFile(jsonFile, Path);
#if DEBUG
            if(ShowDebugLog)
            {
                Debug.Log($"LoadFromJSON:{json}");
            }
#endif
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            var obj = JsonUtility.FromJson<T>(json);
#if DEBUG
            if (ShowDebugLog)
            {
                Debug.Log($"parsed:{obj.ToString()}");
            }
#endif
            return obj;
        }

        public System.IO.FileStream GetOpenORCreateFileStream(string Name, string Path)
        {
            return new System.IO.FileStream(System.IO.Path.Combine(Path, Name), System.IO.FileMode.OpenOrCreate);
        }

        public string ReadTextFile(string Name, string Path)
        {
            string fileContents;
            FileStream fileStream = GetOpenORCreateFileStream(Name, Path);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContents = reader.ReadToEnd();
            }
            return fileContents;

        }
    }
}
