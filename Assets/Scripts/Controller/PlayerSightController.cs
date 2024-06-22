using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSightController : MonoBehaviour
{
    public Camera cam;
    private void Start()
    {
        cam ??= FindObjectOfType<Camera>();
        StartCoroutine(Sight());
    }

    private void Update()
    {
        var Ray = cam.ScreenPointToRay(Input.mousePosition);
        
    }

    IEnumerator Sight()
    {
        // while (true)
        // {
        //     var mPos = Input.mousePosition;
        //     var scrPos = Camera.main.WorldToScreenPoint(transform.localPosition);
        //
        //     var offset = new Vector2(mPos.x - scrPos.x, mPos.y - scrPos.y);
        //     float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //     
        //     Debug.Log(angle);
        //     this.transform.rotation = Quaternion.Euler(0, angle, 0);
        //     yield return null;
        // }
        yield return null;
    }
}