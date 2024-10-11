using UnityEngine;

public class MoveRunAway : Mover
{
    private IMovable _movable;
    private Vector3 _targetRunAway;

    public MoveRunAway(IMovable movable, Vector3 targetRunAway)
    {
        _movable = movable;
        _targetRunAway = targetRunAway;
    }

    public override void StartMove() => _isMoving = true;

    public override void StopMove() => _isMoving = false;

    public override void Update()
    {
        if (_isMoving == false)
            return;

        Vector3 directionAwayTarget = _movable.Transform.position - _targetRunAway;

        Move(_movable.Transform, directionAwayTarget, _movable.Speed);
        ProcessRotate(_movable.Transform, directionAwayTarget, _movable.SpeedRotion);
    }
}
