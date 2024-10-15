using UnityEngine;

public class MoveToTarget : Mover
{
    private Transform _target;
    private const float MinDistance = 0.05f;

    private IMovable _movable;

    public MoveToTarget(Transform target, IMovable movable)
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

        Vector3 directionToTarget = _target.position - _movable.Transform.position;
        Move(_movable.Transform, directionToTarget, _movable.Speed);

        if (directionToTarget.magnitude > MinDistance)
            ProcessRotate(_movable.Transform, directionToTarget, _movable.SpeedRotion);
    }
}
