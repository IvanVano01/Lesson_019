using UnityEngine;

public class Enemy : MonoBehaviour, IMovable
{
    [SerializeField] private CharacterView _enemyView;

    private CharacterState _defaultState;
    private CharacterState _reactionState;
    private CharacterState _currentState;

    private Transform _targetAggr;

    private float _maxDistanceToTarget = 5f;
    private float _speed = 4;
    private float _speedRotion = 900f;

    public void Initialize(CharacterState defaultState, CharacterState reactionState, Transform targetAggr)
    {
        _defaultState = defaultState;
        _reactionState = reactionState;
        _targetAggr = targetAggr;

        SetCurrentState(_defaultState);
    }

    public float Speed => _speed;
    public float SpeedRotion => _speedRotion;
    public CharacterView View => _enemyView;
    public Transform Transform => transform;

    private void Update()
    {
        SwitchState(_targetAggr.position);

        _currentState.Update();
    }

    private void SwitchState(Vector3 target)
    {
        Vector3 distance = target - this.transform.position;

        if (Mathf.Abs(distance.magnitude) > _maxDistanceToTarget)
        {
            SetCurrentState(_defaultState);
            Debug.DrawLine(this.transform.position, target, Color.green);

        }
        else
        {
            SetCurrentState(_reactionState);
            Debug.DrawLine(this.transform.position, target, Color.red);
        }
    }

    private void SetCurrentState(CharacterState state)
    {
        _currentState?.ExitState();
        _currentState = state;
        _currentState.EnterState();
    }

}
