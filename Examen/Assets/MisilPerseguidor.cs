using UnityEngine;

public class MisilPerseguidor : MonoBehaviour
{
    public float velocidad = 4f;
    private Rigidbody2D rb;
    private Transform jugador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Busca el objeto del jugador por su "Tag" (etiqueta)
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            // Calcula la direcci�n hacia el jugador
            Vector2 direccion = (Vector2)jugador.position - rb.position;
            direccion.Normalize();

            // Gira el misil hacia el jugador y lo mueve hacia adelante
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
            rb.linearVelocity = transform.up * velocidad;
        }
    }


    // Cuando el misil choca con algo, se destruye
    void OnCollisionEnter2D(Collision2D collision)
    {
        // --- LÍNEA NUEVA ---
        // Llama al GameManager para sumar un punto
        GameManager.instance.AumentarPuntuacion();
        // --- FIN LÍNEA NUEVA ---

        Destroy(gameObject); // El misil se destruye como antes
    }
}