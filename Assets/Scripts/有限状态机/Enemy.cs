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
    //GamePlay参数
    public Transform player;
    private NavMeshAgent _navMashAgent;

    [SerializeField] BoxCollider outCollider;
    [SerializeField] BoxCollider inCollider;


    private Animator _Anim;
    private void Start()
    {
        _nowState = GetState(beginState);

        _navMashAgent = GetComponent<NavMeshAgent>();

        _Anim=GetComponent<Animator>();
        Switch2OutCollider();

    }

    private void Update()
    {
        _nowState.Excute();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && outCollider.enabled == false
        && inCollider.enabled == true)
           { _nowState = _attackState;
             _Anim.SetBool("Attack",true);
             }
        else if (other.gameObject.CompareTag("Player") && outCollider.enabled == true
      && inCollider.enabled == false)//触发丧尸发现玩家 开始chase
        { _nowState = _chaseState;
             Switch2InCollider(); 
             _Anim.SetBool("Walk",true);
            player = other.transform;


        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
       {     _nowState = _chaseState;
             _Anim.SetBool("Attack",false);
            
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


    public void Switch2OutCollider()
    {
        inCollider.enabled = false;

        outCollider.enabled = true;
    }

    public void Switch2InCollider()
    {
        outCollider.enabled = false;
        inCollider.enabled = true;
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
        print("ATTACK!!!");
    }
}
