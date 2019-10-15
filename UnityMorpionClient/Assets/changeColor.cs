using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Material[] colors;
    Renderer renderer;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = colors[2];
    }

    public void DrawBlank()
    {
        renderer.sharedMaterial = colors[2];
    }
    public void DrawCross()
    {
        renderer.sharedMaterial = colors[0];
        Debug.Log("cross");
    }
    public void DrawCircle()
    {
        renderer.sharedMaterial = colors[1];
    }

}
