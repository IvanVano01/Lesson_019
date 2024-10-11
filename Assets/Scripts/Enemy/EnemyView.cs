using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleEffect;

    public void PlayEffect(Transform transform)
    {
        if (_particleEffect != null)
        {
            ParticleSystem particleeEffect = Instantiate(_particleEffect, transform.position, Quaternion.identity, null);
        }
        else
        {
            Debug.LogError($" Отсутствует ссылка на эффект {_particleEffect}");
        }
    }
}
