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


    private void OnEnable()
    {
        Pause();

        resumeBtn.onClick.AddListener(ResumeBtnDown);
        settingBtn.onClick.AddListener(SettingBtnDown);
        exitBtn.onClick.AddListener(ExitBtnDown);
        mainMenuBtn.onClick.AddListener(mainMenuBtnDown);
    }
    private void SettingBtnDown()
    {
        UIManager.Instance.PushInPanels(UIManager.Instance.SettingsPanel);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        System.GC.Collect();
    }
    public void ResumeBtnDown()
    {
        Time.timeScale = 1;
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
    //todo 返回主菜单按钮
    public void mainMenuBtnDown()
    {
        
    }
    private void OnDisable()
    {
        resumeBtn.onClick.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
        mainMenuBtn.onClick.RemoveAllListeners();
    }

}