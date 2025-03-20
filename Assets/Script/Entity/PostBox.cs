using System;
using UnityEngine;

public class PostBox : MonoBehaviour, IInteract
{
    private event Action OnClick;

    private void Start()
    {
        OnClick += UIManager.Instance.OpenMail;
    }

    public void Interaction()
    {
        OnClick?.Invoke();
    }
}
