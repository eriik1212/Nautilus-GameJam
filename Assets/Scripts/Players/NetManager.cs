using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetManager : MonoBehaviour
{
    static public NetManager instance;

    public RemoteInputs remoteInputs = new();

    [SerializeField] PlayerController boyController;
    [SerializeField] PlayerController girlController;
    [SerializeField] RemotePlayerController boyRemoteController;
    [SerializeField] RemotePlayerController girlRemoteController;

    [SerializeField] DetectorEchoAttack boyEcho;
    [SerializeField] ProjectileEchoAttack girlEcho;
    [SerializeField] RemoteDetectorEchoAttack boyRemoteEcho;
    [SerializeField] RemoteProjectileEchoAttack girlRemoteEcho;
    private void Awake()
    {
        instance = this; // revisar
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Server"))
        {
            boyController.enabled = true;
            girlRemoteController.enabled = true;
            boyEcho.enabled = true;
            girlRemoteEcho.enabled = true;
        }
        else
        {
            girlController.enabled = true;
            boyRemoteController.enabled = true;
            girlEcho.enabled = true;
            boyRemoteEcho.enabled = true;
        }
        remoteInputs.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}