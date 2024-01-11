using System.Collections;
using UnityEngine;

public class SwordSummon : SpellBase
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
        }
    }
}