using System;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 5f; // Aparece cada 5 segundos
    private float spawnTimer;

    // Variables para los l�mites de la pantalla
    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Calcula los l�mites de la pantalla (igual que en el script del jugador)
        Camera camaraPrincipal = Camera.main;
        Vector2 esquinaInfIzq = camaraPrincipal.ScreenToWorldPoint(new Vector3(0, 0, camaraPrincipal.nearClipPlane));
        Vector2 esquinaSupDer = camaraPrincipal.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camaraPrincipal.nearClipPlane));

        minX = esquinaInfIzq.x + 1; // A�adimos un peque�o margen para que no aparezca pegado al borde
        maxX = esquinaSupDer.x - 1;
        minY = esquinaInfIzq.y + 1;
        maxY = esquinaSupDer.y - 1;

        // Inicia el contador
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // El contador descuenta tiempo
        spawnTimer -= Time.deltaTime;

        // Cuando el contador llega a 0, aparece un power-up y se reinicia el contador
        if (spawnTimer <= 0)
        {
            SpawnPowerUp();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnPowerUp()
    {
        // Genera una posici�n aleatoria DENTRO de los l�mites de la pantalla
        Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));

        // Crea una instancia del prefab del power-up
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}