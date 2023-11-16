using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyPosition : MonoBehaviour
{
    public Transform boyTransform;

    void Start()
    {
        
        if (boyTransform == null)
        {
            Debug.LogError("No se encontró el objeto del jugador.");
        }
    }

    void Update()
    {
        // Obtener la posición del jugador
        Vector2 posicionJugador = boyTransform.position;

        // Puedes imprimir la posición en la consola
        Debug.Log("Posición del jugador: " + posicionJugador);
    }
}