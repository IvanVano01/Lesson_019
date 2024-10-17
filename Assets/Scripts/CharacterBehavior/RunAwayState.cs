using UnityEngine;

public class RunAwayState : CharacterState
{
    private IMovable _movable;
    private Transform _targetRunAway;

    public RunAwayState(EnemyMover mover, Transform targetRunAway, IMovable movable) : base(mover)
    {
        _mover = mover;
        _movable = movable;
        _targetRunAway = targetRunAway;
    }

    public override void Update()
    {
        Vector3 directionAwayTarget = _movable.Transform.position - _targetRunAway.position;

        _mover.MoveToDirection(directionAwayTarget);
    }
}
