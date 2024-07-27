using UnityEngine;

public class Equipment
{
    public GameObject EquipObject;
    public EquipmentConfig Config;
    public IController Controller;
    public Equipment(EquipmentConfig config, IController controller )
    {
        Config = config;
        Controller = controller;
        
    }

    public virtual void Fire()
    {
        Debug.Log("발사");
    }
}