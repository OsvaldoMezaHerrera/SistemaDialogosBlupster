using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager Instance;

    [Header("Variables del jugador")]
    public int confianza = 0;
    public int estres = 0;
    public int puntos = 0;

    void Awake()
    {
        // Singleton: solo puede existir uno en toda la escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ModificarVariables(int confianzaChange, int estresChange, int puntosChange)
    {
        confianza += confianzaChange;
        estres += estresChange;
        puntos += puntosChange;

        Debug.Log($"Confianza: {confianza} | Estrés: {estres} | Puntos: {puntos}");
    }
}