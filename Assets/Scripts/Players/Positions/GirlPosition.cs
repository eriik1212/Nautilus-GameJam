using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPosition : MonoBehaviour
{
    public Transform girlTransform;
    static public Vector2 girlPosition;

    void Start()
    {

        girlPosition = new Vector2(2.622f, 1.901f);

        if (girlTransform == null)
        {
            Debug.LogError("No se encontró el objeto del jugador.");
        }
    }

    void Update()
    {
        if (Serializer.girlPositionXML != new Vector2(0f, 0f))
        {
            girlPosition = Serializer.girlPositionXML;
            girlTransform.position = girlPosition;

            Debug.LogError("POSICION GIRL SERIALIZADA: " + girlPosition);

            /*if (girlPosition.x != girlTransform.position.x)
            {
                girlTransform.position = new Vector3(girlPosition.x, girlPosition.y, girlTransform.position.z);
            }*/
        }
        else
        {
            girlPosition = girlTransform.position;

            Debug.LogError("POSICION GIRL TRANSFORM: " + girlPosition);

        }

        // Puedes imprimir la posición en la consola
        //Debug.LogError("Posición del jugador girl: " + girlPosition);
    }
}