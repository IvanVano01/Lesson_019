using System.Collections.Generic;
using UnityEngine;

public class MovePointByPointState : CharacterState
{
    private IMovable _movable;
    private Queue<Vector3> _targets;

    private Vector3 _currentTarget;

    public MovePointByPointState(EnemyMover mover, List<Transform> targets, IMovable movable) : base(mover)
    {
        _mover = mover;
        _movable = movable;

        List<Vector3> targetPoints = new List<Vector3>();

        foreach (Transform point in targets)
            targetPoints.Add(point.position);

        _targets = new Queue<Vector3>(targetPoints);

        _currentTarget = _targets.Dequeue();
    }

    public override void Update()
    {
        base.Update();

        Vector3 directionToTarget = _currentTarget - _movable.Transform.position;

        if (directionToTarget.sqrMagnitude < MinDistance)
        {
            SwitchTarget();
        }

        _mover.MoveToDirection(directionToTarget);
    }

    private void SwitchTarget()
    {
        _targets.Enqueue(_currentTarget);
        _currentTarget = _targets.Dequeue();
    }
}
