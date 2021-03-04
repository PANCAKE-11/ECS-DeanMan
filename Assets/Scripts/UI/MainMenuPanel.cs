using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;
    public Button settingBth;

    public Button continueBtn;

    private Vector3 spawnPos=Vector3.one;
    public SceneName nextScene;
    private void OnEnable()
    {
        startBtn.onClick.AddListener(StartAContinueBtnDown);
        continueBtn.onClick.AddListener(StartAContinueBtnDown);
        settingBth.onClick.AddListener(SettingBtnDown);
        exitBtn.onClick.AddListener(ExitBtnDown);
    }

private void Start() {
    UIManager.Instance.PushInPanels(this.gameObject);
}
    public void ExitBtnDown()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void StartAContinueBtnDown()
    {
        //TODO Savemanager 根据存档加载不同场景
        LevelManager.Instance.StartSwitchScene(nextScene.ToString(),spawnPos);
    }

    public void SettingBtnDown()
    {

        UIManager.Instance.PushInPanels(UIManager.Instance._settingsPanel);

    }


     public void Pause()
    {
        System.GC.Collect();
    }


    private void OnDisables() {
        startBtn.onClick.RemoveAllListeners();
        settingBth.onClick.RemoveAllListeners();
        continueBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
    }
}