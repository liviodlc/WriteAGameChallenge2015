using UnityEngine;
using System.Collections;

public class DisappearAfterTime : MonoBehaviour {

    public float disappearAfterSeconds = 0.5f;

	void Start () {
	    Destroy(this.gameObject, disappearAfterSeconds);
	}
	
}
