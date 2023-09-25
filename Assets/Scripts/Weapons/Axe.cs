using System.Collections.Generic;

public class Axe : IWeapon
{
    public int PoolLimit { get; set; }
    public int Amount { get; set; }
    public float MaxLevel { get; set; }
    public float KnockBack { get; set; }
    public bool BlockByWalls { get; set; }
    public List<IWeaponEffect> WeaponEffects { get; set; }
    public float BaseAttackSpeed { get; set; }
    public float BaseAttackDamage { get; set; }
    public float BaseAttackDuration { get; set; }
    public float BaseAttackCooldown { get; set; }
    public float BaseHitBoxDelay { get; set; }

    public void Attack()
    {
    }
}