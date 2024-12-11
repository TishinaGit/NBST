using System.Collections;
using UnityEngine;

public class DisableUILoading : MonoBehaviour
{
    [SerializeField] private Canvas _canvasLoading;

    private void Start()
    {
        StartCoroutine(DisableLoadingCanvas());
    }
    public IEnumerator DisableLoadingCanvas()
    {
        yield return new WaitForSeconds(2);

        _canvasLoading.enabled = false;
    }
}
