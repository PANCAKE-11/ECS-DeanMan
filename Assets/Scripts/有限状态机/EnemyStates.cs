
using UnityEngine;

///追捕状态
public class ChaseState : IEnemyAIState
{

    Enemy _enemy;
    public ChaseState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Excute()
    {
        _enemy.ChaseTraget();
    }
}


///巡逻状态
public class PatrolState : IEnemyAIState
{
    Enemy _enemy;


    public PatrolState(Enemy enemy)
    {
        _enemy = enemy;

    }

    public void Excute()
    {
     //   throw new System.NotImplementedException();
    }
}
///闲置状态
public class IdleState : IEnemyAIState
{
    Enemy _enemy;

    public IdleState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Excute()
    {
        //throw new System.NotImplementedException();
    }
}
///攻击状态
public class AttackState : IEnemyAIState
{
    Enemy _enemy;


    public AttackState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Excute()
    {
      _enemy.ToAttack();
    }
}