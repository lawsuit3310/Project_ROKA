using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct MovableConfig
{
    public static float DefaultMovSpd = 5;
    public float MovSpd;
    public float CurrentMovSpd;
    
    //이동 방향 - 해당 좌표는 움직이는 주체를 중심으로 얼마나 떨어진 위치를 향해 이동하는 지를 나타냄.
    public Vector3 CurrentTargetPosition;
    public Vector3 TargetPosition;

    public void Reset()
    {
        ResetSpd();
        CurrentTargetPosition = Vector3.zero;
        TargetPosition = Vector3.zero;
    }

    public void ResetSpd()
    {
        CurrentMovSpd = MovSpd;
    }
    
    public void Stop()
    {
        CurrentMovSpd = 0;
        //CurrentTargetPosition = Vector3.zero;
        //TargetPosition = Vector3.zero;
    }   
}