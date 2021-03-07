using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Stack<GameObject> panels = new Stack<GameObject>();

    public int panelsCount
    {
        get { return panels.Count; }
        set { }
    }

    public GameObject PausePanelPrafab;
    [HideInInspector] public GameObject _pausePanel;

    public GameObject SettingsPanelPrafab;
    [HideInInspector] public GameObject _settingsPanel;



    //返回游戏


    private void Start()
    {
        _pausePanel = GameObject.Instantiate(PausePanelPrafab, transform.position, Quaternion.identity);
        _pausePanel.transform.SetParent(transform);
        _settingsPanel = GameObject.Instantiate(SettingsPanelPrafab, transform.position, Quaternion.identity);
        _settingsPanel.transform.SetParent(transform);

    }

    public void PushInPanels(GameObject panel)
    {

        if (panels.Count != 0)
            panels.Peek().SetActive(false);
        panels.Push(panel);
        panel.SetActive(true);
        Time.timeScale = 0;

    }


    public void ClearPanels()
    {
        panels.Clear();
        Time.timeScale = 1;
    }
    public void PopOutPanels()
    {
            panels.Pop().SetActive(false);
            if (panels.Count > 0)
                panels.Peek().SetActive(true);
            else
                Time.timeScale=1;
            
    }

    public void EscKeyDown()
    {
        if (panels.Count == 0)
        {
            PushInPanels(_pausePanel);
        }
    }

}
