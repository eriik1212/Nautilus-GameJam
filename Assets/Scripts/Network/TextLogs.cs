using UnityEngine;
using UnityEngine.UI;

public class TextLogs : MonoBehaviour
{
    private Text logText;

    private void Start()
    {
        logText = GetComponent<Text>();
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logText, string stackTrace, LogType type)
    {
        // Puedes personalizar el formato del mensaje según tus necesidades.
        string logMessage = $"{type}: {logText}";

        // Agregar el mensaje al objeto de texto.
        this.logText.text += logMessage + "\n";
    }
}
