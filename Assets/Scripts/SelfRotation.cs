using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    [Header("Rotaci�n")]
    public float rotationSpeed = 180f; // Velocidad de rotaci�n en grados por segundo

    void Update()
    {
        // Rotar el objeto en el eje Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
