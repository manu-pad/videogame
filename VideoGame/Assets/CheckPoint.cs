using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector2 lastCheckpointPosition;

    [Header("Image to show when this checkpoint is activated")]
    public GameObject imageToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (lastCheckpointPosition != (Vector2)transform.position)
            {
                lastCheckpointPosition = transform.position;
                Debug.Log("New checkpoint activated at: " + lastCheckpointPosition);

                if (imageToActivate != null)
                {
                    imageToActivate.SetActive(true);
                }
            }

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.RestoreFullHealth();
            }
        }
    }
}
