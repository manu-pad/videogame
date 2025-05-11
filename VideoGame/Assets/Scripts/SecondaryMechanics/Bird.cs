using UnityEngine;

public class Bird : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float followStopDistance = 1.5f;

    public GameObject destinationObject;

    private Rigidbody2D rb;
    private bool hasReachedDestination = false;
    private Collider2D destinationCollider;
    private Vector2 initialPosition;
    private RigidbodyConstraints2D originalConstraints;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        initialPosition = transform.position;
        originalConstraints = rb.constraints;

        if (destinationObject != null)
        {
            destinationCollider = destinationObject.GetComponent<Collider2D>();
            if (destinationCollider == null)
            {
                Debug.LogWarning("O objeto de destino não possui um Collider2D.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum objeto de destino atribuído à ovelha.");
        }
    }

    void FixedUpdate()
    {
        if (player == null || hasReachedDestination)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange && distance > followStopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == destinationCollider)
        {
            hasReachedDestination = true;
            rb.linearVelocity = Vector2.zero;

            if (destinationCollider is BoxCollider2D box)
            {
                Vector2 bottomPosition = new Vector2(
                    box.bounds.center.x,
                    box.bounds.min.y
                );

                float sheepHeight = GetComponent<Collider2D>().bounds.extents.y;
                transform.position = new Vector2(bottomPosition.x, bottomPosition.y + sheepHeight);
            }

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }

    public void ResetBird()
    {
        transform.position = initialPosition;
        hasReachedDestination = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = originalConstraints;
    }
}
