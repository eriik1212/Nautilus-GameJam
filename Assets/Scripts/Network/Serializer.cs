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
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        return bytes;
        
    }  
    
    ///Cambiar lo que mandas. quitar hp e introducir las dadas dentro de serializeXML()
    public byte[] SerializeHostDataXML()
    {
        var t = new UsefulData();
        t.hostName = UpdatedText.HostUsernameString;
        t.roomName = UpdatedText.roomNameString;
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        return bytes;
        
    }
    public void DeserializeClientDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        var t = new UsefulData();
        stream = new MemoryStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        t = (UsefulData)serializer.Deserialize(stream);
        hostNameXML = t.hostName;
        roomNameXML = t.roomName;

        Debug.Log("Xml: " + t.hostName + t.roomName);
    }
    public void DeserializeHostDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        var t = new UsefulData();
        stream = new MemoryStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        t = (UsefulData)serializer.Deserialize(stream);
        clientNameXML = t.clientName;

        Debug.Log("Xml: " + t.clientName);
    }
    byte[] bytes;

}
