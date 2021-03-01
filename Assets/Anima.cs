using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anima : MonoBehaviour
{
    // Start is called before the first frame update
    Movement _movement;
    Animator _animator;
    void Start()
    {
        _movement=GetComponent<Movement>();
        _animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_movement._moveDir==Vector2.up||_movement._moveDir==Vector2.down)
        {
            _animator.SetBool("Run",true);
        }else
        {
            _animator.SetBool("Run",false);
        }
    }
}
