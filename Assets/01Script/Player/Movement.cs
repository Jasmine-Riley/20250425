using UnityEngine;

public class Movement : MonoBehaviour, IMove
{
    private CharacterController controller;
    [SerializeField] private float speed = 5f;
    public bool movable { get; protected set; }
    private Vector3 moveVect;

    private void Awake()
    {
        movable = true;
        TryGetComponent<CharacterController>(out controller);
    }

    private void Update()
    {
        if(!controller.isGrounded)
            moveVect.y += -9.81f * Time.deltaTime * 0.08f;
    }

    public void Move(Vector3 direction)
    {
        if (!movable) return;

        var vect = speed * Time.deltaTime * direction;
        vect.y = moveVect.y;
        controller.Move(vect);
    }

    public void Jump()
    {
        if(controller.isGrounded)
            moveVect.y = 0.25f;
    }
}
