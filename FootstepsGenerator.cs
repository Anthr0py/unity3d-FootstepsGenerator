using System.Collections;
using UnityEngine;

public class FootstepsGenerator : MonoBehaviour {

    [Tooltip("Rate at which is the audio playing")]
    public float footstepRate;
    [Tooltip("Audio Source component to use while playing sound")]
    public AudioSource audioSource;
    [Tooltip("Different footsteps sfx")]
    public Footstep[] footsteps;
    

    CharacterController movement;
    float footstepReset = 0.0f;
    string curReactTag = "Default";

    void Start()
    {
        // Get CharacterController
        movement = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        // Check the speed, if more than 0.15f && character controller is grounded, play sound
        float curPlayerSpeed = movement.velocity.magnitude;
        if(curPlayerSpeed > 0.15f && movement.isGrounded)
        {
            PlayFootsteps();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Change current collision tag
        curReactTag = hit.transform.tag;
    }

    public void PlayFootsteps()
    {
        // Prepare audio
        AudioClip audioToPlay = null;

        // Get audio from footsteps loop based on curReactTag string
        for(int i = 0; i < footsteps.Length; i++)
        {
            if(footsteps[i].tagToReact == curReactTag)
            {
                audioToPlay = footsteps[i].sfx[Random.Range(0, footsteps[i].sfx.Length - 1)];
                break;
            }
        }

        // Play the audio and reset footstepreset
        if (Time.time > footstepRate + footstepReset)
        {
            audioSource.PlayOneShot(audioToPlay);
            footstepReset = Time.time;
        }
    }
}

[System.Serializable]
public class Footstep
{
    public string tagToReact;
    public AudioClip[] sfx;
}