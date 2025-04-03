using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour, IInteract
{
    protected event Action OnClick;
    public bool interactable = true;
    public event Action<bool> OnInteractable;    

    public void Interaction()
    {
        if (!interactable) return;
        OnClick?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = true;
            OnInteractable?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = false;
            OnInteractable?.Invoke(false);
        }
    }
}
