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
        }
        else
        {
            girlPosition = girlTransform.position;

        }
    }
}