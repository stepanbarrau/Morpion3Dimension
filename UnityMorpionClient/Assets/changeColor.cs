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
    }
    public void DrawCircle()
    {
        renderer.sharedMaterial = colors[1];
    }

    public void DrawSelected()
    {
        renderer.sharedMaterial = colors[3];
    }

    public bool IsSelected()
    {
        return (renderer.sharedMaterial == colors[3]);
    }

    public bool IsSet() 
    { 
        return (renderer.sharedMaterial == colors[0] || renderer.sharedMaterial == colors[1]); 
    }
}
