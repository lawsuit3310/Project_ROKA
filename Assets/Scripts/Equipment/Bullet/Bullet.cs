using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public CharacterController shooter;
    private Vector3 _dir = Vector3.zero;
    
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
        RaycastHit hit = new RaycastHit();
        _dir = (shooter.sight.point - shooter.transform.position).normalized;
        for (var i =0; i < 200 || _flag; i++)
        {
            var displacement = _dir * (Time.deltaTime * 150);
            this.transform.Translate(displacement, Space.World);

            if (Physics.Raycast(this.transform.position + _dir, _dir, out hit,  3.5f ))
            {
                //충돌 안되면 제일 먼저 레이어 부터 확인 할 것.
                Debug.Log(hit.collider.name);
               break;
            }
            
            yield return null;
        }
        //TODO 데미지 처리

        if(hit.collider)
        {
            if (hit.collider.TryGetComponent<CharacterController>(out CharacterController controller))
            {
                controller.GiveDamage(shooter);
            }
        }
        
        Destroy(this.gameObject);
    }
}