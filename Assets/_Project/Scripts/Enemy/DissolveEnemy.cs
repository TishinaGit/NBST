using System.Collections; 
using UnityEngine;
 
[RequireComponent(typeof(MeshRenderer))]
public class DissolveEnemy : MonoBehaviour
{
    private const float _maxTreshold = 1;
    private const string _tresholdKey = "_Dissolve";
    private MeshRenderer _render;
    private bool _checkEventDeath;

    private void Awake()
    {
        _render = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        OnShaderDeath();
    }

    private void OnShaderDeath()
    { 
        if (_checkEventDeath == true)
        {
            if (DeathMaterial() != null)
            {
                StopCoroutine(DeathMaterial());
            }
            StartCoroutine(DeathMaterial());
        }
       
    }

    public void EnemtEventDeath()
    {
        _checkEventDeath = true;
    }
     
    private IEnumerator DeathMaterial()
    { 
        float treshold = 0;
        while (treshold < _maxTreshold)
        {
            treshold += Time.deltaTime;
            _render.material.SetFloat(_tresholdKey, treshold);
            yield return null;
        } 
    } 
}
