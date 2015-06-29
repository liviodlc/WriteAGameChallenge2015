using UnityEngine;
using System.Collections;

public class ControlsContainer : MonoBehaviour {

    public Player p;
    public ControlDisplay[] moveList;
    public char[] moveValues;

    public Vector3 origin;
    private Vector3 offscreen = new Vector3(5000, 5000, 0);
    private Vector3 i;


    private bool vis = true;
    public bool visible
    {
        get { return vis; }
        set
        {
            vis = value;
            if (vis)
            {
                transform.localPosition = origin;

            }
            else
            {
                transform.localPosition = offscreen;
                foreach (ControlDisplay m in moveList)
                    m.deselectAll();
            }
        }
    }

    public void Start()
    {
        showMoves("dbj");
    }

    /**
    * d = dash attack
    * b = block
    * j = jump attack
    */
    public void showMoves(string moves)
    {
        i = new Vector3(-30f, 50f, 0);

        uint j = 0;
        foreach(ControlDisplay m in moveList)
        {
            char c = moveValues[j];

            if (moves.IndexOf(c) != -1)
                show(m);
            else
                hide(m);

            j++;
        }
    }

    private void show(ControlDisplay move)
    {
        move.transform.localPosition = i; 
        i = new Vector3(i.x, i.y - move.GetComponent<RectTransform>().sizeDelta.y - 10f, i.z);
        move.active = true;
    }

    private void hide(ControlDisplay move)
    {
        move.transform.localPosition = offscreen;
        move.active = false;
    }

    public void Update()
    {
        if(visible && Input.anyKeyDown)
        {
            Direction keyPress = Direction.None;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                keyPress = Direction.Right;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                keyPress = Direction.Left;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                keyPress = Direction.Down;
            if (Input.GetKeyDown(KeyCode.UpArrow))
                keyPress = Direction.Up;

            bool goodKeyPressed = false;
            foreach(ControlDisplay m in moveList)
                goodKeyPressed = goodKeyPressed | m.keyPress(keyPress, p);

            if (!goodKeyPressed)
                foreach (ControlDisplay m in moveList) 
                    m.deselectAll(true);
        }
    }
}
