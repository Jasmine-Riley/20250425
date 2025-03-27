using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMove movement;
    private IInputHandle[] inputHandle;

    private Camera cam;

    public void Init()
    {
        TryGetComponent<IMove>(out movement);
        //TryGetComponent<IInputHandle>(out inputHandle);
        inputHandle = GetComponents<IInputHandle>();
        UIManager.OnPressBtnSlot1 += movement.Jump;

        cam = Camera.main;
    }

    private void Update()
    {
        if (inputHandle is null) return;

        var dir = cam.transform.localRotation * Vector3.forward;

        //movement.Move(inputHandle.GetInput());
        for(int i = 0; i < inputHandle.Length; i++)
            movement.Move(inputHandle[i].GetInput());


        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();
    }
}
