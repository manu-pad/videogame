using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public string playerTag = "Player";

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool atPointA = true;

    void Start()
    {
        transform.position = pointA.position;
        targetPosition = pointA.position;
    }

    void Update()
    {
        if (PauseController.IsGamePaused || !isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            atPointA = !atPointA;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isMoving)
        {
            Debug.Log("Jogador ativou o elevador!");

            targetPosition = atPointA ? pointB.position : pointA.position;
            isMoving = true;
        }
    }
}
