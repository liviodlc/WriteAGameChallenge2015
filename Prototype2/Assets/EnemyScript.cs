using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour, ICallback {

    public Player player;
    public Sprite animStand;
    public Sprite animStandL;
    public Barrier barrier;
    public Barrier barrierL;

    private SpriteRenderer sr;
    private bool isFacingRight;
    private float barrierOffset = 0.25f;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x > transform.position.x)
        {
            isFacingRight = true;
            sr.sprite = animStand;
        }
        else
        {
            isFacingRight = false;
            sr.sprite = animStandL;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(isFacingRight)
            {
                barrier.activate(transform.position + new Vector3(barrierOffset, 0, 0), this);
            }
            else
            {
                barrierL.activate(transform.position - new Vector3(barrierOffset, 0, 0), this);
            }
        }
    }

    public void callback()
    {
        player.currentMove = Player.Move.Retreat;
    }
}
