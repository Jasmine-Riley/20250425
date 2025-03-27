using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMove movement;
    private IInputHandle[] inputHandle;
    
    public void Init()
    {
        TryGetComponent<IMove>(out movement);
        //TryGetComponent<IInputHandle>(out inputHandle);
        inputHandle = GetComponents<IInputHandle>();
        UIManager.OnPressBtnSlot1 += movement.Jump;
    }

    private void Update()
    {
        //movement.Move(inputHandle.GetInput());
        for(int i = 0; i < inputHandle.Length; i++)
            movement.Move(inputHandle[i].GetInput());


        if (Input.GetKeyDown(KeyCode.Space))
            movement.Jump();
    }
}
