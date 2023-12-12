using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoyData : MonoBehaviour
{
    public Transform boyTransform;
    static public Vector2 boyPosition;
    static public bool boyAttack;
    
    void Start()
    {
        boyPosition = new Vector2(2.622f, 1.901f); // RESPAWN
        boyAttack = false;

        if (boyTransform == null)
        {
            Debug.LogError("No se encontró el objeto del jugador.");
        }
    }

    void Update()
    {
        // Obtener la posición del jugador
        if(Serializer.boyPositionXML != new Vector2(0f,0f))
        {

            boyPosition = Serializer.boyPositionXML;
            boyTransform.position = boyPosition;
        }
        else
        {
            boyPosition = boyTransform.position;
        }

        boyAttack = Serializer.boyAttackXML;
    }
}