using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlDisplay : MonoBehaviour {

    public Player.Move myMove;
    public Direction[] dlist = { Direction.Up, Direction.Right };
    public GameObject arrowPrefab;
    public bool active = false;

    public Color normalColor = new Color(173f, 173f, 173f);
    public Color highlightedColor = new Color(255, 237, 0);

    private GameObject[] arrowList;
    private uint a = 0;

    private float keySpacing = 2f;

	// Use this for initialization
	void Start () {
        arrowList = new GameObject[dlist.Length];

        float i = GetComponent<RectTransform>().sizeDelta.x + 30f;
        foreach(Direction d in dlist)
        {
            GameObject key = Instantiate(arrowPrefab);
            arrowList[a] = key;
            a++;
            ArrowKeys akey = key.GetComponent<ArrowKeys>();
            akey.Init();
            akey.direction = d;

            key.transform.SetParent(transform, false);
            RectTransform keyTrans = key.GetComponent<RectTransform>();
            keyTrans.localPosition = new Vector3(i, keyTrans.sizeDelta.y / 2 + 2f, 0);
            i += keyTrans.sizeDelta.x + keySpacing;
        }

        a = 0;
	}

    /**
     * Returns true if any key is currently highlighted
     */
    public bool keyPress(Direction d, Player p)
    {
        if(active)
        {
            if(a < dlist.Length && dlist[a] == d)
            {
                arrowList[a].GetComponent<ArrowKeys>().select();
                a++;
                if(a==dlist.Length)
                {
                    changeColor(highlightedColor);
                    p.doMove(myMove);
                }
                return true;
            }
            else
            {
                deselectAll();
                return false;
            }
        }
        return false;
    }

    public void deselectAll(bool reactivate = false)
    {
        a = 0;
        changeColor(normalColor);
        foreach (GameObject arrow in arrowList)
            arrow.GetComponent<ArrowKeys>().deselect();
        active = reactivate;
    }

    private void changeColor(Color c)
    {
        GetComponent<Text>().color = c;
    }
}
