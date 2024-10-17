using UnityEngine;

public class EnemyMover
{
    private Transform _movableTransform;
    private float _movableSpeed;
    private float _movableSpeedRotion;
    private const float DeadZone = 0.05f;    

    public EnemyMover(Transform movableTransform, float movableSpeed, float movableSpeedRotion)
    {
        _movableTransform = movableTransform;
        _movableSpeed = movableSpeed;
        _movableSpeedRotion = movableSpeedRotion;
    }

    public void MoveToDirection(Vector3 direction)
    {        
        ProcessMove(_movableTransform, direction, _movableSpeed);

        if (direction.magnitude > DeadZone)
            ProcessRotate(_movableTransform, direction, _movableSpeedRotion);
    }    

    public void ProcessMove(Transform transform, Vector3 direction, float speed)
    {
        direction.y = 0;
        Vector3 NormalizeDirection = direction.normalized;

        transform.Translate(NormalizeDirection * speed * Time.deltaTime, Space.World);
    }

    public void ProcessRotate(Transform transform, Vector3 direction, float speedRotation)
    {
        Quaternion lookrotation = Quaternion.LookRotation(direction.normalized);

        Quaternion lookrotationFreezeAxisXZ = Quaternion.Euler(0, lookrotation.eulerAngles.y, 0);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookrotationFreezeAxisXZ, speedRotation * Time.deltaTime);
    }
}
