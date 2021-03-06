public   interface  IEnemyAIState 
{
    public void Excute();

}

public interface IWeapon 
{
    public abstract float ShootCD{get;set;}
    public abstract void fire();
}
