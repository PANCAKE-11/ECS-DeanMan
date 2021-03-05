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



    private void Start()
    {
        _nowState = GetState(beginState);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        _navMashAgent = GetComponent<NavMeshAgent>();
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
            _nowState = _attackState;
        else if (other.gameObject.CompareTag("Player") && outCollider.enabled == true
      && inCollider.enabled == false)//触发丧尸发现玩家 开始chase
        { _nowState = _chaseState; Switch2InCollider(); }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _nowState = _chaseState;

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
        Debug.Log("已死亡");
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
        _navMashAgent.destination = player.transform.position;
        _navMashAgent.isStopped = false;
    }

    public void Attack()
    {
        _navMashAgent.isStopped = true;
    }

}
