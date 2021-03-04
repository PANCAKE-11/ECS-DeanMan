using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler 
{
    /// <summary>
    /// 场景加载事件
    /// </summary>
    public static event Action BeforeSceneUnloadFadeOutEvent;

    public static void CallBeforeSceneUnloadFadeOutEvent()
    {
        if(BeforeSceneUnloadFadeOutEvent!=null)
            BeforeSceneUnloadFadeOutEvent();
    }

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()
    {
       if(BeforeSceneUnloadEvent!=null)
           BeforeSceneUnloadEvent();
    }

    public static event Action AfterSceneLoadEvent;

    public static void CallAfterSceneLoadEvent()
    {
        if(AfterSceneLoadEvent!=null)
            AfterSceneLoadEvent();
    }

    public static event Action AfterSceneLoadFadeInEvent;

    public static void CallAfterSceneLoadFadeInEvent()
    {
        if(AfterSceneLoadFadeInEvent!=null)
            AfterSceneLoadFadeInEvent();
    }
}


