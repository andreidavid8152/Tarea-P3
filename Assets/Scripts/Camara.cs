using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player; // El transform del jugador que la cámara seguirá
    public Vector3 offset; // La distancia entre la cámara y el jugador
    public float rotationSpeed = 5.0f; // La velocidad de rotación de la cámara
    public float verticalRotationLimit = 80.0f; // Límite de rotación vertical

    private float currentRotationAngleX;
    private float currentRotationAngleY;

    void Start()
    {
        // Inicializar los ángulos de rotación actuales con la rotación de la cámara
        currentRotationAngleX = transform.eulerAngles.y;
        currentRotationAngleY = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Rotar la cámara con el mouse
            currentRotationAngleX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentRotationAngleY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Limitar la rotación vertical para evitar giros completos
            currentRotationAngleY = Mathf.Clamp(currentRotationAngleY, -verticalRotationLimit, verticalRotationLimit);

            // Convertir los ángulos de rotación a un quaternion
            Quaternion rotation = Quaternion.Euler(currentRotationAngleY, currentRotationAngleX, 0);

            // Calcular la nueva posición de la cámara
            Vector3 position = player.position - (rotation * offset);

            // Ajustar la posición de la cámara
            transform.position = position;

            // Mirar hacia el jugador
            transform.LookAt(player);
        }
    }
}
