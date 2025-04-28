using UnityEngine;

public class Gear : MonoBehaviour
{
    [Tooltip("Velocidade da rota��o em graus por segundo")]
    public float rotationSpeed = 90f;

    [Tooltip("Se verdadeiro, gira no sentido hor�rio; sen�o, sentido anti-hor�rio")]
    public bool clockwise = true;

    void Update()
    {
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, rotationSpeed * direction * Time.deltaTime);
    }
}
