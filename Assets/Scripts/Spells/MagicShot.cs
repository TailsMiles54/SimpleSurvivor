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
            yield return new WaitForSeconds(1f);
            var test = Instantiate(SpellPrefab, transform);
            test.transform.parent = null;
            var test1 = test.GetComponent<Rigidbody>();
            var test2 = test.GetComponent<SpellColider>();
            test2.Setup(Parent, 3f);
            
            var direction = Random.insideUnitCircle.normalized;
            var vector = direction * 10;
            test1.AddForce(new Vector3(vector.x, 0, vector.y), ForceMode.VelocityChange);
            Debug.Log(vector);
        }
    }
}