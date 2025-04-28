using UnityEngine;

public enum Start
{
    Forward = 0,
    Right = 90,
    Backward = 180,
    Left = 270,
}

public class Movement : MonoBehaviour, IMove
{
    private Rigidbody rb;
    private CapsuleCollider cc;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask checkLayer;

    [SerializeField] private float speed = 50f;
    [SerializeField] private float jump = 15f;


    [SerializeField] private bool reverseMode = false;

    [SerializeField] private int startAngle = (int)Start.Forward;


    public bool movable { get; protected set; }

    private bool isGrounded;

    private void Awake()
    {
        movable = true;
        TryGetComponent<Rigidbody>(out rb);
        TryGetComponent<CapsuleCollider>(out cc);

        var n = Physics.gravity;
        n.y = -80f;
        Physics.gravity = n;
    }
    
    public void Move(Vector3 direction)
    {
        if (!movable) return;
        //if (direction == Vector3.zero) return;


        Vector3 nomalizedVect = direction.normalized;
        nomalizedVect.y = 0;

        switch (startAngle)
        {
            case (int)Start.Forward:
                nomalizedVect = nomalizedVect * speed;
                break;
            case (int)Start.Left:
                nomalizedVect = new Vector3(-nomalizedVect.z, 0, nomalizedVect.x) * speed;
                break;
            case (int)Start.Backward:
                nomalizedVect = -nomalizedVect * speed;
                break;
            case (int)Start.Right:
                nomalizedVect = new Vector3(nomalizedVect.z, 0, -nomalizedVect.x) * speed;
                break;
        }

        if (direction != Vector3.zero)
            transform.forward = nomalizedVect;

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

    public void SetMove(Start angle)
    {
        startAngle = (int)angle;
    }
}
