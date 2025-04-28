using UnityEngine;

public class Gear : MonoBehaviour
{
    [Tooltip("Velocidade da rotação em graus por segundo")]
    public float rotationSpeed = 90f;

    [Tooltip("Se verdadeiro, gira no sentido horário; senão, sentido anti-horário")]
    public bool clockwise = true;

    void Update()
    {
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, rotationSpeed * direction * Time.deltaTime);
    }
}
