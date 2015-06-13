using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

    public int shakeDuration = 15;
    public float shakeIntesity = 0.7f;
    private Vector3 normalPos;
    private int shakeCount = 0;

	void Start () {
        normalPos = transform.position;
	}
	
	void Update () {
	    if(shakeCount > 0)
       {
            shakeCount--;
            if (shakeCount <= 0)
            {
                transform.position = normalPos;
            }
            else
            {
                Vector2 randoPos = Random.insideUnitCircle * shakeIntesity;
                transform.position = new Vector3(randoPos.x, randoPos.y, normalPos.z);
            }
        }
	}

    public void shake()
    {
        shakeCount = shakeDuration;
    }
}
