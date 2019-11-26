﻿using UnityEngine;
using Morpion3Dimension.Model;
//using Morpion3Dimension.UnityClient;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class Antenna : MonoBehaviour, IGraphics
{
    public bool moveNeeded = false;
    public Text logging;

    public delegate void DisplayGrid(Morpion3Dimension.Model.Grid grid);
    public event DisplayGrid DisplayNewGridEvent;

    public delegate void GameOver(GameOverMessage message);
    public event GameOver WinEvent;

    ConnectionClient client;

    [SerializeField]
    public Material[] colors;

    void Start()
    {
        Screen.SetResolution(512*2, 348*2, true);
        Screen.fullScreen = false;
    }
    void Awake()
    {
        logging = GameObject.Find("Text").GetComponent<Text>();
        logging.text += ("antenna awake");
        this.client = Variables.client;
        this.client.graphics = this;

        client.SendReady();
    }
    private void OnApplicationQuit()
    {
        client.Close();
    }

    public void AskMove()
    {
        moveNeeded = true;
        logging.text += ("it's your turn");
        Debug.Log("I was asked a move");
    }

    public void DisplayGameOver(GameOverMessage gameOverMessage)
    {
        if (gameOverMessage.winType == WinType.win)
        {
            if (WinEvent != null)
                WinEvent(gameOverMessage);
        }

        if (gameOverMessage.winType == WinType.lose)
        {
            if (WinEvent != null)
                WinEvent(gameOverMessage);
        }
    }

    public void DisplayNewGrid(Morpion3Dimension.Model.Grid grid)
    {
        if (DisplayNewGridEvent != null)
            DisplayNewGridEvent(grid);
    }

    public void SendMove(Move move)
    {
        //logging.PrintToConsole("Sending a move from antenna");
        client.SendMove(move);
        moveNeeded = false;
    }

    
    public void PrintToConsole(string message) 
    {
        logging.text += message + "\n";
    }
}


//===================================================================================================
public class ConnectionClient
{
    public IGraphics graphics;

    public IPAddress serverIP;
    public int port = 8080;
    public TcpClient client = new TcpClient();
    public byte[] readBuffer = new byte[1024];
    public NetworkStream stream;

    public ConnectionClient(IGraphics graphics)
    {
        this.graphics = graphics;
        this.serverIP = IPAddress.Parse("127.0.0.1");
    }

    public ConnectionClient(IGraphics graphics, IPAddress address)
    {
        this.graphics = graphics;
        this.serverIP = address;
    }

    public void Connect()
    {
        //client.BeginConnect(serverIP, port, (ar) => ConnectCallback(ar), client);
        var ipe = new IPEndPoint(serverIP, port);
        client.Connect(ipe);
        stream = client.GetStream();
        stream.BeginRead(readBuffer, 0, readBuffer.Length, (ar4) => OnRead(ar4), null);
        Debug.Log("began reading");
    }

    public void OnRead(IAsyncResult ar)
    {
        Debug.Log("OnRead called");
        int length = stream.EndRead(ar);

        byte[] responseData = readBuffer.Take(length).ToArray();

        graphics.PrintToConsole($"onread : stream length = {length}");
        graphics.PrintToConsole($"onread : received : {Encoding.UTF8.GetString(responseData)}");
        stream.BeginRead(readBuffer, 0, readBuffer.Length, (ar3) => OnRead(ar3), null);

        HandleMessage(responseData);
    }

    private void HandleMessage(byte[] data)
    {
        MessageType type = Message.GetMessageType(data);
        string DataAsText = Encoding.UTF8.GetString(data);

        Debug.Log($"received from server : {DataAsText}");

        switch (type)
        {
            case MessageType.move:
                graphics.AskMove();
                Debug.Log("asked a move");
                break;
            case MessageType.gameOver:
                GameOverMessage gameOverMessage = new GameOverMessage(data);
                graphics.DisplayGameOver(gameOverMessage);
                break;
            case MessageType.grid:
                var grid = new Morpion3Dimension.Model.Grid(data);
                graphics.DisplayNewGrid(grid);
                break;
            default:
                Debug.Log(Encoding.UTF8.GetString(data));
                break;
        }
    }
    public void SendMove(Move move)
    {
        byte[] data = Encoding.UTF8.GetBytes(move.MessageToString());
        stream.Write(data, 0, data.Length);
    }

    public void SendReady()
    {
        byte[] data = Encoding.UTF8.GetBytes("Client Ready");
        stream.Write(data, 0, data.Length);
    }

    public void Close() { client.Close(); }
}


public interface IGraphics
{
    void AskMove();
    void DisplayGameOver(Morpion3Dimension.Model.GameOverMessage gameOverMessage);
    void DisplayNewGrid(Morpion3Dimension.Model.Grid grid);
    void SendMove(Morpion3Dimension.Model.Move move);
    void PrintToConsole(string message);
}



