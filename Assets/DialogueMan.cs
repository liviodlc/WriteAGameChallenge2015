using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueMan : MonoBehaviour {

    private class DialogueEntry
    {
        public string text;
        public bool forPlayer;
        public int delay;
        public int expire;

        public DialogueEntry(bool _forPlayer, int _delay, int _expire, string _text)
        {
            text = _text;
            forPlayer = _forPlayer;
            delay = _delay * 30;
            expire = _expire * 30;
        }
    }

    public Text princess;
    public Text uncle;
    public Player player;

    private DialogueEntry nextLine;
    private int i = 0;
    private int waitForIt = 0;
    private bool noMoreLines = false;
    private int waitForExpire = 0;
    private bool expirePlayer = false;

    private DialogueEntry[] dialogue =
        {
        new DialogueEntry(true, 1, 5, "This ends here!"),
        new DialogueEntry(false, 1, 3, "You'll never defeat me!"),
        new DialogueEntry(true, 1, 3, "Yes I will!"),
        new DialogueEntry(false, 1, 3, "No you won't!"),
        new DialogueEntry(true, 1, 3, "I'll never give up!"),
        new DialogueEntry(false, 1, 3, "You're weak!"),
        new DialogueEntry(true, 1, 5, "As princess, it's my duty to protect this kingdom!"),
        new DialogueEntry(false, 1, 4, "Fool!")
        };

	void Start () {
        nextLine = dialogue[i];
        princess.text = "";
        uncle.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (waitForExpire > 0)
        {
            waitForExpire--;
            if (waitForExpire == 0)
            {
                if (expirePlayer)
                {
                    princess.text = "";
                }
                else
                {
                    uncle.text = "";
                }

                waitForIt = 0;
                i++;
                if (i >= dialogue.Length)
                {
                    noMoreLines = true;
                }
                else
                {
                    nextLine = dialogue[i];
                }
            }
        }
        else
        {
            if (!noMoreLines && waitForIt >= nextLine.delay)
            {
                if (nextLine.forPlayer)
                {
                    princess.text = nextLine.text;
                    ////print(princess.transform.position);
                    //princess.transform.position = new Vector3(Camera.main.WorldToViewportPoint(player.transform.position).x, princess.transform.position.y, princess.transform.position.z);
                    ////print(player.transform.position.x / 10.2f * 500f);
                    //print(princess.transform.position);
                }
                else
                {
                    uncle.text = nextLine.text;
                }
                waitForExpire = nextLine.expire;
                expirePlayer = nextLine.forPlayer;
            }
            waitForIt++;
        }
	}
}
