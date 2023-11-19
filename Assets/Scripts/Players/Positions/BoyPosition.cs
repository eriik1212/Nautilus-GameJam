using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
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
        if(Serializer.boyPositionXML != null)
        {
            boyPosition = Serializer.boyPositionXML;
            boyTransform.position = new Vector3(boyPosition.x, boyPosition.y, boyTransform.position.z);
        }
        else{
            boyPosition = boyTransform.position;

        }

        // Puedes imprimir la posici�n en la consola
        Debug.Log("Posici�n del jugador: " + boyPosition);
    }
}