using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
	
	// Update is called once per frame
	void Update () {

        Vector3 newPos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
        -10);

        this.transform.position = newPos;
	
	}
}
