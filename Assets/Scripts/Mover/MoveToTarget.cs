using UnityEngine;

public class MoveToTarget : Mover
{
    private Vector3 _target;

    private IMovable _movable;

    public MoveToTarget(Vector3 target, IMovable movable)
    {
        _target = target;
        _movable = movable;
    }

    public override void StartMove() => _isMoving = true;

    public override void StopMove() => _isMoving = false;

    public override void Update()
    {
        if (_isMoving == false)
            return;

        Vector3 directionToTarget = _target - _movable.Transform.position;

        Move(_movable.Transform, directionToTarget, _movable.Speed);
        ProcessRotate(_movable.Transform, directionToTarget, _movable.SpeedRotion);
    }
}
