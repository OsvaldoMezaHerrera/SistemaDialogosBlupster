using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Referencias UI")]
    public TextMeshProUGUI npcText;
    public Button[] choiceButtons;
    public TextMeshProUGUI[] choiceTexts;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateUI(DialogueNode node)
    {
        // Muestra el texto del NPC
        npcText.text = node.npcText;

        // Actualiza cada botón con su opción
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < node.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = node.choices[i].choiceText;

                int index = i; 
                choiceButtons[i].onClick.RemoveAllListeners();
                
                // Le dice al botón qué opción debe mandar al DialogueManager
                choiceButtons[i].onClick.AddListener(() =>
                    DialogueManager.Instance.SelectChoice(index));
            }
            else
            {
                // Si el nodo tiene menos de 3 opciones, esconde los botones sobrantes
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
}