using UnityEngine;
using System.Collections;
using EZ_Pooling;

public class BulletController : MonoBehaviour {

    public float TimeToLive;

    private float age;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;

        if (age > TimeToLive)
            EZ_PoolManager.Despawn(this.transform);
	}
}
