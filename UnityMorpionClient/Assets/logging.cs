using UnityEngine;
using UnityEngine.UI;

public class logging : MonoBehaviour
{
    public Text log;

    public void PrintToConsole(string message)
    {
        //log.text = log.text + message + "\n";
        Debug.Log(message);
    }
}
