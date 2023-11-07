using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class serilizer : MonoBehaviour
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
    
            serializeXML();
            deserializeXML();
            a = false;
        }
    }

    public class testClass
    {
        public int hp = 12;
        public List<int> pos = new List<int> { 3, 3, 3 };
    }

   
    ///Cambiar lo que mandas. quitar hp e introducir las dadas dentro de serializeXML()
    public byte[] serializeXML()
    {
        var t = new testClass();
        t.hp = 40;
        t.pos = new List<int> { 10, 3, 12 };
        XmlSerializer serializer = new XmlSerializer(typeof(testClass));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        return bytes;
        //Aqui conectar a un pointer de bytes en la clase data sender
    }
    void deserializeXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(testClass));
        var t = new testClass();
        stream = new MemoryStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        t = (testClass)serializer.Deserialize(stream);
        Debug.Log("Xml " + t.hp.ToString() + " " + t.pos.ToString());
    }
    byte[] bytes;
   
}
