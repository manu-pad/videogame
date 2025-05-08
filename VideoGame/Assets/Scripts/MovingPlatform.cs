using UnityEngine;
using System.Collections;


public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float waitTime = 0.03f;

    private Vector3 nextPosition;
    private bool isWaiting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        nextPosition = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == nextPosition)
            {
                StartCoroutine(WaitAndSwitch());
            }
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true;

        if (waitTime > 0f)
            yield return new WaitForSeconds(waitTime);

        nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        isWaiting = false;
    }

}
