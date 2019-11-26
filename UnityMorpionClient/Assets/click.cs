using System;
using UnityEngine;
using Morpion3Dimension.Model;

public class click : MonoBehaviour
{ 
    public int x;
    public int y;
    public int z;

    public Antenna antenna;
    logging logging;
    changeColor changeColor;
    bool animateWinSequence = false;
    Vector3 rotation = new Vector3(0, 1, 0);

    Ray ray;
    RaycastHit hit;

    void Awake()
    {
        logging = GameObject.Find("Text").GetComponent<logging>();
        antenna = GameObject.Find("AntennaObject").GetComponent<Antenna>();
        antenna.DisplayNewGridEvent += OnDisplayNewGridEvent;
        antenna.WinEvent += OnGameOverEvent;

        changeColor = gameObject.GetComponent<changeColor>();
        changeColor.colors = antenna.colors;

        SetCoord();
    }
    void SetCoord()
    {
        x = (int)Math.Round(transform.position.x/2);
        y = (int)Math.Round(transform.position.y/2);
        z = (int)Math.Round(transform.position.z/2);
    }

    private void OnMouseDown()
    {
        Debug.Log($"Clicked on cube {x}{y}{z}");
        if (antenna.moveNeeded) { SendMove(new Move(new int[] { x, y, z })); }
    }

    public void OnDisplayNewGridEvent(Morpion3Dimension.Model.Grid grid)
    {
        
        //Debug.Log($"trying to display grid at {x}{y}{z}");
        Square square = (Square)grid[x, y, z];
        Symbol symbol = square.symbol;
        if (symbol == Symbol.empty) { changeColor.DrawBlank(); }
        if (symbol == Symbol.circle) { changeColor.DrawCircle(); }
        if (symbol == Symbol.cross) { changeColor.DrawCross(); }

        //Debug.Log($"I (Cube {x}, {y}, {z}) am displaying a new grid");
    }

    public void OnGameOverEvent(GameOverMessage message)
    {
        Position[] winningSequence = message.winningSequence;
        foreach(Position p in winningSequence){
            if((x,y,z) == (p.x, p.y, p.z))
            {
                Debug.Log("make cube spin bc game over");
                this.animateWinSequence = true;
            }
        }
    }

    public void SendMove(Move move) 
    {
        Debug.Log($"Sending a move from cube {x}, {y}, {z}");
        antenna.SendMove(move);
    }

    private void OnMouseEnter()
    {
        if (antenna.moveNeeded && !changeColor.IsSet())
        {
            changeColor.DrawSelected();
        }
    }

    private void OnMouseExit()
    {
        if (antenna.moveNeeded && changeColor.IsSelected())
        {
            changeColor.DrawBlank();
        }
    }
    private void Update()
    {
        
        if (animateWinSequence)
        {
            transform.Rotate(rotation, 10);
        }
    }
}
