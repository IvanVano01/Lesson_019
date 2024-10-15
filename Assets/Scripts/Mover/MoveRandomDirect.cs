using UnityEngine;

public class MoveRandomDirect : Mover
{
    private float _maxTime = 1f;
    private float _currentTime;
    private const float MinDistance = 0.05f;
    private float _maxdistanceFromBorder = 15f;

    private IMovable _movable;
    private Transform _targetBound;

    private Vector3 _direction;

    public MoveRandomDirect(IMovable movable, Transform targetBound)
    {
        _movable = movable;
        _targetBound = targetBound;
        _currentTime = _maxTime;
        _direction = RandomDirection();
    }

    public override void StartMove() => _isMoving = true;

    public override void StopMove() => _isMoving = false;

    public override void Update()
    {
        if (_isMoving == false)
            return;

        _currentTime -= Time.deltaTime;

        if (_currentTime < 0)
        {
            _currentTime = _maxTime;
            SwichDirection();
        }

        Move(_movable.Transform, _direction, _movable.Speed);

        if (_direction.magnitude > MinDistance)
            ProcessRotate(_movable.Transform, _direction, _movable.SpeedRotion);
    }

    private Vector3 RandomDirection()
    {
        float axisX = Random.Range(-1, 1);
        float axisZ = Random.Range(-1, 1);

        Vector3 randomDirection = new Vector3(axisX, 0, axisZ);

        if (IsOutBounds())
            return DirectionToTargetBound(_targetBound.position);

        return randomDirection;
    }

    private void SwichDirection()
    {
        _direction = RandomDirection();
    }

    private bool IsOutBounds()
    {
        Vector3 vectorlength = _targetBound.position - _movable.Transform.position;

        if (vectorlength.magnitude > _maxdistanceFromBorder)
            return true;

        return false;
    }

    private Vector3 DirectionToTargetBound(Vector3 targetBound)
    {
        Vector3 direction = targetBound - _movable.Transform.position;
        Vector3 directionNormalized = direction.normalized;
        return directionNormalized;
    }
}
