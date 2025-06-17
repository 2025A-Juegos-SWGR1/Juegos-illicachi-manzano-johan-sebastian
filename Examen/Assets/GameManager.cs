using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <-- �MUY IMPORTANTE! Para gestionar escenas.

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text textoPuntuacion;

    private int puntuacion = 0;

    // --- VARIABLES NUEVAS ---
    public GameObject gameOverPanel; // Referencia al panel que creamos
    public TMP_Text puntuacionFinalTexto; // Referencia al texto del panel

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegurarse de que el panel est� oculto y el juego corra a velocidad normal
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Velocidad normal del juego
        textoPuntuacion.text = "Puntuaci�n: " + puntuacion.ToString();
    }

    public void AumentarPuntuacion()
    {
        puntuacion++;
        textoPuntuacion.text = "Puntuaci�n: " + puntuacion.ToString();
    }

    // --- FUNCI�N NUEVA ---
    public void GameOver()
    {
        // Muestra el panel de Game Over
        gameOverPanel.SetActive(true);
        // Actualiza el texto de la puntuaci�n final
        puntuacionFinalTexto.text = "Puntuaci�n Final: " + puntuacion.ToString();
        // Detiene el tiempo en el juego para que todo se congele
        Time.timeScale = 0f;
    }

    // --- FUNCI�N NUEVA ---
    public void ReiniciarJuego()
    {
        // Restaura la velocidad del tiempo antes de cargar la escena
        Time.timeScale = 1f;
        // Carga la escena actual de nuevo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}