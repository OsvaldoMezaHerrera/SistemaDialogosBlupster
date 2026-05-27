using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactRange = 3f; 
    public Transform playerCamera;   

    void Update()
    {
        // Si el diálogo ya está abierto, cancelamos el rayo para no abrirlo 2 veces
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueOpen())
            return;

        // Presionamos E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactRange))
        {
            if (hitInfo.collider.CompareTag("NPC"))
            {
                // Buscamos el script del NPC que tiene guardado tu texto
                NPCDialogue npc = hitInfo.collider.GetComponent<NPCDialogue>();
                
                if (npc != null && npc.nodoInicial != null)
                {
                    // ¡Abrimos la interfaz visual de Brayan!
                    DialogueManager.Instance.StartDialogue(npc.nodoInicial);
                }
            }
        }
    }
}