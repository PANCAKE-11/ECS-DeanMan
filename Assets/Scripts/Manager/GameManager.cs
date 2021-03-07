using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    


    public Texture2D normalCursor;
    public Texture2D AimCursor;


    
  
    /// <summary>
    /// 主角相关的存储
    /// </summary>
  
    private void Start() {
        Cursor.SetCursor(normalCursor,Vector2.zero,CursorMode.Auto);
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor,Vector2.zero,CursorMode.Auto);

    }

    
    public void SetAimCursor()
    {
        Cursor.SetCursor(AimCursor,Vector2.zero,CursorMode.Auto);

    }

   
}
