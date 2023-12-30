using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public Player Parent;
    [SerializeField] private int _level;
    public bool Active { get; private set; }
    [field: SerializeField] public GameObject SpellPrefab { get; private set; }

    private void LevelUp()
    {
        _level++;
        
        if (_level != 0)
        {
            Activate();
        }
    }

    private void FixedUpdate()
    {
        if (_level != 0 && !Active)
        {
            Active = true;
            Activate();
        }
    }

    public void SetActive(bool state)
    {
        Active = state;
    }

    public int GetLevel => _level;

    public virtual void Activate() { }
}
