using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour, IInteract
{
    protected event Action OnClick;
    public bool interactable = true;
        
    // 
    private MeshRenderer mesh;
    private Material[] meshsOrigin;
    private Material[] meshs;

    public Material effectMaterial;

    private void Awake()
    {
        if (!effectMaterial) return;
        
        // effect meshs

        TryGetComponent<MeshRenderer>(out mesh);

        meshsOrigin = mesh.sharedMaterials;

        meshs = new Material[mesh.sharedMaterials.Length + 1];
        for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            meshs[i] = mesh.sharedMaterials[i];
        meshs[meshs.Length - 1] = effectMaterial;
    }

    public void Interaction()
    {
        if (!interactable) return;

        OnClick?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mesh.sharedMaterials = meshs;
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mesh.sharedMaterials = meshsOrigin;
            interactable = false;
        }
    }
}
