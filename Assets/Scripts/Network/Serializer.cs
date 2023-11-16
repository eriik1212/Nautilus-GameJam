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

    // Start is called before the first frame update
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            SerializeXML();
            //DeserializeXML();
            a = false;
        }
    }

    public class UsefulData
    {
        //public int hp = 12;
        //public List<int> pos = new List<int> { 3, 3, 3 };

        public string clientName;
    }


    ///Cambiar lo que mandas. quitar hp e introducir las dadas dentro de serializeXML()
    public byte[] SerializeXML()
    {
        var t = new UsefulData();
        t.clientName = UpdatedText.ClientUsernameString;
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        return bytes;
        //Aqui conectar a un pointer de bytes en la clase data sender
    }
    public void DeserializeXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UsefulData));
        var t = new UsefulData();
        stream = new MemoryStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        t = (UsefulData)serializer.Deserialize(stream);
        Debug.Log("Xml: " + t.clientName);
    }
    byte[] bytes;

}
