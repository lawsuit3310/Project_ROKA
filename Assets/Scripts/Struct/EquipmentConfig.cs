using System;

[Serializable]
public struct EquipmentConfig
{
    public enum EquipmentType
    {
        //cqc 근접, bvr 원거리
        CQC, BVR
    }

    public int Type;
    public float AttackPoint;
    public float AttackCoolDown;
    public float ReloadCoolDown;
    public int MaxCapacity;
    public float CurrentCapacity;
    public float CurrentHitPoint;
    public string Name;
    public string Explanation;

    public void Reset()
    {
        
    }
}