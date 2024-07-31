using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class BeatenTimer : MonoBehaviour
{
    private Renderer[] _renderers;
    private Material[]  _sharedMat;
    private Coroutine _coroutine;
    private MaterialPropertyBlock mpb;
    private void OnEnable()
    {
        mpb ??= new();
        _renderers = _renderers == null || _renderers.Length == 0 
            ? this.transform.GetComponentsInChildren<Renderer>() : _renderers;

        _sharedMat ??= (    
            from r in _renderers
            select r.sharedMaterial
            ).ToArray();

        StartCoroutine(flickering());
    }

    private void OnDisable()
    {
        GC.SuppressFinalize(_sharedMat);
    }

    private void OnDestroy()
    {
        GC.SuppressFinalize(_renderers);
        GC.SuppressFinalize(_sharedMat);
    }

    IEnumerator flickering()
    {
        // *주의* UTS를 사용중인 머테리얼 에서만 동작
        var mpb = new MaterialPropertyBlock();

        var org_base = (
            from r in _renderers
            select r.sharedMaterial.GetColor("_BaseColor")).ToArray();
        var org_1st  = (
            from r in _renderers
            select r.sharedMaterial.GetColor("_1st_ShadeColor")).ToArray();
        var org_2nd = (
            from r in _renderers
            select r.sharedMaterial.GetColor("_2nd_ShadeColor")).ToArray();
        
        while (this.enabled)
        {
            Debug.Log(true);
            mpb.SetColor("_BaseColor", Color.red);
            mpb.SetColor("_1st_ShadeColor", Color.red);
            mpb.SetColor("_2nd_ShadeColor", Color.red);
            
            foreach (var r in _renderers)
            {
                r.SetPropertyBlock(mpb);
            }
            yield return new WaitForSeconds(0.1f);
            
            mpb.SetColor("_BaseColor", org_base[0]);
            mpb.SetColor("_1st_ShadeColor", org_1st[0]);
            mpb.SetColor("_2nd_ShadeColor", org_2nd[0]);
            
            foreach (var r in _renderers)
            {
                r.SetPropertyBlock(mpb);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}