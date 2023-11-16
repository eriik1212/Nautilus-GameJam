using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyPosition : MonoBehaviour
{
    public Transform boyTransform;
    static public Vector2 boyPosition;

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
        boyPosition = boyTransform.position;

        // Puedes imprimir la posici�n en la consola
        Debug.Log("Posici�n del jugador: " + boyPosition);
    }
}