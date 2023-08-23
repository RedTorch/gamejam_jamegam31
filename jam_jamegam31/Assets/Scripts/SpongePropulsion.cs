using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpongePropulsion : MonoBehaviour
{
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpTorque = 20f;
    [SerializeField] private Rigidbody myRb;

    public AudioClip sound_jump;
    AudioSource audioSource;

    private float water = 1f;
    private bool jump = false;
    private Vector3 jumpVec;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    private void FixedUpdate() 
    {
        if(jump && isGrounded)
        {
            // myRb.AddForce(jumpVec * jumpForce, ForceMode.Impulse);
            myRb.velocity = jumpVec * jumpForce / myRb.mass;
            myRb.AddTorque(new Vector3(0f, 0f, -1f * jumpVec.x * jumpTorque), ForceMode.Impulse);
            jump = false;
            audioSource.PlayOneShot(sound_jump);
        }
    }

    public bool Jump(Vector3 vec)
    {
        if(!isGrounded || jump)
        {
            return false;
        }
        jumpVec = vec.normalized; // Mathf.Clamp(vec.magnitude / 5f, 0.5f, 1f);
        jump = true;
        print("jump executed:" + vec.normalized);
        return true;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        print("collision entered");
        if(water > 0f)
        {
            // create splash effect
            // splash the stains in the area!
            // water -= amount
        }
    }

    private void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        if(water > 0f)
        {
            // create impact sound
        }
    }

    private void OnCollisionExit(Collision other)
    {
        print("collision left");
        isGrounded = false;
    }
}
