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
    static MemoryStream boyStream;
    static MemoryStream girlStream;
    bool a = true;

    // STORE VARIABLES
    static public string clientNameXML;
    static public string hostNameXML;
    static public string roomNameXML;
    static public bool playButtonPressed;

    // Boy
    static public Vector2 boyPositionXML;
    static public bool boyAttackXML;
    static public bool boyJumpingXML;
    static public int boyOrientationXML;

    // Girl
    static public Vector2 girlPositionXML;
    static public bool girlAttackXML;
    static public bool girlJumpingXML;
    static public int girlOrientationXML;


    // ------------------------------------------------------------------------------------------ IN-GAME DATA
    public class InGameData
    {
        public Vector2 boyPos;
        public Vector2 girlPos;

        public bool boyAttackData;
        public bool girlAttackData;

        public bool boyJumpingData;
        public bool girlJumpingData;

        public int boyOrientationData;
        public int girlOrientationData;
    }
    public byte[] BoyDataSerialize()
    {
        var t = new InGameData();
        t.boyPos = BoyData.boyPosition;
        t.boyAttackData = BoyData.boyAttack;
        t.boyJumpingData = BoyData.isBoyJumping;
        t.boyOrientationData = BoyData.boyOrientation;
        XmlSerializer serializer = new XmlSerializer(typeof(InGameData));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        Debug.Log("--BOY-- Data to serialize: POS: " + t.boyPos + " ATTACK: " + t.boyAttackData);
        return bytes;
    }

    public void DeserializeBoyDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(InGameData));
        var t = new InGameData();
        boyStream = new MemoryStream();
        boyStream.Write(bytes, 0, bytes.Length);
        boyStream.Seek(0, SeekOrigin.Begin);
        t = (InGameData)serializer.Deserialize(boyStream);
        boyPositionXML = t.boyPos;
        boyAttackXML = t.boyAttackData;
        boyJumpingXML = t.boyJumpingData;
        boyOrientationXML = t.boyOrientationData;
    }

    public byte[] GirlDataSerialize()
    {
        var t = new InGameData();
        t.girlPos = GirlData.girlPosition;
        t.girlAttackData = GirlData.girlAttack;
        t.girlJumpingData = GirlData.isGirlJumping;
        t.girlOrientationData = GirlData.girlOrientation;
        XmlSerializer serializer = new XmlSerializer(typeof(InGameData));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        Debug.Log("--GIRL-- Data to serialize: POS: " + t.girlPos + " ATTACK: " + t.girlAttackData);
        return bytes;
    }

    public void DeserializeGirlDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(InGameData));
        var t = new InGameData();
        girlStream = new MemoryStream();
        girlStream.Write(bytes, 0, bytes.Length);
        girlStream.Seek(0, SeekOrigin.Begin);
        t = (InGameData)serializer.Deserialize(girlStream);
        girlPositionXML = t.girlPos;
        girlAttackXML = t.girlAttackData;
        girlJumpingXML = t.girlJumpingData;
        girlOrientationXML = t.girlOrientationData;
    }

    // ------------------------------------------------------------------------------------------ WAITING ROOM DATA
    public class WaitingRoomData
    {
        public string clientName;
        public string hostName;
        public string roomName;

        public bool playPressed = false;
    }

    public byte[] PlayButtonSerialize()
    {
        var t = new WaitingRoomData();
        t.playPressed = PlayManagement.playPres;
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        stream = new MemoryStream();
        serializer.Serialize(stream, t);
        bytes = stream.ToArray();
        return bytes;
    }
    public void PlayButtonDeserialize(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        var t = new WaitingRoomData();
        stream = new MemoryStream();
        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        t = (WaitingRoomData)serializer.Deserialize(stream);
        playButtonPressed = t.playPressed;

        Debug.Log("Xml: " + t.playPressed);
    }

    public byte[] SerializeClientDataXML()
    {
        var t = new WaitingRoomData();
        t.clientName = UpdatedText.ClientUsernameString;
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        clientStream = new MemoryStream();
        serializer.Serialize(clientStream, t);
        bytes = clientStream.ToArray();
        return bytes;
        
    }  
    
    public byte[] SerializeHostDataXML()
    {
        var t = new WaitingRoomData();
        t.hostName = UpdatedText.HostUsernameString;
        t.roomName = UpdatedText.roomNameString;
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        hostStream = new MemoryStream();
        serializer.Serialize(hostStream, t);
        bytes = hostStream.ToArray();
        return bytes;
        
    }
    public void DeserializeHostDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        var t = new WaitingRoomData();
        hostStream = new MemoryStream();
        hostStream.Write(bytes, 0, bytes.Length);
        hostStream.Seek(0, SeekOrigin.Begin);
        t = (WaitingRoomData)serializer.Deserialize(hostStream);
        hostNameXML = t.hostName;
        roomNameXML = t.roomName;

        Debug.Log("Xml: " + t.hostName + t.roomName);
    }
    public void DeserializeClientDataXML(byte[] bytes)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WaitingRoomData));
        var t = new WaitingRoomData();
        clientStream = new MemoryStream();
        clientStream.Write(bytes, 0, bytes.Length);
        clientStream.Seek(0, SeekOrigin.Begin);
        t = (WaitingRoomData)serializer.Deserialize(clientStream);
        clientNameXML = t.clientName;

        Debug.Log("Xml: " + t.clientName);
    }


    byte[] bytes;

}

public class RemoteInputs
{
    public bool Apressed = false;
    public bool Wpressed = false;
    public bool Dpressed = false;
    public bool spacePressed = false;
    public bool shiftPressed = false;

    public void Reset()
    {
        Apressed = false;
        Wpressed = false;
        Dpressed = false;
        spacePressed = false;
        shiftPressed = false;
    }
}