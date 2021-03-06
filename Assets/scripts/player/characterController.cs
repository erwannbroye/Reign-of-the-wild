﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class characterController : MonoBehaviour
{
    public float Speed = 5f;
    public float runSpeed = 10f;
    public float JumpHeight = 2f;
    public float DashDistance = 5f;
    public float playerWeight;

    private float xAxis;
    private float zAxis;

    private CharacterController Controller;
    private Vector3 move;
    private Mesh objectMesh;
    public bool DashEnable;

    public bool canMove; 

    public bool Climbing;
    public bool onSlop; // is on a slope or not
    public float slideFriction = 0.3f; // ajusting the friction of the slope
    private Vector3 hitNormal; //orientation of the slope.

    public Interactable focus;
    public float desiredRotationSpeed = 0.1f;
    public Camera cam;
    Animator anim;
    public bool CameraNormalView;
    bool crouch = false;
    RaycastHit terrainHit;
    float fallTime = 0;
    float distanceCovered = 0;

    //audio
    public AudioSource audioSource;

    public AudioClip[] stoneClips;
    public AudioClip[] woodClips;
    public AudioClip[] dirtClips;
    AudioClip previousClip;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        DashEnable = true;
        Climbing = false;
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        CameraNormalView = true;
        crouch = false;
    }

    void OnControllerColliderHit (ControllerColliderHit hit)
    {
         hitNormal = hit.normal;
         move.y = 0;
    }

    void LateUpdate()
    {
        //control
        xAxis = (Input.GetAxis("Vertical"));
        zAxis = (Input.GetAxis("Horizontal"));
        if (canMove) {
            run();
            jump();
            interact();
            crouching();
            AimingMove();
        }
        playFallingSound();

        
        // if (Controller.isGrounded && Climbing == true && (xAxis < 0 || zAxis != 0))
        //     Climbing = false;
        // if (Climbing) {
        //     move.y = xAxis * Speed;
        //     xAxis = 0;
        //     zAxis = 0;
        // }
        // if (Controller.isGrounded)
        if ((Controller.velocity.x != 0 || Controller.velocity.z != 0) && onSlop == true) {
            distanceCovered += Controller.velocity.magnitude * Time.deltaTime;
            if (distanceCovered > 3 + (Speed / 10)) {
                footstepsSound();
                distanceCovered = 0;  
            }
        }
        
        
        if (!onSlop) {
            move.x += (1f - hitNormal.y) * hitNormal.x * (Speed * slideFriction);
            move.z += (1f - hitNormal.y) * hitNormal.z * (Speed * slideFriction);
        }

        // character slide down slopes
        
        //apply permanant gravity
        if (!Climbing)
            move.y = move.y + ((Physics.gravity.y - playerWeight) * Time.deltaTime);

        Controller.Move(move * Time.deltaTime);
        onSlop = (Vector3.Angle (Vector3.up, hitNormal) <= Controller.slopeLimit);
    }

    AudioClip getClip(AudioClip[] clipArray)
    {
        AudioClip selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];
        while (selectedClip == previousClip ) {
            selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];
        }
        previousClip = selectedClip;
        return selectedClip;
    }

    void footstepsSound()
    {
        Physics.Raycast(transform.position +  (Vector3.down), Vector3.down, out terrainHit, Controller.bounds.extents.y + 0.5f);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.8f, 1f);
        if (terrainHit.collider != null) {
            if (terrainHit.collider.tag == "ice")
                audioSource.PlayOneShot(getClip(stoneClips), 1f);
            else
                audioSource.PlayOneShot(getClip(dirtClips), 1f);
        }
    }

    void playFallingSound()
    {
        if (Controller.isGrounded == false)
            fallTime += Time.deltaTime;
        else {
            if (fallTime >= 0.25f && onSlop == true) {
                footstepsSound();
                fallTime = 0;
            }
        }
    }

    void crouching()
    {
        if (Input.GetButtonDown("Crouch")) {
            Controller.height = 1.0f;
            crouch = true;
            Speed = 2;
        }
        if ( Input.GetButtonUp("Crouch")) {
            crouch = false;
            Speed = 5;
        }
        if (!Physics.Raycast(transform.position + Vector3.up,  Vector3.up,2f) && crouch == false && Controller.height < 3)
            Controller.height += (10 * Time.deltaTime);
        if (Controller.height > 3.2)
            Controller.height = 3.0f;


    }

    void AimingMove()
    {
        
        float yStore = move.y;
        move = (transform.forward * xAxis) + (transform.right * zAxis);

        // slow down diagonal move
        if ((xAxis >= 0.5 || xAxis <= -0.5) && (zAxis >= 0.5 || zAxis <= -0.5))
            move = move.normalized * Speed;
        else
            move = move * Speed;
        move.y = yStore;
    }

    void jump() 
    {
        if ((Controller.isGrounded || Climbing == true) && Input.GetButtonDown("Jump")) {
            move.y = JumpHeight;
            Climbing = false;
        }
    }

    void run() 
    {
        if (Input.GetButtonDown("Run") && xAxis > 0 && crouch == false)
            Speed = runSpeed;
        if (Input.GetButtonUp("Run"))
            Speed = runSpeed / 2;
;
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact")) {
            removeFocus();
            Transform cam = Camera.main.transform;
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Vector3.Distance(cam.position, transform.position) + 1f)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                Debug.Log(hit.collider.gameObject.name);
                if (interactable != null)
                {
                    setFocus(interactable);
                }
            }
        }
    }

    void setFocus(Interactable newFocus)
    {
        if (newFocus != focus) {
            if (focus)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void removeFocus()
    {
        if (focus)
            focus.OnDefocused();
        focus = null;
    }
}


   