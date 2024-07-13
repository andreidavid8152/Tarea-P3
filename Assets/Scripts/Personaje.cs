using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded;

    private Rigidbody rb;
    private Vector3 moveDirection;

    // Referencia a la cámara principal
    private Transform cameraTransform;

    // Referencia al Animator
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Obtener la dirección hacia adelante y hacia la derecha en relación con la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Asegurarse de que el movimiento esté en el plano horizontal
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calcular la dirección de movimiento en relación con la cámara
        moveDirection = (forward * moveVertical + right * moveHorizontal).normalized * moveSpeed;

        // Rotar el personaje en la dirección de movimiento
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Actualizar el valor de isRunning en el Animator
        if (animator != null)
        {
            animator.SetBool("isRunning", moveDirection != Vector3.zero);
        }
    }

    void FixedUpdate()
    {
        // Aplicar el movimiento en FixedUpdate para un movimiento más suave con física
        Vector3 velocity = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
