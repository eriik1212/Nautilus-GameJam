using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextLogs : MonoBehaviour
{
    public TMP_Text logText;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logText, string stackTrace, LogType type)
    {
        // Puedes personalizar el formato del mensaje según tus necesidades.
        string logMessage = $"{type}: {logText}";

        // Agregar el mensaje al objeto de texto.
        this.logText.text += logMessage + "\n";
    }
}
