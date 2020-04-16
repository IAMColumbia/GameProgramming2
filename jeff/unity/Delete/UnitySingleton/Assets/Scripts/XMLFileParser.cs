
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace JSONOjbectMap
{
    class XMLFileParser<T>
    {
        
        public bool ShowDebugLog { get; set; }

        public XMLFileParser()
        {
            ShowDebugLog = true;
        }

        public T LoadFromXML(string xmlFile, string Path)
        {
            
            string xml = this.ReadTextFile(xmlFile, Path);
#if DEBUG
            if (ShowDebugLog)
            {
                UnityEngine.Debug.Log($"LoadFromXML:{xml}");
            }
#endif
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            T obj = (T)serializer.Deserialize(stream);
#if DEBUG
            if (ShowDebugLog)
            {
                UnityEngine.Debug.Log($"parsed:{obj.ToString()}");
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
