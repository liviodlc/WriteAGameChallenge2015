using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

    public Barrier other;
    public ScreenShake cam;

    private int duration = 30;
    private int frameCount = 0;

    private Vector3 hiddenPos;
    private bool activated = false;

	void Start () {
	    hiddenPos = transform.position;
	}
	
	void Update () {
	    if(activated)
        {
            frameCount++;
            if(frameCount >= duration)
            {
                transform.position = hiddenPos;
                frameCount = 0;
                activated = false;
            }
        }
	}

    public void activate(Vector3 pos)
    {
        transform.position = pos;
        activated = true;
        cam.shake();
    }
}
