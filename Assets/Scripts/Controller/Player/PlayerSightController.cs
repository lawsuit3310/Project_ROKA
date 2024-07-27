using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSightController : CharacterSightController
{

    private void Awake()
    {
        cam ??= Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Sighting());
    }


    public override IEnumerator Sighting()
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
            yield return null;
        }
    }
}