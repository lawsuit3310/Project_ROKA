using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Session _onPlaySession;
    public Session OnPlaySession => _onPlaySession;

    //실제 게임 프로세스를 시작하는 메소드
    
    void StartSession()
    {
        EndSession();
        _onPlaySession = (Session) new Session().StartSession();
    }
    void EndSession()
    {
        if (_onPlaySession != null)
        {
            Debug.Log($"current session close in : {_onPlaySession.GetSessionTime()}s");
            _onPlaySession.Dispose();
            _onPlaySession = null;
        }
    }
    private void FixedUpdate()
    {
        if (_onPlaySession != null)
        {
            _onPlaySession.SessionTime += Time.deltaTime;
        }
    }
    private void OnGUI()
    {
        if (_onPlaySession != null)
        {
            if (GUILayout.Button("Session Close"))
                EndSession();
        }
        else
        {
            if (GUILayout.Button("new Session Open"))
                StartSession();
        }
    }
}
