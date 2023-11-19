using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoyNetwData : MonoBehaviour
{
    public Transform boyTransform;
    static public Vector2 boyPosition;
    static public bool attack;

    static public int boyRotationDirection; // no rotation --> 0, right --> 1, left --> 2
    static public int boyRotationAngle;

    void Start()
    {

        if (boyTransform == null)
        {
            Debug.LogError("No se encontró el objeto del jugador.");
        }
    }

    void Update()
    {
        Position();
        Attack();
        Direction();
    }

    private void Direction()
    {
        boyRotationDirection = Serializer.boyRotationDirection;
    }

    private void Position()
    {
        // Obtener la posición del jugador
        if (Serializer.boyPositionXML != null)
        {
            boyPosition = Serializer.boyPositionXML;
        }
        else
        {
            boyPosition = boyTransform.position;

        }

        // Puedes imprimir la posición en la consola
        Debug.Log("Posición del jugador: " + boyPosition);
    }

    private void Attack()
    {

    }
}
