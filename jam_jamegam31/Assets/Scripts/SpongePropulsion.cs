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
    private bool jump = false;
    private Vector3 jumpVec;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrounded)
        {
            collisionTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        if(jump)
        {
            myRb.AddForce(jumpVec * jumpForce, ForceMode.Impulse);
            myRb.AddTorque(new Vector3(0f, 0f, -1f * Mathf.Clamp(jumpVec.x,-1f,1f) * jumpTorque), ForceMode.Impulse);
            jump = false;
        }
        if(collisionTimer >= collisionDuration && myRb.velocity.magnitude <= 0.05f)
        {
            isGrounded = true;
            collisionTimer = 0f;
            jump = false;
        }
    }

    public bool Jump(Vector3 vec)
    {
        if(collisionTimer >= collisionDuration)
        {
            print("failure: collisionTimer = " + collisionTimer + " > " + collisionDuration);
            return false;
        }
        else if(jump)
        {
            print("failure: jump already called");
            return false;
        }
        jumpVec = vec.normalized * Mathf.Clamp(vec.magnitude / 5f, 0.5f, 1f);
        jump = true;
        print("jump executed:" + vec.normalized);
        collisionTimer = collisionDuration;
        return true;
    }

    private void OnCollisionEnter(Collision other)
    {
        collisionTimer = 0f;
        isGrounded = true;
        print("collision stay; collisionTimer = " + collisionTimer);
        if(water > 0f)
        {
            // create splash effect
            // splash the stains in the area!
            // water -= amount
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
