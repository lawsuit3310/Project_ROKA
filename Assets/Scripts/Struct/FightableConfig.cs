using System;

[Serializable]
public struct FightableConfig
{
    public float HitPoint;
    public float CurrentHitPoint;

    public void Reset()
    {
        CurrentHitPoint = HitPoint;
    }

    public bool ModifyHealth(float amount)
    {
        return (CurrentHitPoint += amount) > 0;
    }
}