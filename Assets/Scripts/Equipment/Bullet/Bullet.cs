using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public CharacterController shooter;
    
    //총알이 충돌했는지 여부를 저장
    private bool _flag = false;

    public void Launch(Equipment equip)
    {
        if(equip.Controller is CharacterController)
        {
            shooter ??= (CharacterController)equip.Controller;
            StartCoroutine(GoForward());
        }
    }

    IEnumerator GoForward()
    {
        var dir = (shooter.Sight.point - shooter.transform.position).normalized;
        for (var i =0; i < 200 || _flag; i++)
        {
            var displacement = dir * (Time.deltaTime * 150);
            this.transform.Translate(displacement, Space.World);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}