using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowKeys : MonoBehaviour {

    public Texture arrowKey;
    public Texture arrowKeyHL;

    private Direction dir;
    public Direction direction
    {
        get{
            return dir;
        } 
        set{
            dir = value;
            switch(dir)
            {
                case Direction.Up:
                    this.GetComponent<RectTransform>().transform.Rotate(0, 0, 90);
                    break;
                case Direction.Down:
                    this.GetComponent<RectTransform>().transform.Rotate(0, 0, -90);
                    break;
                case Direction.Left:
                    this.GetComponent<RectTransform>().transform.Rotate(0, 0, 180);
                    break;
            }
        }
    }

    private RawImage img;

    public void Init()
    {
        img = GetComponent<RawImage>();
    }

    public void select()
    {
        img.texture = arrowKeyHL;
    }


    public void deselect()
    {
        img.texture = arrowKey;
    }
}
