using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        //get all components
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();


        rigidBody.isKinematic = true;
        circleCollider.enabled = false;
    }

    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.isKinematic = false;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction *  force, ForceMode2D.Impulse);
    }
}
