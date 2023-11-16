using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPosition : MonoBehaviour
{
    public Transform girlTransform;
    static public Vector2 girlPosition;

    void Start()
    {
       

        if (girlTransform == null)
        {
            Debug.LogError("No se encontr� el objeto del jugador.");
        }
    }

    void Update()
    {
        // Obtener la posici�n del jugador
        girlPosition = girlTransform.position;

        // Puedes imprimir la posici�n en la consola
        Debug.Log("Posici�n del jugador: " + girlPosition);
    }
}