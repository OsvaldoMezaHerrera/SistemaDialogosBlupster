using UnityEngine;

// Esto asegura que Unity le ponga automáticamente el componente de físicas al jugador
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento (WASD)")]
    public float moveSpeed = 5f;

    [Header("Configuración de Cámara (Ratón)")]
    public float mouseSensitivity = 200f;
    public Transform playerCamera; // Aquí conectaremos la cámara

    private CharacterController controller;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Esto oculta el cursor y lo bloquea en el centro de la pantalla al jugar
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- 1. ROTACIÓN DE LA CÁMARA ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la vista para no dar volteretas hacia atrás

        // Gira la cámara hacia arriba/abajo
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Gira todo el cuerpo del jugador hacia los lados
        transform.Rotate(Vector3.up * mouseX);


        // --- 2. MOVIMIENTO DEL JUGADOR ---
        float x = Input.GetAxis("Horizontal"); // Detecta A y D
        float z = Input.GetAxis("Vertical");   // Detecta W y S

        // Calcula la dirección basándose hacia dónde está mirando el jugador
        Vector3 move = transform.right * x + transform.forward * z;

        // Mueve al jugador
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}