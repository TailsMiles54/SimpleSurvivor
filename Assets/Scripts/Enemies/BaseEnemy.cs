using System;
using UnityEngine;

namespace Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public EnemyData EnemyData;

        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class EnemyData
    {
        [field: SerializeField]public EnemyTypes EnemyType { get; private set; }
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public float Health { get; private set; }
        public BaseEnemy Parent;

        public EnemyData(EnemyTypes enemyType,float speed,float damage,float health, BaseEnemy parent)
        {
            EnemyType = enemyType;
            Speed = speed;
            Damage = damage;
            Health = health;
            Parent = parent;
        }
        
        public bool TakeDamage(float damage)
        {
            Debug.Log(Health);
            Health -= damage;

            if (Health <= 0)
            {
                Parent.DestroyMe();
                return true;
            }

            Debug.Log(Health);
            return false;
        } 
    }
}