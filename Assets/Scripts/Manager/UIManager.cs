using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Stack<GameObject> panels=new Stack<GameObject>();
    public GameObject PausePanel;

    public GameObject SettingsPanel;
    
//返回游戏
 

  

    public void PushInPanels(GameObject panel)
    {

        if(panels.Count!=0)
            panels.Peek().SetActive(false);
        panels.Push(panel);
        panel.SetActive(true);
        print(panels.Count);
       
    }

    public void PopOutPanels()
    {
        panels.Pop().SetActive(false);
        if (panels.Count>0)
            panels.Peek().SetActive(true);
        print(panels.Count);
        
    }

    public void EscKeyDown()
    {
        if (panels.Count==0)
        {
            PushInPanels(PausePanel);
        }else
        {
            PopOutPanels();
        }
    }

}
