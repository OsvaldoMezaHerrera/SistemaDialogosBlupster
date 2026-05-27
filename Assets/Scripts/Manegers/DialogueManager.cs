using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Referencia al panel de UI")]
    public GameObject dialoguePanel;

    private DialogueNode currentNode;
    private bool isDialogueOpen = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(DialogueNode startNode)
    {
        isDialogueOpen = true;
        dialoguePanel.SetActive(true);
        
        // Liberar y mostrar el ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        LoadNode(startNode);
    }

    public void LoadNode(DialogueNode node)
    {
        currentNode = node;
        // Le avisamos a la UI que actualice la pantalla con los datos de este nodo
        UIManager.Instance.UpdateUI(node);
    }

    public void SelectChoice(int choiceIndex)
    {
        DialogueChoice choice = currentNode.choices[choiceIndex];

        // Modificar variables
        VariableManager.Instance.ModificarVariables(
            choice.confianzaChange,
            choice.estresChange,
            choice.puntosChange
        );

        // Ir al siguiente nodo o cerrar
        if (choice.nextNode != null)
        {
            LoadNode(choice.nextNode);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isDialogueOpen = false;
        dialoguePanel.SetActive(false);
        currentNode = null;
        
        // Ocultar y bloquear el ratón de nuevo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Debug.Log("Conversación terminada.");
    }

    public bool IsDialogueOpen() => isDialogueOpen;
}