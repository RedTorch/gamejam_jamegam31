using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpongePropulsion : MonoBehaviour
{
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpTorque = 20f;
    [SerializeField] private Rigidbody myRb;
    [SerializeField] private ParticleSystem myPs;

    public AudioClip sound_jump;
    public AudioClip sound_dash;
    public AudioClip sound_impact;
    public AudioClip sound_splash;
    AudioSource audioSource;

    private float water = 1f;
    private bool jump = false;
    private Vector3 jumpVec;
    private bool isGrounded = true;
    private bool dashStored = true;
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
        if(jump)
        {
            // myRb.AddForce(jumpVec * jumpForce, ForceMode.Impulse);
            myRb.velocity = jumpVec * jumpForce / myRb.mass;
            myRb.AddTorque(new Vector3(0f, 0f, -1f * jumpVec.x * jumpTorque), ForceMode.Impulse);
            jump = false;
            audioSource.PlayOneShot(sound_jump);
        }
    }

    public bool Jump(Vector3 vec, bool forceJump = false)
    {
        if((!isGrounded && !forceJump) || jump)
        {
            return false;
        }
        if(forceJump)
        {
            if(!dashStored)
            {
                return false;
            }
            else
            {
                dashStored = false;
            }
        }
        jumpVec = vec.normalized; // Mathf.Clamp(vec.magnitude / 5f, 0.5f, 1f);
        jump = true;
        print("jump executed:" + vec.normalized);
        return true;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        dashStored = true;
        print("collision entered");
        if(water > 0f)
        // if other is an enemy!
        {
            // attack!
        }
    }

    private void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        dashStored = true;
    }

    private void OnCollisionExit(Collision other)
    {
        print("collision left");
        isGrounded = false;
    }

    public void OnPaintCollision(int paintType)
    {
        audioSource.PlayOneShot(sound_splash);
        var em = myPs.emission;
        em.SetBursts(new ParticleSystem.Burst[]{new ParticleSystem.Burst(0.0f, 10)});
        myPs.Play();
    }
}
