using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    [SerializeField] private int _level;
    [field: SerializeField] public GameObject SpellPrefab { get; private set; }

    private void LevelUp()
    {
        _level++;
    }

    public int GetLevel => _level;

    public void FixedUpdate()
    {
        if (_level != 0)
        {
            Attack();
        }
    }

    public virtual void Attack() { }
}
