using UnityEngine;

public class PlatformFollower : MonoBehaviour
{
    private GameObject currentPlatform;
    private Vector3 lastPlatformPosition;

    void Update()
    {
        if (currentPlatform != null)
        {
            Vector3 delta = currentPlatform.transform.position - lastPlatformPosition;
            transform.position += delta;
            lastPlatformPosition = currentPlatform.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.gameObject;
            lastPlatformPosition = currentPlatform.transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}
