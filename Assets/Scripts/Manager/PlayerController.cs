using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] CharacterController _characterController;
    /// <summary>
    /// 移动
    /// </summary>
    private Vector3 _moveDir;
    public Vector3 MoveDir
    {
        get
        {
            return _moveDir;
        }
    }
    private Vector3 _mousePos;
    private Camera _camera;
    public int Speed
    {
        get
        {
            return _speed;
        }

        
    }

    private bool _death=false;
    /// <summary>
    /// 射击
    /// </summary>
    private IWeapon _weapon;

    [Header("玩家属性")]
    [SerializeField] private float _shootCD;
    [SerializeField] private int _speed;

    [SerializeField] private int _health;
    public int Healeth{
        get{
            return _health;
        }
        set
        {
            _health=value;
        }
    }
    private bool _aim;

    public Transform ShootPoint;
    /// <summary>
    /// 动画
    /// </summary>
    Animator _animator;


    private void Start()
    {
        _camera = Camera.main;
        _weapon = transform.GetComponentInChildren<IWeapon>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(!_death)
      {  if (Time.timeScale == 1)
        {
            Rotate();
            AttackAnim();
        }
        else  
        {
            _animator.SetBool("Aim", false);
             _animator.SetFloat("Speed",0);
        }
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                UIManager.Instance.EscKeyDown();
                GameManager.Instance.SetNormalCursor();
        }}
    }
    private void FixedUpdate()
    {
        if(!_death)
        Move();
    }
    private void AttackAnim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _aim = !_aim;
            if(_aim)
                GameManager.Instance.SetAimCursor();
            else{
                GameManager.Instance.SetNormalCursor();
            }
            _animator.SetBool("Aim", _aim);
        }
        if (Input.GetMouseButtonDown(0) && _shootCD <= 0 && _aim)
        {
            _animator.SetBool("Shoot", true);
            _shootCD = _weapon.ShootCD;
        }
        else if (_shootCD > 0)
        {
            _shootCD -= Time.deltaTime;
        }

    }
    private void Move()
    {

        Vector2 moveDir=new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        if (moveDir.y > 0)
        {
            _characterController.SimpleMove(_speed * moveDir.y* transform.forward);

        }
        else if (moveDir.y < 0)
        {
            _characterController.SimpleMove(_speed * 0.5f *moveDir.y * transform.forward);

        }
        _animator.SetFloat("Speed", moveDir.y);

    //   else if (moveDir.x > 0)
    //     {
    //         _characterController.SimpleMove(_speed *0.5f* moveDir.x* transform.right);
    //     _animator.SetFloat("Speed",-Mathf.Abs( moveDir.x));

    //     }
    //     else if (moveDir.x < 0)
    //     {
    //         _characterController.SimpleMove(_speed * 0.5f *moveDir.x * transform.right);
    //     _animator.SetFloat("Speed",-Mathf.Abs( moveDir.x));

    //     }
        

    }
    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 200))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

    }


    public void ChangeWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public void OpenFire()
    {

        _weapon.fire();

    }
//TODO 玩家受到伤害
    public void TakeDamage(int Damage)
    {
        _health-=Damage;
            if(_health<=0)
            {
               _animator.SetTrigger("Die");
               EventHandler.CallPlayerDieEvent();
                _death=true;
            }
    }
}
