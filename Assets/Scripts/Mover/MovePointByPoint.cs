using System.Collections.Generic;
using UnityEngine;

public class MovePointByPoint : Mover
{
    private const float MinDistance = 0.05f;

    private IMovable _movable;
    private Queue<Vector3> _targets;

    private Vector3 _currentTarget;

    public MovePointByPoint(IMovable movable, List<Transform> targets)
    {
        _movable = movable;

        List<Vector3> targetPoints = new List<Vector3>();

        foreach (Transform point in targets)
            targetPoints.Add(point.position);

        _targets = new Queue<Vector3>(targetPoints);

        _currentTarget = _targets.Dequeue();
    }

    public override void StartMove() => _isMoving = true;

    public override void StopMove() => _isMoving = false;

    public override void Update()
    {
        if (_isMoving == false)
            return;

        Vector3 directionToTarget = _currentTarget - _movable.Transform.position;

        if (directionToTarget.sqrMagnitude < MinDistance)
        {
            SwitchTarget();
        }

        Move(_movable.Transform, directionToTarget, _movable.Speed);

        if (directionToTarget.sqrMagnitude > MinDistance)
            ProcessRotate(_movable.Transform, directionToTarget, _movable.SpeedRotion);
    }

    private void SwitchTarget()
    {
        _targets.Enqueue(_currentTarget);
        _currentTarget = _targets.Dequeue();
    }
}
