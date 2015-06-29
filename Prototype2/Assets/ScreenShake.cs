using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{

    public enum Strength : int { Normal = 70, Strong = 100 };

    public int shakeDuration = 15;
    public float shakeIntesity = 0.7f;

    private Vector3 normalPos;
    private int shakeCount = 0;
    private ICallback cb = null;

    void Start()
    {
        normalPos = transform.position;
    }

    void Update()
    {
        if (shakeCount > 0)
        {
            shakeCount--;
            if (shakeCount <= 0)
            {
                transform.position = normalPos;
                if (cb != null)
                {
                    ICallback c = cb;
                    cb = null;
                    c.callback();
                }
            }
            else
            {
                Vector2 randoPos = Random.insideUnitCircle * shakeIntesity;
                transform.position = new Vector3(randoPos.x, randoPos.y, normalPos.z);
            }
        }
    }

    public void shake(Strength s, ICallback c = null)
    {
        shakeCount = shakeDuration;
        shakeIntesity = (int)s / 100.0f;
        cb = c;
    }
}
