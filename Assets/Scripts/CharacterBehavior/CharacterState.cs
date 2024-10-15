public abstract class CharacterState : IBehavior
{
    protected bool _isRunningState;

    protected Mover _mover;

    protected CharacterState(Mover mover)
    {
        _mover = mover;
    }

    public virtual void EnterState()
    {
        _isRunningState = true;
        SetMover(_mover);
    }

    public virtual void ExitState()
    {
        _isRunningState = false;
    }

    public virtual void Update()
    {
        if (_isRunningState == false)
            return;
        _mover.Update();
    }

    public virtual void SetMover(Mover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }
}
