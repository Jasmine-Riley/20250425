using UnityEngine;

public class KeyboardInputHandle : MonoBehaviour, IInputHandle
{
    public Vector3 GetInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}
