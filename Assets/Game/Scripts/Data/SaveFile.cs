using Assets.Game.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public DataSetting dataSetting;
    private void Awake()
    {
        dataSetting = GetComponent<DataSetting>();
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadData()
    {
        string file = "data.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath,"");
        }
        dataSetting = JsonUtility.FromJson<DataSetting>(File.ReadAllText(filePath));
    }
    public void SaveData()
    {
        string file = "data.json";
        string filePath =Path.Combine(Application.persistentDataPath,file);
        string  data = JsonUtility.ToJson(dataSetting);
        File.WriteAllText(filePath, data);
        Debug.Log(filePath);

    }
    public float GetSound()
    {
        return dataSetting.Sound;
    }
    public float GetMusic()
    {
        return dataSetting.Music;
    }
    public int GetIndexGraphic()
    {
        return dataSetting.indexGraphic;
    }
    public int GetLanguage()
    {
        return dataSetting.indexLengauge;
    }

}
