using System;
using UnityEngine;
using System.Collections;
using EZ_Pooling;

public class PlayerController : MonoBehaviour
{

    public float MaxSpeed;
    public float Acceleration;
    public float RotationSpeed;
    public float DelayBetweenShots;
    public Transform Bullet;
    public float ShotSpeed;

    private float _timeSinceLastShot;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // movement 
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.angularVelocity = 0; // clear any rotation caused by collisions 
            rigidbody.rotation += RotationSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.angularVelocity = 0; // clear any rotation caused by collisions
            rigidbody.rotation -= RotationSpeed;
        }
	    
	    if (Input.GetKey(KeyCode.UpArrow))
        {
            // apply thrust             
            rigidbody.AddForce(transform.up * Acceleration * Time.deltaTime);

            // limit speed
            if (rigidbody.velocity.magnitude > MaxSpeed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
            }

            // show engine boost particle effect
            transform.Find("RightThrustPE").GetComponent<ParticleSystem>().Play();
            transform.Find("LeftThrustPE").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            // turn off engine particle effect
            transform.Find("RightThrustPE").GetComponent<ParticleSystem>().Stop();
            transform.Find("LeftThrustPE").GetComponent<ParticleSystem>().Stop();
        }

        //shooting
	    _timeSinceLastShot += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (_timeSinceLastShot > DelayBetweenShots)
            {
                //shoot!
                _timeSinceLastShot = 0f;

                Transform bullet = EZ_PoolManager.Spawn(Bullet, transform.position, transform.rotation);

                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidBody.velocity = transform.up * ShotSpeed;

            }
        }
	}
}
