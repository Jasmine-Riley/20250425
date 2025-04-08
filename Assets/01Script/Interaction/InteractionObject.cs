using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionObject : MonoBehaviour, IInteract
{
    protected event Action OnClick;
    public bool interactable = true;
    public bool oneOff = false;
    public event Action<bool> OnInteractable;

    [SerializeField] private UnityEvent OnClickUnityEvent;

    public void Interaction()
    {
        if (!interactable) return;

        OnClickUnityEvent?.Invoke();
        OnClick?.Invoke();

        if (oneOff)
        {
            interactable = false;
            OnInteractable?.Invoke(false);
            Destroy(this);
        }
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
