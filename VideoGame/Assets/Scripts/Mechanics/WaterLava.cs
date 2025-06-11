using UnityEngine;
using System.Collections;

public class WaterLava : MonoBehaviour
{
    public Transform restartSpot;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RespawnAfterDelay(other.transform));
        }
    }

    IEnumerator RespawnAfterDelay(Transform player)
    {
        yield return new WaitForSeconds(0.5f);
        player.position = restartSpot.position;
    }
}
