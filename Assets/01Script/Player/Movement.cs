using UnityEngine;

public class Movement : MonoBehaviour, IMove
{
    private CharacterController controller;
    [SerializeField] private float speed = 5f;
    public bool movable { get; protected set; }

    private void Awake()
    {
        movable = true;
        TryGetComponent<CharacterController>(out controller);
    }

    public void Move(Vector3 direction)
    {
        if (!movable) return;
        
        controller.Move(speed * Time.deltaTime * direction);
    }
}
