using System.Collections; 
using UnityEngine;
 
[RequireComponent(typeof(MeshRenderer))]
public class DissolveEnemy : MonoBehaviour
{
    private const float _maxTreshold = 1;
    private const string TresholdKey = "_Dissolve";
    private MeshRenderer _render;
    private bool h;

    private void Awake()
    {
        _render = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        GGg();
    }

    private void GGg()
    { 
        if (h == true)
        {
            if (DeathMaterial() != null)
            {
                StopCoroutine(DeathMaterial());
            }
            StartCoroutine(DeathMaterial());
        }
       
    }

    public void GO()
    {
        h = true;
    }
     
    private IEnumerator DeathMaterial()
    { 
        float treshold = 0;
        while (treshold < _maxTreshold)
        {
            treshold += Time.deltaTime;
            _render.material.SetFloat(TresholdKey, treshold);
            yield return null;
        } 
    } 
}
