public interface IWeaponEffect
{
    public void ApplyEffect();
}

public class FireEffect : IWeaponEffect
{
    public int Duration;
    
    public void ApplyEffect()
    {
    }
} 