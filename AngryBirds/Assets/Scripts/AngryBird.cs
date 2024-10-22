using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private bool hasBeenLaunched;
    private bool shouldFaceVelocityDir;

    private void Awake()
    {
        //get all components
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    private void Start()
    {
        rigidBody.isKinematic = true;
        circleCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (hasBeenLaunched && shouldFaceVelocityDir)
        {
            transform.right = rigidBody.velocity;
        }
    }

    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.isKinematic = false;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction *  force, ForceMode2D.Impulse);

        hasBeenLaunched = true;
        shouldFaceVelocityDir = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVelocityDir = false;
    }
}
