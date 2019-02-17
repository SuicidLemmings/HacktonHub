using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMissile : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        GameControl.instance.DecreaseMissile();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 30)
            Destroy(me);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(me);
    }
}
