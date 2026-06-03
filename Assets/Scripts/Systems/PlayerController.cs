using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento (WASD)")]
    public float moveSpeed = 5f;

    [Header("Configuración de Cámara (Ratón)")]
    public float mouseSensitivity = 200f;
    public Transform playerCamera; 

    private CharacterController controller;
    private float xRotation = 0f;

    // --- Variables nuevas para la gravedad ---
    private Vector3 velocity;
    private float gravity = -9.81f; // La fuerza de gravedad de la Tierra

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Si el diálogo está abierto, congelamos la cámara y el movimiento
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueOpen())
            return;

        // --- 1. ROTACIÓN DE LA CÁMARA ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


        // --- 2. MOVIMIENTO DEL JUGADOR ---
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);


        // --- 3. GRAVEDAD ---
        // Si estamos tocando el piso, detenemos la velocidad de caída
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Lo mantenemos pegado al suelo
        }

        // Le aplicamos la fuerza de gravedad a lo largo del tiempo
        velocity.y += gravity * Time.deltaTime;
        
        // Movemos al jugador hacia abajo
        controller.Move(velocity * Time.deltaTime);
    }
}