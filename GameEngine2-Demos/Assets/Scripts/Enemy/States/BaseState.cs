public abstract class BaseState 
{
    public Enemy enemy;
    // instance of statemachine class
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();

}