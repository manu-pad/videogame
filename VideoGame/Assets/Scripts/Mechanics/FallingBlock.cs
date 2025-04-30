using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public GameObject groundTrigger;
    public Transform fallTarget;
    public float fallSpeed = 10f;
    public float riseSpeed = 3f;
    public float waitTime = 1f;

    private bool playerDetected = false;
    private bool isFalling = false;
    private bool isWaiting = false;
    private bool readyToDetect = true; 
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        if (groundTrigger == null)
            Debug.LogWarning("GroundTrigger não atribuído!");

        if (fallTarget == null)
            Debug.LogError("Fall Target não atribuído!");
    }

    void Update()
    {
        if (playerDetected && readyToDetect && !isFalling && !isWaiting)
        {
            isFalling = true;
            readyToDetect = false; 
        }

        if (isFalling)
        {
            float newY = Mathf.MoveTowards(transform.position.y, fallTarget.position.y, fallSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y <= fallTarget.position.y + 0.01f)
            {
                transform.position = new Vector3(transform.position.x, fallTarget.position.y, transform.position.z);
                isFalling = false;
                isWaiting = true;
                Invoke(nameof(StartRising), waitTime);
            }
        }
        else if (!isWaiting && transform.position.y < startPosition.y)
        {
            float newY = Mathf.MoveTowards(transform.position.y, startPosition.y, riseSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (transform.position.y >= startPosition.y - 0.01f)
            {
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
                readyToDetect = true; 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && readyToDetect)
        {
            playerDetected = true;
        }
    }

    void StartRising()
    {
        isWaiting = false;
        playerDetected = false;
    }
}
