using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongePropulsion : MonoBehaviour
{
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpTorque = 20f;
    [SerializeField] private float collisionDuration = 0.3f;
    [SerializeField] private Rigidbody myRb;

    private float collisionTimer = 0f;
    private float water = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    public void Jump(Vector3 vec)
    {
        if(collisionTimer >= collisionDuration)
        {
            print("failure: collisionTimer = " + collisionTimer + " > " + collisionDuration);
            return;
        }
        Vector3 moveVec = vec.normalized * Mathf.Clamp(vec.magnitude / 5f, 0.5f, 1f);
        myRb.AddForce(moveVec * jumpForce, ForceMode.Impulse);
        myRb.AddTorque(new Vector3(0f, 0f, -1f * moveVec.x * jumpTorque), ForceMode.Impulse);
        print("jump executed:" + vec.normalized);
        collisionTimer = collisionDuration;
    }

    private void OnCollisionStay(Collision other)
    {
        collideHandler(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        collideHandler(other);
    }

    void collideHandler(Collision other)
    {
        collisionTimer = 0f;
        print("collision stay; collisionTimer = " + collisionTimer);
        if(water > 0f)
        {
            // create splash effect
            // splash the stains in the area!
            // water -= amount
        }
    }
}
