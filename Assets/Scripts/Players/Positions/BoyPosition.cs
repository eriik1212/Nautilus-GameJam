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
            Debug.LogError("No se encontr� el objeto del jugador.");
        }
    }

    void Update()
    {
        // Obtener la posici�n del jugador
        Vector2 posicionJugador = boyTransform.position;

        // Puedes imprimir la posici�n en la consola
        Debug.Log("Posici�n del jugador: " + posicionJugador);
    }
}