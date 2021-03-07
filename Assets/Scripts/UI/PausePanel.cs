using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePanel : MonoBehaviour
{

    public Button resumeBtn;
    public Button settingBtn;
    public Button exitBtn;

    public Button mainMenuBtn;
    public  SceneName nextScene;

    private void OnEnable()
    {
        Pause();

        resumeBtn.onClick.AddListener(ResumeBtnDown);
        settingBtn.onClick.AddListener(SettingBtnDown);
        exitBtn.onClick.AddListener(ExitBtnDown);
        mainMenuBtn.onClick.AddListener(mainMenuBtnDown);
    }
    private void OnDisable()
    {
        resumeBtn.onClick.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
        mainMenuBtn.onClick.RemoveAllListeners();

    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                UIManager.Instance.PopOutPanels();
        }
    }
    private void SettingBtnDown()
    {
        UIManager.Instance.PushInPanels(UIManager.Instance._settingsPanel);
    }
    public void Pause()
    {
        System.GC.Collect();
    }
    public void ResumeBtnDown()
    {
        UIManager.Instance.PopOutPanels();
    }
    public void ExitBtnDown()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    public void mainMenuBtnDown()
    {
        LevelManager.Instance.StartSwitchScene(nextScene.ToString());
        PlayerController.Instance.gameObject.SetActive(false);
        
    }
   

}