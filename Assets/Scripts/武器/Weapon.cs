using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    public abstract float ShootCD{get;set;}
    public abstract void fire();
}
