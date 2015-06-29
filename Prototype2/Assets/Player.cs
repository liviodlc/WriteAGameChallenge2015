using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum Move { None, DashAttack, JumpAttack, Dodge, Block, Retreat, Stand, Start };

    [System.Serializable]
    public class PlayerSprites
    {
        public Sprite animStanding;
        public Sprite animStandingL;
        public Sprite animDashing;
        public Sprite animDashingL;
        public Sprite animJump;
        public Sprite animJumpL;
    }
    public PlayerSprites sprites;
    public float dashSpeed = 5f;
    public float jumpSpeed = 20f;
    public float friction = 1f;
    public GameObject dashingPrefab;
    public DialogueMan dman;

    private Move cMove = Move.Start;
    public Move currentMove
    {
        get { return cMove; }
        set {
            cMove = value;
            if(cMove == Move.None)
            {
                dman.showControls();
            }
            else if (cMove == Move.Retreat)
            {
                rb.velocity = new Vector2(0, jumpSpeed / 2.5f);
                buffer = 5;
            }
        }
    }
    //private Vector3 startPos;
    private int buffer;

    private SpriteRenderer sr;
    private float distToFeet;
    private float groundCheckDistance;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
   //private bool wasDashing = false;

	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //startPos = transform.position;

        distToFeet = GetComponent<BoxCollider2D>().bounds.extents.y;
        groundCheckDistance = distToFeet + 0.3f;
	}

    public void doMove(Move m)
    {
        currentMove = m;
        dman.hideControls();
    }

    private bool isStanding()
    {
        //int layer = 1 << 8;
        //bool hit = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckDistance, layer);
        //Color color = hit ? Color.green : Color.red;
        //Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckDistance, 0));
        //return hit;
        return Physics2D.Raycast(transform.position, -Vector2.up, groundCheckDistance, 1 << 8);
    }

    private Sprite anim
    {
        set
        {
            sr.sprite = value;
        }
        get
        {
            return sr.sprite;
        }
    }

    void FixedUpdate()
    {
        switch(currentMove)
        {
            case Move.Start:
                if(isStanding())
                {
                    currentMove = Move.None;
                }
                break;
            case Move.None:
                //if (isFacingRight)
                //{
                    //if (Input.GetKey(KeyCode.RightArrow))
                    //{
                    //    currentMove = Move.DashAttack;
                    //    dman.hideControls();
                    //}
                    //else if (Input.GetKey(KeyCode.LeftArrow))
                    //{
                    //    currentMove = Move.Block;
                    //    dman.hideControls();
                    //}
                //}
                //else
                //{
                    //if (Input.GetKey(KeyCode.LeftArrow))
                    //{
                    //    currentMove = Move.DashAttack;
                    //    dman.hideControls();
                    //}
                    //else if (Input.GetKey(KeyCode.RightArrow))
                    //{
                    //    currentMove = Move.Block;
                    //    dman.hideControls();
                    //}
                //}
                break;
            case Move.DashAttack:
                if(isFacingRight)
                {
                    anim = sprites.animDashing;
                    rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                }
                else
                {
                    anim = sprites.animDashingL;
                    rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                }
                break;
            case Move.Retreat:
                if (isFacingRight)
                {
                    anim = sprites.animJump;
                    rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                    if(buffer > 0)
                        buffer--;
                    if (buffer <= 0 && isStanding())
                    {
                        currentMove = Move.Stand;
                    }
                }
                break;
            case Move.Stand:
                if(isFacingRight)
                    anim = sprites.animStanding;
                else
                    anim = sprites.animStandingL;
                if (rb.velocity.x > 0)
                {
                    float newSpeed = rb.velocity.x - friction;
                    if (newSpeed < 0)
                    {
                        newSpeed = 0;
                        currentMove = Move.None;
                    }
                    rb.velocity = new Vector2(newSpeed, rb.velocity.y);
                }
                else if (rb.velocity.x < 0)
                {
                    float newSpeed = rb.velocity.x + friction;
                    if (newSpeed > 0)
                    {
                        newSpeed = 0;
                        currentMove = Move.None;
                    }
                    rb.velocity = new Vector2(newSpeed, rb.velocity.y);
                }
                else
                {
                    currentMove = Move.None;
                }
                break;
        }
/*
        bool isDashing = false;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            if (!wasDashing)
            {
                Instantiate(dashingPrefab, transform.position + new Vector3(0, -distToFeet, 0), transform.rotation);
            }
            anim = sprites.animDashing;
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
            isDashing = true;
            isFacingRight = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            anim = sprites.animDashingL;
            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
            isDashing = true;
            isFacingRight = false;
        }
        else if(rb.velocity.x > 0)
        {
            float newSpeed = rb.velocity.x - friction;
            if(newSpeed < 0)
                newSpeed = 0;
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < 0)
        {
            float newSpeed = rb.velocity.x + friction;
            if (newSpeed > 0)
                newSpeed = 0;
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);
        }

        if (isStanding())
        {
            if(!isDashing)
            {
                if(isFacingRight)
                    anim = sprites.animStanding;
                else
                    anim = sprites.animStandingL;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
        else if(!isDashing)
        {
            if (isFacingRight)
                anim = sprites.animJump;
            else
                anim = sprites.animJumpL;
        }
        wasDashing = isDashing;
 */
    }
}
