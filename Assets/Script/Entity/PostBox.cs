using System;
using UnityEngine;

public class PostBox : MonoBehaviour, IInteract
{
    private event Action OnClick;

    private void Start()
    {
        OnClick += UIManager.Instance.PopUpMail;
    }

    public void Interaction()
    {
        OnClick?.Invoke();
        Debug.Log("편지 띄우기!");
    }
}
