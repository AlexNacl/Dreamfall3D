using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool drawDebugRaycasts = true; //if inviroment check is visualized
    public Rigidbody rigidBody;

    [Header("Movement Properties")]
    public float speed = 0f;
    public float maxFallSpeed = -5f;
    
    [Header("Jump Properties")]
    public float jumpHoldDuration = .1f;
    public float jumpForce;
    float jumpTime;


    [Header ("Status Flags")]
	public bool isOnGround;
    public bool isJumping = true;
    

    [Header("Environment Check Properties")]
	public float groundDistance = .2f;	
    public LayerMask groundLayer;


    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsCheck();
    }


    void PhysicsCheck()
    {
        isOnGround = false;
        
        isOnGround = Raycast(new Vector3(0f, 0f, 0f), Vector3.down, groundDistance, groundLayer);
		//if (GroundCheck) isOnGround = true;
        
        rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal")*speed, rigidBody.velocity.y, Input.GetAxis("Vertical")*speed);
        if (Input.GetButtonDown("Jump") && !isJumping && isOnGround)
        {
            jumpTime = Time.time + jumpHoldDuration;
            rigidBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = true;
        }
        if (isJumping)
		{
			if (jumpTime <= Time.time) isJumping = false;
            if (rigidBody.velocity.y < maxFallSpeed) rigidBody.velocity = new Vector3(rigidBody.velocity.x, maxFallSpeed, rigidBody.velocity.z);
		}
    }


	bool Raycast(Vector3 offset, Vector3 rayDirection, float length, LayerMask mask)
	{
		Vector3 pos = transform.position;
		RaycastHit hit;
        Physics.Raycast(pos, rayDirection, out hit, length, mask);
		if (drawDebugRaycasts)
		{
			Color color = Physics.Raycast(pos, rayDirection, out hit, length, mask) ? Color.red : Color.green;
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}
		return Physics.Raycast(pos, rayDirection, out hit, length, mask);
	}
}
