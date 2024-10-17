using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyDefaultState _enemyDefaultState;
    [SerializeField] private EnemyReactionToPlayer _enemyReactionToPlayer;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _targetPoints;

    [SerializeField] private Transform _target;

    private void Awake()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, null);

        CharacterState characterDefaultState = GetCharacterDefaultState(_enemyDefaultState, enemy, _targetPoints, _target);
        CharacterState characterReactionState = GetCharacterReactionState(_enemyReactionToPlayer, enemy, _target);

        enemy.Initialize(characterDefaultState, characterReactionState, _target);

    }

    private CharacterState GetCharacterDefaultState(EnemyDefaultState enemyDefaultState, Enemy enemyPrefab, List<Transform> targetPoints, Transform target)
    {
        switch (enemyDefaultState)
        {
            case EnemyDefaultState.EnemyNoMove:
                {
                    EnemyMover mover = new EnemyMover(enemyPrefab.Transform, enemyPrefab.Speed, enemyPrefab.SpeedRotion);
                    return new NoMoveState(mover);
                }

            case EnemyDefaultState.EnemyMovePointByPoint:
                {
                    EnemyMover mover = new EnemyMover(enemyPrefab.Transform,enemyPrefab.Speed,enemyPrefab.SpeedRotion);
                    return new MovePointByPointState(mover,targetPoints,enemyPrefab);
                }

            case EnemyDefaultState.EnemyMoveRandomDirect:
                {
                    EnemyMover mover = new EnemyMover(enemyPrefab.Transform, enemyPrefab.Speed, enemyPrefab.SpeedRotion);
                    return new MoveRandomDirectState(mover, target, enemyPrefab);
                }

            default:
                Debug.LogError($" Нет такого типа состояния для врага {enemyDefaultState} ");
                return null;
        }
    }

    private CharacterState GetCharacterReactionState(EnemyReactionToPlayer enemyReactionToPlayer, Enemy enemyPrefab, Transform playerTransform)
    {
        switch (enemyReactionToPlayer)
        {
            case EnemyReactionToPlayer.EnemyFollowPlayer:
                {
                    EnemyMover mover = new EnemyMover(enemyPrefab.Transform, enemyPrefab.Speed, enemyPrefab.SpeedRotion);
                    return new FollowTargetState(mover, playerTransform, enemyPrefab);
                }

            case EnemyReactionToPlayer.EnemyRunAway:
                {
                    EnemyMover mover = new EnemyMover(enemyPrefab.Transform, enemyPrefab.Speed, enemyPrefab.SpeedRotion);
                    return new RunAwayState(mover, playerTransform.transform, enemyPrefab);
                }

            case EnemyReactionToPlayer.EnemyScaredAndDie:
                {                   
                    CharacterView characterView = _enemyPrefab.View;
                    return new ScaredAndDieState(null, enemyPrefab, characterView);
                }

            default:

                Debug.LogError($" Нет такого типа состояния для врага {enemyReactionToPlayer} ");
                return null;
        }
    }
}
