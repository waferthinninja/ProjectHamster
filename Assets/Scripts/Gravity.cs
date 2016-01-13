using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Gravity : MonoBehaviour
{
    Rigidbody2D ownRb;

    void Start()
    {
        ownRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        List<Rigidbody2D> rbs = FindObjectsOfType<Rigidbody2D>().ToList();

        foreach (Rigidbody2D rb in rbs)
        {
            if (rb != null && rb != ownRb)
            {
                Vector2 offset = transform.position - rb.transform.position;
                rb.AddForce(offset / offset.sqrMagnitude * ownRb.mass * Time.deltaTime);
            }
        }
    }

}