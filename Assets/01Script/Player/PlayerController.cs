using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMove movement;
    private IInputHandle[] inputHandle;

    private Camera cam;

    private int hp = 3;

    private event Action<int> OnChangeHp;

    public void Init()
    {
        TryGetComponent<IMove>(out movement);
        //TryGetComponent<IInputHandle>(out inputHandle);
        inputHandle = GetComponents<IInputHandle>();
        UIManager.OnPressBtnSlot1 += movement.Jump;

        cam = Camera.main;

        OnChangeHp += UIManager.Instance.SetHpUI;
    }

    private void Update()
    {
        if (inputHandle is null) return;


        Vector3 input = Vector3.zero;
        for (int i = 0; i < inputHandle.Length; i++) {
            if (inputHandle[i].GetInput() != Vector3.zero)
                input = inputHandle[i].GetInput();
        }

        movement.Move(input);


        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();
    }

    public void RemoveButtonInteraction()
    {
        UIManager.OnPressBtnSlot1 -= movement.Jump;
    }

    public void GetDamage(int damage)
    {
        if (hp > 0)
        {
            SetHp(hp - damage);
            if (hp <= 0)
                GameManager.Instance.GameOver();
        }
    }

    public void SetHp(int value)
    {
        hp = value;
        OnChangeHp?.Invoke(value);
    }
}
