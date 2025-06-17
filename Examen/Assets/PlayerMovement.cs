using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 8f;
    private Rigidbody2D rb;

    // Nuevas variables para los límites
    private Camera camaraPrincipal;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // --- CÓDIGO NUEVO ---
        // Guarda una referencia a la cámara principal
        camaraPrincipal = Camera.main;

        // Calcula los límites del mundo basados en la vista de la cámara
        // La esquina inferior izquierda
        Vector2 esquinaInfIzq = camaraPrincipal.ScreenToWorldPoint(new Vector3(0, 0, camaraPrincipal.nearClipPlane));
        // La esquina superior derecha
        Vector2 esquinaSupDer = camaraPrincipal.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camaraPrincipal.nearClipPlane));

        minX = esquinaInfIzq.x;
        maxX = esquinaSupDer.x;
        minY = esquinaInfIzq.y;
        maxY = esquinaSupDer.y;
        // --- FIN CÓDIGO NUEVO ---
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 direccion = new Vector2(horizontal, vertical);
        rb.linearVelocity = direccion.normalized * velocidad;

        if (direccion != Vector2.zero)
        {
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        }
    }

    // --- FUNCIÓN NUEVA ---
    // LateUpdate se ejecuta después de todos los Update, es ideal para ajustar posiciones.
    void LateUpdate()
    {
        // Crea una copia de la posición actual del jugador
        Vector3 posicionSujeta = transform.position;

        // Sujeta la posición en el eje X y Y usando los límites que calculamos
        posicionSujeta.x = Mathf.Clamp(posicionSujeta.x, minX, maxX);
        posicionSujeta.y = Mathf.Clamp(posicionSujeta.y, minY, maxY);

        // Aplica la nueva posición (ya corregida) al jugador
        transform.position = posicionSujeta;
    }
    // --- FIN FUNCIÓN NUEVA ---

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MisilPerseguidor>() != null)
        {
            // En lugar de destruirse, llama a la función GameOver del GameManager
            GameManager.instance.GameOver();

            // Desactiva el objeto del jugador para que desaparezca
            gameObject.SetActive(false);
        }
    }

    // Esta función se llama cuando el jugador entra en un collider que es "Trigger"
    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto con el que chocó tiene el tag "PowerUp"
        if (other.CompareTag("PowerUp"))
        {
            // Primero, destruye el objeto del power-up para que no se pueda usar dos veces
            Destroy(other.gameObject);

            // Luego, busca TODOS los objetos de misiles en la escena
            MisilPerseguidor[] todosLosMisiles = FindObjectsOfType<MisilPerseguidor>();

            // Recorre la lista de misiles encontrados
            foreach (MisilPerseguidor misil in todosLosMisiles)
            {
                // Aumenta la puntuación por cada misil destruido
                GameManager.instance.AumentarPuntuacion();
                // Destruye el misil
                Destroy(misil.gameObject);
            }
        }
    }
}