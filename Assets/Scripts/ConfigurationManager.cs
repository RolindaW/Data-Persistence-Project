using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    private const string FILENAME = "savefile.json";

    public static ConfigurationManager Instance { get; private set; }
    public string Name { get; private set; }
    public int Score { get; private set; }
    
    public string ActiveName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadConfiguration();
    }

    [Serializable]
    private class Configuration
    {
        public string name;
        public int score;
    }

    public void SaveConfiguration()
    {
        var data = new Configuration();
        data.name = Name;
        data.score = Score;

        var json = JsonUtility.ToJson(data);
        
        File.WriteAllText(GetPersistantDataPath(), json);
    }

    public void LoadConfiguration()
    {
        var path = GetPersistantDataPath();
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            
            var data = JsonUtility.FromJson<Configuration>(json);

            Name = data.name;
            Score = data.score;
        }
    }

    private string GetPersistantDataPath()
    {
        return String.Format("{0}/{1}", Application.persistentDataPath, FILENAME);
    }
}
