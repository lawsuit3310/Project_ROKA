using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct MovableConfig
{
    public static float MovSpd = 2;
    public float CurrentMovSpd;
    //이동 방향
    public Vector3 CurrentTargetPosition;
    public Vector3 LookPoint;

    public void Reset()
    {
        CurrentMovSpd = MovSpd;
        CurrentTargetPosition = Vector3.zero;
        LookPoint = Vector3.zero;
    }   
}