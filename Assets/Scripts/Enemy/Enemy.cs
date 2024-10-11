using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable
{
    [SerializeField] private EnemyView _enemyView;

    private EnemyDefaultState _defaultState;
    private EnemyReactionToPlayer _reactionToPlayer;
    private EnemyState _currentState;

    private List<Transform> _targetPoints;
    private Transform _target;

    private float _maxDistanceToTarget = 5f;
    private float _speed = 4;
    private float _speedRotion = 900f;

    private Mover _mover;

    public void Initialize(List<Transform> targetsArray, Transform target,EnemyDefaultState enemyDefaultState,EnemyReactionToPlayer enemyReactionToPlayer)
    {
        _target = target;

        _targetPoints = new List<Transform>();
        foreach (Transform targetPoint in targetsArray)
        {
            _targetPoints.Add(targetPoint);
        }

        _defaultState = enemyDefaultState;
        _reactionToPlayer = enemyReactionToPlayer;
    }

    public Transform Transform => transform;
    public float Speed => _speed;
    public float SpeedRotion => _speedRotion;

    public List<Transform> TargetPoints => _targetPoints;
    public Transform Target => _target;
    public EnemyView EnemyView => _enemyView;    

    private void Update()
    {
        SwitchState(_target.position);

        if (_mover == null)
            return;

        _mover.Update();
    }

    public void SetMover(Mover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    public void SetState(EnemyState state)
    {
        if (_currentState != state)
        {
            _currentState = state;
        }
    }

    public EnemyDefaultState DefaultState => _defaultState;
    public EnemyReactionToPlayer ReactionToPlayer => _reactionToPlayer;
    public EnemyState CurrentState => _currentState;

    public void SetCurrentState(EnemyState enemyState) => _currentState = enemyState;

    private void SwitchState(Vector3 target)
    {
        Vector3 distance = target - this.transform.position;

        if (Mathf.Abs(distance.magnitude) < _maxDistanceToTarget)
        {
            SetState(EnemyState.ReactionToPlayer);
            Debug.DrawLine(this.transform.position, target, Color.green);
        }
        else
        {
            SetState(EnemyState.DefaultState);
            Debug.DrawLine(this.transform.position, target, Color.red);
        }
    }
}
