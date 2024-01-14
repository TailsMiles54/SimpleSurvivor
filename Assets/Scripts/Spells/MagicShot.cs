using System.Collections;
using UnityEngine;

public class MagicShot : SpellBase
{
    public override void Activate()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        while (Active)
        {
            var test = Instantiate(SpellPrefab, transform);
            test.transform.parent = null;
            var test1 = test.GetComponent<Rigidbody>();
            var test2 = test.GetComponent<SpellColider>();
            test2.Setup(Parent, GetLevelSetting.BaseDamage, GetLevelSetting.Pierce);
            
            var direction = Random.insideUnitCircle.normalized;
            var vector = direction * GetLevelSetting.Speed;
            test1.AddForce(new Vector3(vector.x, 0, vector.y), ForceMode.VelocityChange);
            Debug.Log(vector);
            yield return new WaitForSeconds(GetLevelSetting.Cooldown);
        }
    }
}