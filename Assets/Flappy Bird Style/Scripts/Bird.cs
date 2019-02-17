using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;			//Has the player collided with a wall?
    public GameObject missile;

	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.
    private float timer1 = 0f;
    private float timer2 = 0f;
    private float timeBetweenCommand = 0.4f; // Seconds

    void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;
            //Look for input to trigger a "flap".
            if (GameControl.instance.GetComponent<SerialInputs>().getValues(GameControl.instance.GetComponent<SerialInputs>().JUMP) && timer1 > timeBetweenCommand)
            {
                timer1 = 0f;
                //...zero out the birds current y velocity before...
                rb2d.velocity = Vector2.zero;
				//	new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.
				rb2d.AddForce(new Vector2(0, upForce));
                GameControl.instance.GetComponent<SerialInputs>().setValues(GameControl.instance.GetComponent<SerialInputs>().JUMP, false);
            }
            if (GameControl.instance.GetMissileNumber() > 0 && GameControl.instance.GetComponent<SerialInputs>().getValues(GameControl.instance.GetComponent<SerialInputs>().FIRE) && timer2 > timeBetweenCommand)
            {
                timer2 = 0f;
                GameObject mymissile = GameObject.Instantiate(missile, transform.position + new Vector3(GetComponent<Renderer>().bounds.size.x, 0), Quaternion.identity);
                mymissile.GetComponent<Rigidbody2D>().velocity = new Vector2(mymissile.GetComponent<MyMissile>().Speed, 0);
                mymissile.GetComponent<MyMissile>().me = mymissile;
                GameControl.instance.GetComponent<SerialInputs>().setValues(GameControl.instance.GetComponent<SerialInputs>().FIRE, false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
	{
        if (other.transform.tag != "Pipe" && other.transform.tag != "Floor")
            return;
        // Zero out the bird's velocity
        rb2d.velocity = Vector2.zero;
		// If the bird collides with something set it to dead...
		isDead = true;
		//...and tell the game control about it.
		GameControl.instance.BirdDied ();
	}
}
