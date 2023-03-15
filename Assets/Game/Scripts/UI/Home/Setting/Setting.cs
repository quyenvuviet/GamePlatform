using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : FrameBase
{
    [SerializeField]
    private Button buttonBack;
    [SerializeField]
    private Slider silderMusic;
    [SerializeField]
    private Slider silderSound;
    [SerializeField]
    private Dropdown DropdownGraphic;
    [SerializeField]
    private Dropdown DropdownLanguage;


    List<string> Graphic = new List<string>()
    {
       "low","hight","Nomal"
    };
    List<string> language = new List<string>()
    {
       "VN","USA","ENG"
    };

    private void Awake()
    {
        Init();
        gameObject.SetActive(false);
    



    }
    private void LoadData()
    {
       /* DropdownGraphic.value = SaveFile.GetIndexGraphic();
        DropdownLanguage.value = SaveFile.GetLanguage();
        silderMusic.value = SaveFile.GetMusic();
        silderSound.value = SaveFile.GetSound();*/
    }
    // Start is called before the first frame update
    void Start()
    {
        //LoadData();
        buttonBack.onClick.AddListener(OnButtonBack);
        silderMusic.onValueChanged.AddListener(OnValueChangeMusic);
        silderSound.onValueChanged.AddListener(OnValueChangeSound);
     // (OnDropdownGraphic)

    }
    private void OnButtonBack()
    {
        gameObject.SetActive(false);
    }
    public void OnButtonshow()
    {
        gameObject.SetActive(true);
    }

    private void OnValueChangeMusic(float value)
    {
      
      
    }
    private void OnValueChangeSound(float value)
    {
       
      

    }
    private void Init()
    {
        DropdownGraphic.ClearOptions();
        foreach(var data in Graphic)
        {
            var options = new Dropdown.OptionData()
            {
               text = data,
            };
            DropdownGraphic.options.Add(options);
        }
        DropdownGraphic.onValueChanged.AddListener(delegate
        {
           /* SaveFile.dataSetting.indexGraphic = DropdownGraphic.value;
            SaveFile.SaveData();*/
        });
        DropdownLanguage.ClearOptions();
        foreach (var data in language)
        {
            var options = new Dropdown.OptionData()
            {
                text = data,
            };
            DropdownLanguage.options.Add(options);
        }
        DropdownLanguage.onValueChanged.AddListener(delegate
        {
            /*SaveFile.dataSetting.indexGraphic = DropdownLanguage.value;
            SaveFile.SaveData();*/
        });


    }
    // Update is called once per frame
   
}
