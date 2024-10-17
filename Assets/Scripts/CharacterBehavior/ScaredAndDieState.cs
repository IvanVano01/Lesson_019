using UnityEngine;

public class ScaredAndDieState : CharacterState
{
    private IMovable _movable;
    private CharacterView _view;

    public ScaredAndDieState(EnemyMover mover, IMovable movable, CharacterView view) : base(mover)
    {
        _movable = movable;
        _view = view;
    }

    public override void EnterState()
    {
        base.EnterState();
        ScareAndDie();
    }

    private void ScareAndDie()
    {
        Transform objectDestroy = _movable.Transform;

        _view.PlayEffect(objectDestroy);

        GameObject.Destroy(objectDestroy.gameObject);
    }
}
