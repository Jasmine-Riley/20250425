using UnityEngine;

public class KeyboardInputHandle : MonoBehaviour, IInputHandle
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
