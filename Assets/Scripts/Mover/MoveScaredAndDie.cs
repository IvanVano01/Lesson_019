using UnityEngine;

public class MoveScaredAndDie : Mover
{
    private IMovable _movable;

    public MoveScaredAndDie(IMovable movable)
    {
        _movable = movable;
    }

    public override void StartMove()
    {
        ScareAndDie();
    }

    public override void StopMove()
    {

    }

    public override void Update()
    {

    }

    private void ScareAndDie()
    {
        Transform objectDestroy = _movable.Transform;
        GameObject.Destroy(objectDestroy.gameObject);
    }
}
