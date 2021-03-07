
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class EnemySave 
{
  
  public List<Vector3> Enemyposition=new List<Vector3>();
  public List<Quaternion> EnemyRotation=new List<Quaternion>();

  public List<bool> EnemyDeadFlag=new List<bool>();

  public List<int> EnemyHealth=new List<int>();
// public Transform player;
// public int playerHealth;

}
