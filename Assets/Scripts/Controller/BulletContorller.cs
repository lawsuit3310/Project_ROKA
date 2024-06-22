using System;
using System.Collections;
using UnityEngine;

public class BulletContorller : MonoBehaviour
{
    
    private Vector3 _direction = Vector3.forward;
    
    public static PlayerController Player;
    
    public float BulletSpd = 1f;

    private void OnEnable()
    {
        StartCoroutine(StartMove());
        //Player = !Player ? FindObjectOfType<PlayerController>() : Player;
        //Debug.Log(Player.transform.position);
    }

    IEnumerator StartMove()
    {
        for (int i = 0; i < 1000; i++)
        {
            transform.Translate(_direction * BulletSpd);
            yield return null;
        }
        Debug.Log("최대 사정거리 도달");
        Destroy(this.gameObject);
    }
}