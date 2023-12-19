using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoyData : MonoBehaviour
{
    public Transform boyTransform;
    static public Vector2 boyPosition;
    static public bool boyAttack;
    static public bool isBoyJumping;
    public Animator boyAnimator;

    static public bool isBoyMoving = false;

    static public int boyOrientation = 0; // no rotation --> 0, right --> 1, left --> 2

    void Start()
    {
        boyPosition = new Vector2(2.622f, 1.901f); // RESPAWN
        boyAttack = false;
        isBoyMoving = false;

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

        isBoyJumping = Serializer.boyJumpingXML;

        boyOrientation = Serializer.boyOrientationXML;
    }
}