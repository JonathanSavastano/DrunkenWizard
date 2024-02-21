using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public float destroyDelay = 3.0f;

    private void Start()
    {
        // Destroy the projectile after a certain delay
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        // Move the projectile in its forward direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
