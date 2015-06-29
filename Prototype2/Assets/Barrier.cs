using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour, ICallback {

    public Barrier other;
    public ScreenShake cam;

    private int duration = 30;
    private int frameCount = 0;
    private ICallback cb;

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

    public void activate(Vector3 pos, ICallback c)
    {
        transform.position = pos;
        activated = true;
        cb = c;
        cam.shake(ScreenShake.Strength.Normal, this);
    }

    public void callback()
    {
        cb.callback();
        cam.shake(ScreenShake.Strength.Strong);
    }
}
