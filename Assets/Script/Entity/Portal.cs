using System;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    private event Action OnClick;

    public void Interaction()
    {
        OnClick?.Invoke();
    }
}
