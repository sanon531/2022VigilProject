using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

[System.Serializable]
public class SaveData
{
    public List<int> passResult = new List<int>();
    public List<int> catchResult = new List<int>();
    public int currentCount = 0;
}


public class LogSaver : MonoBehaviour
{
    string path;
    [SerializeField]
    string path_toSave = "pocaResult.json";

    int currentCount = 0;

    // Start is called before the first frame update
    SaveData formerData = new SaveData();
    void Start()
    {
        path = Path.Combine(Application.dataPath,
                            path_toSave);
        JsonLoad();
    }
    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            string json = JsonUtility.ToJson(formerData, true);
            File.WriteAllText(path, json);
        }

        string loadJson = File.ReadAllText(path);
        saveData = JsonUtility.FromJson<SaveData>(loadJson);
        formerData = saveData;
        if (saveData != null)
        {
            currentCount = saveData.currentCount;
        }
    }

    public void JsonSave(int passVal, int catchVal)
    {
        formerData.currentCount++;
        formerData.passResult.Add(passVal);
        formerData.catchResult.Add(catchVal);
        string json = JsonUtility.ToJson(formerData, true);
        File.WriteAllText(path, json);
    }

}
