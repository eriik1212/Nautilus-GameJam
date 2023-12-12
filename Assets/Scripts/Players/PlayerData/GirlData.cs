using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlData : MonoBehaviour
{
    public Transform girlTransform;
    static public Vector2 girlPosition;
    static public bool girlAttack;

    public ProjectileEchoAttack echoScript;
    void Start()
    {

        girlPosition = new Vector2(2.622f, 1.901f);

        if (girlTransform == null)
        {
            Debug.LogError("No se encontr� el objeto del jugador.");
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

        girlAttack = Serializer.girlAttackXML;
        echoScript.echoAttack = girlAttack;
    }
}