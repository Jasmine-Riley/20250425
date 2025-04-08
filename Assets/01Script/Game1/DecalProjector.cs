using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalProjector : MonoBehaviour
{
    [SerializeField] private Material _projectorMaterial;
    [SerializeField] private float _blendSpeed = 25.0f;

    private DecalProjector decalProjector;

    private RenderTexture _prev;
    private RenderTexture _curr;

    [SerializeField] private RenderTexture _fogTexture;
    private float _blendAmount;

    private void Awake()
    {
        if (TryGetComponent<DecalProjector>(out decalProjector))
            decalProjector.enabled = true;

        _prev = GenerateTexture();
        _curr = GenerateTexture();

        decalProjector._projectorMaterial = new Material(_projectorMaterial);

        decalProjector._projectorMaterial.SetTexture("_PrevTexture", _prev);
        decalProjector._projectorMaterial.SetTexture("_CurrTexture", _curr);

        Blend();
    }

    private RenderTexture GenerateTexture()
    {
        RenderTexture rt = new RenderTexture(_fogTexture.width * 2, _fogTexture.height * 2, 0, _fogTexture.format);
        rt.filterMode = FilterMode.Bilinear;

        return rt;
    }

    IEnumerator Fog()
    {
        while(_blendAmount < 1)
        {
            _blendAmount += Time.deltaTime * _blendSpeed;
            decalProjector._projectorMaterial.SetFloat("_Blend", _blendAmount);
            yield return null;
        }

        Blend();
    }

    private void Blend()
    {
        //StopCoroutine("Fog");
        _blendAmount = 0;
        // Swap the textures
        Graphics.Blit(_curr, _prev);
        Graphics.Blit(_fogTexture, _curr);

        StartCoroutine("Fog");
    }
}
