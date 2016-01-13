using UnityEngine;
using System.Collections;

public class Charger : MonoBehaviour {

    public float NormalSpeed = 5;
    public float ChargeSpeed = 30;
    public float NormalAcceleration = 3000;
    public float ChargeAcceleration = 10000;
    public float RotationSpeed = 180;
    public float ChargeDuration = 2;
    public float ChargeCooldown = 4;
    public float ChargeTriggerAngle = 10;

    bool Charging;
    float TimeSpentCharging;
    float TimeInCooldown;
    Transform player; 

	// Use this for initialization
	void Start () {
        Charging = false;
        TimeInCooldown = 0;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, NormalSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        if (!Charging)
        {
            // turn towards player
            GameObject go = GameObject.Find("Player");

            if (go != null)
            {
                player = go.transform;
            }

            if (player != null)
            {
                Vector3 dir = player.position - transform.position;
                dir.Normalize();
                float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;


                Quaternion desired = Quaternion.Euler(0, 0, zAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, RotationSpeed * Time.deltaTime);

                // if angle to player is small enough, charge!       
                float angle = Quaternion.Angle(desired, transform.rotation);

                Debug.Log("TimeInCooldown " + TimeInCooldown.ToString() + " Angle " + angle.ToString() +  (Charging ? "Charging" : ""));
                if (Mathf.Abs(angle) < ChargeTriggerAngle && TimeInCooldown > ChargeCooldown )
                {
                    Charging = true;
                    TimeSpentCharging = 0;
                }
                else
                {
                    TimeInCooldown += Time.deltaTime;
                }
            }
        }
        else
        {
            TimeSpentCharging += Time.deltaTime;
            if (TimeSpentCharging > ChargeDuration)
            {
                Charging = false;
                TimeInCooldown = 0;
            }

        }


        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        float Acceleration = (Charging ? ChargeAcceleration : NormalAcceleration);
        rigidbody.AddForce(transform.up * Acceleration * Time.deltaTime);
        
        // limit speed
        float maxSpeed = (Charging ? ChargeSpeed : NormalSpeed);
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        
    }
}
