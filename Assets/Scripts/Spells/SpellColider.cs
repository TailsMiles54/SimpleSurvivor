using Enemies;
using UnityEngine;

public class SpellColider : MonoBehaviour
{
    private Player _parent;
    private float _damage;

    public void Setup(Player userId, float damage)
    {
        _parent = userId;
        _damage = damage;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseEnemy baseEnemy))
        {
            Debug.Log("damage");
            if (baseEnemy.EnemyData.TakeDamage(10))
            {
                _parent.AddXp(10);
            }
            Destroy(gameObject);
        }
    }
}
