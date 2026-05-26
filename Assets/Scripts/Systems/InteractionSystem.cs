using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactRange = 3f; // Qué tan cerca debes estar del NPC
    public Transform playerCamera;   // Desde dónde sale el rayo (tu cámara)

    void Update()
    {
        // Solo lanzamos el rayo si el jugador presiona 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        // 1. Creamos el rayo que sale del centro de la cámara hacia adelante
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hitInfo;

        // 2. Lanzamos el rayo físicamente en el mundo
        if (Physics.Raycast(ray, out hitInfo, interactRange))
        {
            // 3. Verificamos si golpeó a un objeto con el Tag "NPC"
            if (hitInfo.collider.CompareTag("NPC"))
            {
                // ¡Éxito! Imprimimos un mensaje en la consola
                Debug.Log("¡NPC detectado! Presionaste E. Aquí abriremos la UI del diálogo.");
            }
        }
    }
}