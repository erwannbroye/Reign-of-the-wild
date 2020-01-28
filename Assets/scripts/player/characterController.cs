using System.Collections;
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

    public const float maxDashTime = 15f;

    private CharacterController Controller;
    private Vector3 move;
    float currentDashTime = maxDashTime;
    private Mesh objectMesh;
    public bool DashEnable;

    public bool Climbing;
    public bool onSlop; // is on a slope or not
    public float slideFriction = 0.3f; // ajusting the friction of the slope
    private Vector3 hitNormal; //orientation of the slope.

    public Interactable focus;
    public float desiredRotationSpeed = 0.1f;
    public Camera cam;
    Animator anim;
    public bool CameraNormalView;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        DashEnable = true;
        Climbing = false;
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        CameraNormalView = true;
    }

    void OnControllerColliderHit (ControllerColliderHit hit)
    {
         hitNormal = hit.normal;
         move.y = 0;
    }

    public float getXAxis()
    {
        return(xAxis);
    }
    void LateUpdate()
    {
        //control
        xAxis = (Input.GetAxis("Vertical"));
        zAxis = (Input.GetAxis("Horizontal"));

        run();
        jump();
        interact();

        if (Controller.isGrounded && Climbing == true && (xAxis < 0 || zAxis != 0))
            Climbing = false;

        //end control

        // apply move

        if (Climbing) {
            move.y = xAxis * Speed;
            xAxis = 0;
            zAxis = 0;
        }
        float yStore = move.y;

        AimingMove();
        
        move.y = yStore;

        // character slide down slopes 
        // if (!onSlop) {
        //     move.x += (1f - hitNormal.y) * hitNormal.x * (Speed * slideFriction);
        //     move.z += (1f - hitNormal.y) * hitNormal.z * (Speed * slideFriction);
        // }
        //apply permanant gravity
        if (!Climbing)
            move.y = move.y + ((Physics.gravity.y - playerWeight) * Time.deltaTime);

        Controller.Move(move * Time.deltaTime);

        onSlop = (Vector3.Angle (Vector3.up, hitNormal) <= Controller.slopeLimit);
    }

    void BasicMove()
    {
        var camera = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
 
        forward.y = 0f;
        right.y = 0f;
 
        forward.Normalize ();
        right.Normalize ();
 
        move = forward * xAxis + right * zAxis;

        if (xAxis >= 0.5 && zAxis >= 0.5)
            move = move.normalized * Speed;
        else
            move = move * Speed;

        if ((xAxis != 0 || zAxis != 0))
                transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (move), desiredRotationSpeed);
    }

    void AimingMove()
    {
        
        move = (transform.forward * xAxis) + (transform.right * zAxis);

        // slow down diagonal move
        if ((xAxis >= 0.5 || xAxis <= -0.5) && (zAxis >= 0.5 || zAxis <= -0.5))
            move = move.normalized * Speed;
        else
            move = move * Speed;
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
        if (Input.GetButtonDown("Fire3") && xAxis > 0)
            Speed = runSpeed
 * 1.5f;
        if (Input.GetButtonUp("Fire3"))
            Speed = runSpeed
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


   