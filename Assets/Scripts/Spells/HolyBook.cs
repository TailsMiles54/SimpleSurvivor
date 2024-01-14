using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public class HolyBook : SpellBase
    {
        private List<GameObject> _booksObject;
        public override void Activate()
        {
            SetActive(true);
            StartCoroutine(Attack());
        }

        public IEnumerator Attack()
        {
            while (Active)
            {
                for (int i = 0; i < GetLevelSetting.Amount; i++)
                {
                    var book = Instantiate(SpellPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                    _booksObject.Add(book);
                }
                yield return new WaitForSeconds(GetLevelSetting.Duration);
                foreach (var bookObject in _booksObject)
                {
                    Destroy(bookObject);
                }
                yield return new WaitForSeconds(GetLevelSetting.Cooldown);
            }
        }
    
        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 vec = Random.insideUnitCircle.normalized * GetLevelSetting.Radius;
        
            var pos = new Vector3(vec.x, vec.z, vec.y);
        
            return transform.position + pos;
        }

        public void FixedUpdate()
        {
            transform.rotation = new Quaternion(0, 10, 0, 0);
        }
    }
}