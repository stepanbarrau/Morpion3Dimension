using System.Collections;
using System.Collections.Generic;
using System.Net;
using Morpion3Dimension.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServerIPInput : MonoBehaviour
{
    Text output;
    string prefixMessage = "Please Enter the IP address of the server you'd like to connect to. \n\n";
    ConnectionClient client;
    void Start()
    {
        gameObject.GetComponent<InputField>().onEndEdit.AddListener(readIP);
        output = GameObject.Find("OutputText").GetComponent<Text>();

        //or simply use the line below, 
        //input.onEndEdit.AddListener(SubmitName);  // This also works
    }
    private void readIP(string textInField)
    {
        IPAddress serverIP;
        try
        {
            serverIP = IPAddress.Parse(textInField);
        }
        catch
        {
            output.text = prefixMessage + "Please enter a valid IP";
            return;
        }
        ConnectionClient client_ = new ConnectionClient(new DummyGraphics(), serverIP);
        try
        {
            client_.Connect();
        }
        catch
        {
            output.text = prefixMessage + "Sorry, couldn't connect to server, is the address you chose correct?";
            return;
        }

        Variables.client = client_;

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    class DummyGraphics : IGraphics
    {
        public void AskMove() { }
        public void DisplayGameOver(GameOverMessage gameOverMessage) { }

        public void DisplayNewGrid(Morpion3Dimension.Model.Grid grid){ }

        public void PrintToConsole(string message) { }

        public void SendMove(Move move) { }
    }
}
