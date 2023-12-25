using UnityEngine;

public class MagicShot : SpellBase
{
    public override void Attack()
    {
        var test = Instantiate(SpellPrefab, transform, true);
        var test1 = test.AddComponent<Rigidbody>();
        test1.AddForce(new Vector3(1,1));
    }
}