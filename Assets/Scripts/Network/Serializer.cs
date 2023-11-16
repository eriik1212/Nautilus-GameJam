using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class Serializer : MonoBehaviour
{
    static MemoryStream stream;
    static MemoryStream clientStream;
    static MemoryStream hostStream;
    bool a = true;

    // STORE VARIABLES
    static public string clientNameXML;
    static public string hostNameXML;
    static public string roomNameXML;

    // Update is called once per frame
    void Update()
    {
        //if (a)
        //{
        //    //SerializeXML();
        //    //DeserializeXML();
        //    a = false;
        //}
    }

    public class UsefulData
    {
        //public int hp = 12;
        //public List<int> pos = new List<int> { 3, 3, 3 };

        public string clientName;
        public string hostName;
        public string roomName;
    }


    ///Cambiar lo que mandas. quitar hp e introducir las dadas dentro de serializeXML()
    public byte[] SerializeClientDataXML()
    {
        var t = new UsefulData();
        t.clientName = UpdatedText.ClientUsernameString;
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        clientStream = new MemoryStream();
        serializer.Serialize(clientStream, t);
        bytes = clientStream.ToArray();
        return bytes;
        
    }  
    
    ///Cambiar lo que mandas. quitar hp e introducir las dadas dentro de serializeXML()
    public byte[] SerializeHostDataXML()
    {
        var t = new UsefulData();
        t.hostName = UpdatedText.HostUsernameString;
        t.roomName = UpdatedText.roomNameString;
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        hostStream = new MemoryStream();
        serializer.Serialize(hostStream, t);
        bytes = hostStream.ToArray();
        return bytes;
        
    }
    public void DeserializeHostDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        var t = new UsefulData();
        hostStream = new MemoryStream();
        hostStream.Write(bytes, 0, bytes.Length);
        hostStream.Seek(0, SeekOrigin.Begin);
        t = (UsefulData)serializer.Deserialize(hostStream);
        hostNameXML = t.hostName;
        roomNameXML = t.roomName;

        Debug.Log("Xml: " + t.hostName + t.roomName);
    }
    public void DeserializeClientDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        var t = new UsefulData();
        clientStream = new MemoryStream();
        clientStream.Write(bytes, 0, bytes.Length);
        clientStream.Seek(0, SeekOrigin.Begin);
        t = (UsefulData)serializer.Deserialize(clientStream);
        clientNameXML = t.clientName;

        Debug.Log("Xml: " + t.clientName);
    }
    byte[] bytes;

}
