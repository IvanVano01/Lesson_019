public abstract class CharacterState : IBehavior
{
    protected const float MinDistance = 0.05f;

    protected EnemyMover _mover;

    protected bool _isRunningState;

    protected CharacterState(EnemyMover mover)
    {
        _mover = mover;
    }

    public virtual void EnterState()
    {
        _isRunningState = true;        
    }

    public virtual void ExitState()
    {
        _isRunningState = false;
    }

    public virtual void Update()
    {
        if (_isRunningState == false)
            return;        
    }    
}
