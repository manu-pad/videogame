using System.Collections;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float returnDelay = 3f;
    public Transform detectionArea; // Área de trigger
    public Vector3 shakeAmount = new Vector3(0.1f, 0f, 0f); // Chacoalha horizontalmente
    public float shakeSpeed = 50f;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isFalling = false;
    private bool isResetting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        rb.bodyType = RigidbodyType2D.Kinematic; // Fica parado até cair
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling && !isResetting)
        {
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        // Sacudida
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount.x;
            transform.position = originalPosition + new Vector3(offset, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // volta à posição exata
        rb.bodyType = RigidbodyType2D.Dynamic; // começa a cair
        isFalling = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && collision.collider.CompareTag("Ground")) // ou outra tag de chão
        {
            StartCoroutine(ResetBlock());
        }
    }

    IEnumerator ResetBlock()
    {
        isResetting = true;
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(returnDelay);

        float timeToReturn = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < timeToReturn)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed / timeToReturn);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isResetting = false;
    }
}
