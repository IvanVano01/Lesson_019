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
                    Mover mover = new NoMove();
                    return new NoMoveState(mover);
                }

            case EnemyDefaultState.EnemyMovePointByPoint:
                {
                    Mover mover = new MovePointByPoint(enemyPrefab, targetPoints);
                    return new MovePointByPointState(mover);
                }

            case EnemyDefaultState.EnemyMoveRandomDirect:
                {
                    Mover mover = new MoveRandomDirect(enemyPrefab, target);
                    return new MoveRandomDirectState(mover);
                }

            default:
                Debug.LogError($" ��� ������ ���� ��������� ��� ����� {enemyDefaultState} ");
                return null;
        }
    }

    private CharacterState GetCharacterReactionState(EnemyReactionToPlayer enemyReactionToPlayer, Enemy enemyPrefab, Transform playerTransform)
    {
        switch (enemyReactionToPlayer)
        {
            case EnemyReactionToPlayer.EnemyFollowPlayer:
                {
                    Mover mover = new MoveToTarget(playerTransform, enemyPrefab);
                    return new FollowTargetState(mover);
                }

            case EnemyReactionToPlayer.EnemyRunAway:
                {
                    Mover mover = new MoveRunAway(enemyPrefab, playerTransform.position);
                    return new RunAwayState(mover);
                }

            case EnemyReactionToPlayer.EnemyScaredAndDie:
                {
                    Mover mover = new NoMove();
                    CharacterView characterView = _enemyPrefab.View;
                    return new ScaredAndDieState(mover, enemyPrefab, characterView);
                }

            default:

                Debug.LogError($" ��� ������ ���� ��������� ��� ����� {enemyReactionToPlayer} ");
                return null;
        }
    }
}
