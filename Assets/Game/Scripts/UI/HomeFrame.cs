using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeFrame : FrameBase
{
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_setting;
   

    private void Awake() {
        btn_Play.onClick.AddListener(StartGame);
         btn_setting.onClick.AddListener(onlickSetting);
    }
  
    private void StartGame() {
        SceneManager.Instance.LoadSceneAsyn(SceneManager.SCENE_GAME);
    }
    private void onlickSetting()
    {
        FrameManager.Instance.Push<Setting>();
    }
   
}
