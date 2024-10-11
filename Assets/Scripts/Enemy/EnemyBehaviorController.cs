using UnityEngine;

public class EnemyBehaviorController : MonoBehaviour
{
    private Enemy _enemy;

    private Mover _noMove;
    private Mover _moverPointByPoint;
    private Mover _moveRandonDirect;    

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void Start()
    {
        _noMove = new NoMove();
        _moverPointByPoint = new MovePointByPoint(_enemy, _enemy.TargetPoints);
        _moveRandonDirect = new MoveRandomDirect(_enemy,_enemy.Target.position);
    }

    private void Update()
    {
        switch (_enemy.CurrentState)
        {
            case EnemyState.DefaultState:
                {
                    Mover mover = GetDefaultStateMover(_enemy.DefaultState);
                    _enemy.SetMover(mover);
                    return;
                }

            case EnemyState.ReactionToPlayer:
                {
                    Mover mover = GetReactionToPlayerMover(_enemy.ReactionToPlayer, _enemy, _enemy.Target.position);
                    _enemy.SetMover(mover);
                    return;
                }
            default:
                Debug.LogError($" Нет такого типа состояния для врага {_enemy.CurrentState} ");
                return;

        }
    }

    private Mover GetDefaultStateMover(EnemyDefaultState enemyDefaultState)
    {
        switch (enemyDefaultState)
        {
            case EnemyDefaultState.EnemyNoMove:
                {
                    Mover noMover = _noMove;
                    return noMover;
                }

            case EnemyDefaultState.EnemyMovePointByPoint:
                {
                    Mover pointByPoint = _moverPointByPoint;
                    return pointByPoint;
                }

            case EnemyDefaultState.EnemyMoveRandomDirect:
                {
                    Mover moveRandomDirect = _moveRandonDirect;
                    return moveRandomDirect;
                }

            default:
                Debug.LogError($" Нет такого типа передвижения для врага {enemyDefaultState} ");
                return null;
        }
    }

    private Mover GetReactionToPlayerMover(EnemyReactionToPlayer enemyReactionToPlayer, IMovable enemy, Vector3 target)
    {
        switch (enemyReactionToPlayer)
        {
            case EnemyReactionToPlayer.EnemyRunAway:
                {
                    Mover enemyRunAway = new MoveRunAway(enemy, target);
                    return enemyRunAway;
                }

            case EnemyReactionToPlayer.EnemyFollowPlayer:
                {
                    Mover enemyFollowPlayer = new MoveToTarget(target, enemy);
                    return enemyFollowPlayer;
                }

            case EnemyReactionToPlayer.EnemyScaredAndDie:
                {
                    enemy.EnemyView.PlayEffect(_enemy.transform);
                    Mover enemyScaredAndDie = new MoveScaredAndDie(enemy);
                    return enemyScaredAndDie;
                }

            default:
                Debug.LogError($" Нет такого типа передвижения для врага {enemyReactionToPlayer} ");
                return null;
        }
    }
}
