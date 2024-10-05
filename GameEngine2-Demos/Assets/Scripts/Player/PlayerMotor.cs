using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHright = 3f;
    public bool crouching = false;
    public bool lerpCrouch = false;
    public float crouchTimer = 1;
    public bool sprinting = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch) 
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if(p > 1) 
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    public void Crouch() 
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint() 
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5; ;
    }



    public void ProcessMove(Vector2 input) 
    {
        Vector3 moveDirecation = Vector3.zero;
        moveDirecation.x = input.x;
        moveDirecation.z = input.y;
        controller.Move(transform.TransformDirection(moveDirecation) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) 
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);

    }
    public void Jump() 
    {
        playerVelocity.y = Mathf.Sqrt(jumpHright * -3.0f * gravity);
    }
}
