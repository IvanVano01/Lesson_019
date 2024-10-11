using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyDefaultState _enemyDefaultState;
    [SerializeField] private EnemyReactionToPlayer _enemyReactionToPlayer;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _targetPoints;

    //[SerializeField] private EnemyBehaviorController _behaviorController;

    [SerializeField] private Player _player;

    private void Awake()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        CreateEnemy(_enemyPrefab, _enemyDefaultState);
    }

    private void CreateEnemy(Enemy enemyPrefab, EnemyDefaultState enemyMoveType)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, null);

        enemy.Initialize(_targetPoints, _player.transform, _enemyDefaultState, _enemyReactionToPlayer);
        
        enemy.GetComponent<EnemyBehaviorController>().Initialize(enemy);
    }
}
