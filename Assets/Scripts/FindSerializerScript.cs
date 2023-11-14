using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindSerializerScript : MonoBehaviour
{
    private Serializer serScript;

    // Start is called before the first frame update
    void Start()
    {
        serScript = GameObject.Find("NetworkManagerWaitingRoom").GetComponent<Serializer>();
    }

    public void SerializeObject()
    {
        serScript.SerializeXML();
    }
}
