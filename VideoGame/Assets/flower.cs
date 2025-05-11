using UnityEngine;

public class Flower : MonoBehaviour
{
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void ResetFlower()
    {
        transform.position = initialPosition;  
        gameObject.SetActive(true);
    }
}
