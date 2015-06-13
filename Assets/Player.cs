using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Sprite animStanding;
    public Sprite animStandingL;
    public Sprite animDashing;
    public Sprite animDashingL;
    public Sprite animJump;
    public Sprite animJumpL;
    public float dashSpeed = 5f;
    public float jumpSpeed = 20f;
    public float friction = 1f;

    private SpriteRenderer sr;
    private float groundCheckDistance;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        groundCheckDistance = GetComponent<BoxCollider2D>().bounds.extents.y + 0.3f;
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

    void Update()
    {
        bool isDashing = false;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            anim = animDashing;
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
            isDashing = true;
            isFacingRight = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            
            anim = animDashingL;
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
                    anim = animStanding;
                else
                    anim = animStandingL;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
        else if(!isDashing)
        {
            if (isFacingRight)
                anim = animJump;
            else
                anim = animJumpL;
        }
    }
	
	void FixedUpdate ()
    {
       
	}
}
