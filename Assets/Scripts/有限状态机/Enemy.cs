using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    /// <summary>
    ///状态
    /// </summary>
    IEnemyAIState _chaseState;
    IEnemyAIState _idleState;
    IEnemyAIState _attackState;
    IEnemyAIState _patrolState;
    //属性


    public Enemy()
    {
        _chaseState = new ChaseState(this);
        _idleState = new IdleState(this);
        _attackState = new AttackState(this);
        _patrolState = new PatrolState(this);

    }

    private IEnemyAIState _nowState = null;
    [SerializeField] private EnemyAIState_Enum beginState;
    [SerializeField] private float _health;
    public float Health
    {
        get { return _health; }
        set { Health = value; }
    }
    private NavMeshAgent _navMashAgent;
    //GamePlay参数
   [HideInInspector] public Transform player;
   private bool playerCanbeHit;
   [Header("属性")]
    [SerializeField]private int damage;


    [Header("丧尸视野"),SerializeField,TooltipAttribute("会根据ViewRadius自动调整")]
     FieldOfView outViewFied;
    [SerializeField,TooltipAttribute("会根据ViewRadius自动调整")]
    FieldOfView inViewFied;


    private Animator _Anim;
    private void Start()
    {
        _nowState = GetState(beginState);

        _navMashAgent = GetComponent<NavMeshAgent>();

        _Anim=GetComponent<Animator>();
        Switch2OutView();

        if (inViewFied.viewRadius>outViewFied.viewRadius)
        {
            FieldOfView temp=inViewFied;
            inViewFied=outViewFied;
            outViewFied=temp;
        }

    }

    private void Update()
    {
        _nowState.Excute();
        
            Discovery();
       
            InToAttack();
        
    }

    public void Discovery()
    {
        if (outViewFied.enabled==true&&outViewFied.visibleTargets.Count>0)
        {
            _nowState = _chaseState;
             _Anim.SetBool("Walk",true);
            player = outViewFied.visibleTargets[0];      
            Switch2InView(); 
            //notDiscovered=false;
        }
    }

    public void InToAttack()
    {
        if(inViewFied.enabled==true){
        if (inViewFied.visibleTargets.Count>0)
        {
            _nowState = _attackState;
             _Anim.SetBool("Attack",true);
            player = inViewFied.visibleTargets[0];      
            playerCanbeHit=true; 
        }else 
        {
            _nowState = _chaseState;
             _Anim.SetBool("Attack",false);
            playerCanbeHit=false; 

        }

        }
    }

   



    public IEnemyAIState GetState(EnemyAIState_Enum state)
    {
        switch (state)
        {
            case EnemyAIState_Enum.AttackState:
                return _attackState;
            case EnemyAIState_Enum.IdleState:
                return _idleState;
            case EnemyAIState_Enum.PatrolState:
                return _patrolState;
            case EnemyAIState_Enum.ChaseState:
                return _chaseState;
            default: return _nowState;
        }
    }


    public void Switch2OutView()
    {
        inViewFied.enabled = false;

        outViewFied.enabled = true;
    }

    public void Switch2InView()
    {
        outViewFied.enabled = false;
        inViewFied.enabled = true;
    }

    //TODO 角色死亡
    public void Die()
    {
        _navMashAgent.enabled=false;
        _Anim.SetTrigger("Die");
        Destroy(this);
    }

    public bool IsDie(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChaseTraget()
    {
        if(player!=null)
       { _navMashAgent.destination = player.transform.position;
        _navMashAgent.isStopped = false;}
    }

    public void ToAttack()
    {
        _navMashAgent.isStopped = true;
    }
    public void Attack()
    {
        if (playerCanbeHit)
        {
            PlayerController.Instance.TakeDamage(damage);
        }

    }


    public void PlayerDie()
    {
        _Anim.SetBool("Walk",false);
        Destroy(this);
    }
    private void OnEnable() {
        EventHandler.PlayerDieEvent+=PlayerDie;
    }

    private void OnDisable() {
        EventHandler.PlayerDieEvent-=PlayerDie;
        
    }
}
