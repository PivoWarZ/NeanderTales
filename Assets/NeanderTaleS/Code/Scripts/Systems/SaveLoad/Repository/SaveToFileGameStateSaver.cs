using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository
{
    public sealed class SaveToFileGameStateSaver: IGameStateSaver
    {
        private const string SAVE_KEY = "SAVE_TO_FILE";

        Dictionary<string, string> IGameStateSaver.LoadState()
        {
            string path = BuildPath(SAVE_KEY);

            using (var filestream = new StreamReader(path))
            { 
                string json = filestream.ReadToEnd();
                Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (data != null)
                {
                    Debug.Log($"<color=green> LOAD DATA COMPLETED </color>");
                    return data;
                }

                Debug.Log($"<color=red> DATA NOT FOUND </color>");
                return new Dictionary<string, string>();
            }

        }

        void IGameStateSaver.SaveState(Dictionary<string, string> data)
        {
            string path = BuildPath(SAVE_KEY);
            string json = JsonConvert.SerializeObject(data);

            using (var filestream = new StreamWriter(path))
            { 
                filestream.Write(json);
            }
            Debug.Log($"<color=yellow> SAVE DATA COMPLETED </color>");
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.dataPath + "/Saves", key);
        }
    }
}