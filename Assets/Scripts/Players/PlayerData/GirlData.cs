using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlData : MonoBehaviour
{
    public Transform girlTransform;
    static public Vector2 girlPosition;
    static public bool girlAttack;
    static public bool isGirlJumping;
    public Animator girlAnimator;

    static public bool isGirlMoving = false;

    static public int girlOrientation = 0; // no rotation --> 0, right --> 1, left --> 2

    void Start()
    {

        girlPosition = new Vector2(2.622f, 1.901f);

        girlOrientation = 0;

        isGirlMoving = false;

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

        girlAttack = Serializer.girlAttackXML;

        isGirlJumping = Serializer.girlJumpingXML;

        girlOrientation = Serializer.girlOrientationXML;
    }
}