using UnityEngine;

public abstract class Mover
{
    protected bool _isMoving;

    public abstract void StartMove();
    public abstract void StopMove();
    public abstract void Update();

    public void Move(Transform transform, Vector3 direction, float speed)
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
