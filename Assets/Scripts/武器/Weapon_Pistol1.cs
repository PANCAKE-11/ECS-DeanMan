using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pistol1 : MonoBehaviour, IWeapon
{

   [SerializeField]Transform _shootPoint;
    [SerializeField] private ParticleSystem fireVFX;
    [SerializeField] private ParticleSystem hitVFX;

    [SerializeField] private float _CDTime;
    public float ShootCD { get {return _CDTime;}set{}}
       private void Start() {
            _shootPoint=PlayerController.Instance.ShootPoint;
           
       }
       
    public void fire()
    {
        fireVFX.Play();
        RaycastHit ray;
      if ( Physics.Raycast(_shootPoint.position,_shootPoint.forward,out ray,1000))
        { 
         GameObject.Instantiate(hitVFX,ray.point,Quaternion.identity).transform.SetParent(ray.transform);
         }
    }

 private void OnDrawGizmos() {
      Debug.DrawRay(_shootPoint.position,_shootPoint.forward*100);
 }

    
}
