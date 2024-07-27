using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSightController : MonoBehaviour
{
    public Camera cam;
    public Vector3 point;

    private void Awake()
    {
        cam ??= Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Sighting());
    }


    IEnumerator Sighting()
    {
        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(!hit.collider.CompareTag("Player"))
                {
                    if (!hit.collider.CompareTag("Enemy"))
                        point = hit.point;
                    else
                        point = hit.point - new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }
                else
                {
                    Debug.Log("Ray hit to Player");
                }
            }
            // var offset = new Vector2(mPos.x - scrPos.x, mPos.y - scrPos.y);
            // float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            //
            // Debug.Log(angle);
            // this.transform.rotation = Quaternion.Euler(0, angle, 0);
            yield return null;
        }
        //yield return null;
    }
}