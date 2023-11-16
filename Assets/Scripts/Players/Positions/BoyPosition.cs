using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoyPosition : MonoBehaviour
{
    public Transform boyTransform;
    static public Vector2 boyPosition;

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
        if(Serializer.boyPositionXML != null)
        {
            boyPosition = Serializer.boyPositionXML;
        }
        else{
            boyPosition = boyTransform.position;

        }

        // Puedes imprimir la posición en la consola
        Debug.Log("Posición del jugador: " + boyPosition);
    }
}