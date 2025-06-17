using System;
using UnityEngine;

public class GeneradorDeMisiles : MonoBehaviour
{
    public GameObject misilPrefab; // Aquí arrastraremos nuestro misil
    public float ratioDeAparicion = 0.5f; // Misiles por segundo
    private float tiempoParaAparecer;

    void Update()
    {
        if (Time.time > tiempoParaAparecer)
        {
            // Crea un nuevo misil en una posición aleatoria en el borde de la pantalla 
            Vector2 posicionAleatoria = new Vector2(UnityEngine.Random.Range(-9f, 9f), 6f);
            Instantiate(misilPrefab, posicionAleatoria, Quaternion.identity);

            // Resetea el contador
            tiempoParaAparecer = Time.time + 1f / ratioDeAparicion;
        }
    }
}