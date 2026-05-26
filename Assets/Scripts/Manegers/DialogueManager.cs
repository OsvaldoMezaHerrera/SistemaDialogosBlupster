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
        LoadNode(startNode);
    }

    public void LoadNode(DialogueNode node)
    {
        currentNode = node;
        Debug.Log("NPC dice: " + node.npcText);
        // Aquí después llamaremos al UIManager para mostrar el texto y los botones
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
        Debug.Log("Conversación terminada.");
    }

    public bool IsDialogueOpen() => isDialogueOpen;
}