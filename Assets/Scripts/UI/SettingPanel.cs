using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    //TODO 分辨率设置 亮度音量设置
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                UIManager.Instance.PopOutPanels();
        }
    }
}
