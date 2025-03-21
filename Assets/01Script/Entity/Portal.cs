using System;
using UnityEngine;

public class Portal : MonoBehaviour, IInteract
{
    private event Action OnClick;

    private void Start()
    {
        OnClick += UIManager.Instance.OpenChapter;
    }

    public void Interaction()
    {
        OnClick?.Invoke();
    }
}
