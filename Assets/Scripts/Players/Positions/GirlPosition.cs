using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPosition : MonoBehaviour
{
    public Transform girlTransform;

    void Start()
    {
       

        if (girlTransform == null)
        {
            Debug.LogError("No se encontró el objeto del jugador.");
        }
    }

    void Update()
    {
        // Obtener la posición del jugador
        Vector2 posicionJugadora = girlTransform.position;

        // Puedes imprimir la posición en la consola
        Debug.Log("Posición del jugador: " + posicionJugadora);
    }
}