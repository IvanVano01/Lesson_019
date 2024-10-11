using UnityEngine;

public interface IMovable 
{
    Transform Transform { get; }
    public float Speed { get; }
    public float SpeedRotion { get; }
    public EnemyView EnemyView { get; }
}
