using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HolyBook : SpellBase
{
    private List<GameObject> _booksObject;
    public override void Activate()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        while (Active)
        {
            var spellSettings = SettingsProvider.Get<SpellsSettings>().GetSpell(SpellType).SpellLevelSettings[GetLevel];
            for (int i = 0; i < spellSettings.Amount; i++)
            {
                var book = Instantiate(SpellPrefab, transform);
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

    public void FixedUpdate()
    {
        transform.rotation = new Quaternion(0, 10, 0, 0);
    }
}