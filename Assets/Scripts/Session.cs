using System;
using UnityEngine;

public class Session : ISession
{
    private float _sessionStartTime; 
    public float SessionTime;

    public uint SessionCount = 0;
    public void Dispose()
    {
        // TODO release managed resources here
        _sessionStartTime = 0;
        SessionTime = 0;
        SessionCount--;
        GC.SuppressFinalize(this);
    }

    public ISession StartSession()
    {
        _sessionStartTime = Time.deltaTime;
        SessionTime = _sessionStartTime;
        Debug.Log("Start Session");
        SessionCount++;
        return this;
    }

    public float GetSessionTime()
    {
        return SessionTime - _sessionStartTime;
    }
    
    
}