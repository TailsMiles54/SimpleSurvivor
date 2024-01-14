using Enemies;
using UnityEngine;

public class SpellColider : MonoBehaviour
{
    private Player _parent;
    private float _damage;
    
    private int _pierce; //Колво врагов

    public void Setup(Player userId, float damage, int pierce)
    {
        _parent = userId;
        _damage = damage;
        _pierce = pierce;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseEnemy baseEnemy))
        {
            _pierce--;
            if (baseEnemy.EnemyData.TakeDamage(_damage))
            {
                _parent.AddXp(10);
            }
            
            if(_pierce <= 0)
                Destroy(gameObject);
        }
    }
}
