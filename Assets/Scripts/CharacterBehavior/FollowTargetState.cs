using UnityEngine;

public class FollowTargetState : CharacterState
{
    private Transform _target;

    private IMovable _movable;

    public FollowTargetState(EnemyMover mover, Transform targetFollow, IMovable movable) : base(mover)
    {
        _mover = mover;
        _movable = movable;
        _target = targetFollow;
    }

    public override void Update()
    {
        Vector3 directionToTarget = _target.position - _movable.Transform.position;

        _mover.MoveToDirection(directionToTarget);
    }
}
