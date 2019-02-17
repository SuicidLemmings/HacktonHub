using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour 
{
    public GameObject me;
    public float xToDestroy;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<Bird>() != null)
		{
			//If the bird hits the trigger collider in between the columns then
			//tell the game control that the bird scored.
			GameControl.instance.BirdScored();
		}
	}

    void Update()
    {
        if (transform.position.x < xToDestroy)
        {
            Destroy(me);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Missile")
            Destroy(me);
    }
}
