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
                var v = hit.point;
                
                Debug.Log(v);
                this.transform.LookAt(v);
                point = hit.point;
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