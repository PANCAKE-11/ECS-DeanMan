using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField]  CharacterController _characterController;
    public Vector2  _moveDir;
    private Vector3 _mousePos;
    private Camera _camera;
    public float speed;
   private void Start() {
       _camera=Camera.main;
   }
   private void Update() {
       Move();
       Rotate();
   }
   private void Move()
   {
       _moveDir=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
       if (_moveDir== Vector2.up)
       {
           
         _characterController.Move(Time.deltaTime*speed*transform.forward);
         
       }else if (_moveDir==Vector2.down)
       {
             _characterController.Move(Time.deltaTime*-speed*transform.forward);
       }
    //    else if(_moveDir==Vector2.right)
    //    {
    //      _characterController.Move(Time.deltaTime*speed*transform.right);
    //    }
    //    else if(_moveDir==Vector2.left)
    //    {
    //      _characterController.Move(Time.deltaTime*-speed*transform.right);
    //    }
   }
    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
 
        RaycastHit hitInfo;
 
        if(Physics.Raycast(ray, out hitInfo, 200)){
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

    }
}
