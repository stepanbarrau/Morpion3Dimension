using System;
using System.Collections;
using System.Collections.Generic;
using Morpion3Dimension.Model;
using UnityEngine;
using UnityEngine.UI;

public class restart_button : MonoBehaviour
{
    private GameObject button;
    private Canvas canvas;
    private Antenna antenna;
    private void Awake()
    {
        Debug.Log("started button");

        canvas = GameObject.Find("RestartCanvas").GetComponent<Canvas>();
        canvas.enabled = false;

        

        //button = GameObject.Find("restart_Button");
        //button.SetActive(false);

        antenna = GameObject.Find("AntennaObject").GetComponent<Antenna>();
        antenna.WinEvent += OnGameOverEvent;
    }

    private void OnGameOverEvent(GameOverMessage message)
    {
        canvas.enabled = true;
    }

    public void Restart()
    {
        Debug.Log("pressed restart button");
        UnityEngine.SceneManagement.SceneManager.LoadScene("chooseServer");
    }
}
