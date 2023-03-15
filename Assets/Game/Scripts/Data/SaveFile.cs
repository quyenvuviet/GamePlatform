using Assets.Game.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    [SerializeField]
    public DataSetting DataSetting { get; set; }
    public static SaveFile Intance;

    private void Awake()
    {
        LoadData();
        if (Intance == null)
        {
            Intance = this;
        }
        else return;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*if (DataSetting == null)
        {
            DataSetting = new DataSetting()
            {
                Music = 1,
                Sound =1,
                indexGraphic =1,
                indexLengauge =1,
            };
        }*/
    }
    public void LoadData()
    {
        string file = "data.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath,"");
        }
        DataSetting = JsonUtility.FromJson<DataSetting>(File.ReadAllText(filePath));
    }
    public void SaveData()
    {
        string file = "data.json";
        string filePath =Path.Combine(Application.persistentDataPath,file);
        string  data = JsonUtility.ToJson(DataSetting);
        File.WriteAllText(filePath, data);
        Debug.Log(filePath);

    }
    public void OnApplicationPause(bool pause)
    {
        SaveData();
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }
    public void SetDataSound(float value)
    {
        DataSetting.Sound = value;
        SaveData();
    }
    public void SetDataMusic(float value)
    {
        DataSetting.Music = value;
        SaveData();
    }
    public float GetSound()
    {
        return DataSetting.Sound ;
    }
    public float GetMusic()
    {
        return DataSetting.Music;
    }
    public int GetIndexGraphic()
    {
        return DataSetting.indexGraphic;
    }
    public int GetLanguage()
    {
        return DataSetting.indexLengauge;
    }

}
