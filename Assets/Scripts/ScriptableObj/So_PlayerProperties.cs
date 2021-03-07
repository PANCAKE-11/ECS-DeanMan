using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_SceneRouteList", menuName = "Scriptable Objects/PlayerProperties")]
public class So_PlayerProperties : ScriptableObject 
{
  
    public Vector3 position=Vector3.zero;

    public Quaternion roatition=Quaternion.identity;
    public bool HaveGun;

    public int HealthValue;
}
