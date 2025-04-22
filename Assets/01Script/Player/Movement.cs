using UnityEngine;

public class Movement : MonoBehaviour, IMove
{
    private Rigidbody rb;
    private CapsuleCollider cc;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask checkLayer;

    [SerializeField] private float speed = 50f;
    [SerializeField] private float jump = 15f;

    public bool movable { get; protected set; }

    private bool isGrounded;

    private void Awake()
    {
        movable = true;
        TryGetComponent<Rigidbody>(out rb);
        TryGetComponent<CapsuleCollider>(out cc);

        var n = Physics.gravity;
        n.y = -40f;
        Physics.gravity = n;
    }
    
    public void Move(Vector3 direction)
    {
        if (!movable) return;
        //if (direction == Vector3.zero) return;

        var vect = direction;
        vect.y = 0;
        var nomalizedVect = vect.normalized * speed;
        nomalizedVect.y = rb.velocity.y;
        rb.velocity = nomalizedVect;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(jump * Vector3.up, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (Physics.OverlapSphere(groundCheck.position, 0.2f, checkLayer).Length > 0)
            isGrounded = true;
        else
            isGrounded = false;
    }
}
