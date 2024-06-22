public struct FightableConfig
{
    public static float HitPoint = 100;
    public static float AttackPoint = 1;
    public static float Range = 4;
    public static float AttackCoolDown = 0.2f;

    public float CurrentHitPoint;

    public void Reset()
    {
        CurrentHitPoint = HitPoint;
    }
}