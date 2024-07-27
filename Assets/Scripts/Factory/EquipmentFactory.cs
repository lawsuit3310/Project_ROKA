using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class EquipmentFactory : IDisposable
{
    private Dictionary<int, EquipmentConfig> _equipmentDict;
    public EquipmentFactory()
    {
        var path = Application.dataPath + "/Database/EquipmentDB.csv";
        _equipmentDict = new Dictionary<int, EquipmentConfig>();
        
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
                {
                    //header 제거
                    sr.ReadLine();
                    string row;
                    while (( row = sr.ReadLine()) != null)
                    {
                        var _arr = row.Split(',');
                        
                        var config = new EquipmentConfig()
                        {
                            Type = Convert.ToInt32(_arr[1]),
                            AttackPoint = Convert.ToSingle(_arr[2]),
                            AttackCoolDown = Convert.ToSingle(_arr[3]),
                            ReloadCoolDown = Convert.ToSingle(_arr[4]),
                            MaxCapacity = Convert.ToInt32(_arr[5]),
                            Name = _arr[6],
                            Explanation = _arr[7]
                        };
                        
                        _equipmentDict.Add(
                            Convert.ToInt32(_arr[0]), config);
                    }
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Fatal Error During Loading DB!. " + e.Message);
        }
    }
    
    public Equipment CreateEquipment(int index, IController controller)
    {
        Equipment equip;
        switch (index)
        {
            case 0:
                equip = new FlintLock(_equipmentDict[index], controller);
                break;
            case 1:
                equip = new Equipment(_equipmentDict[index], controller);
                break;
            default:
                equip = new Equipment(_equipmentDict[index], controller);
                break;
        }
        return equip;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(_equipmentDict);
    }
}