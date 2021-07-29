using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool drawDebugRaycasts = true; 
    
    public CharacterController controller;
    public Transform Camera;
    GameManager gameManager;

    public float maxHealth = 100;
    public float currentHealth;

    [Header("Movement Properties")]
    public float speed = 5f;
    public float maxFallSpeed = -5f;

    [Header("Jump Properties")]
    public float jumpHoldDuration = .1f;
    public float jumpForce = 1.5f;
    float jumpTime;

    public float turnSmoothTime = 0.1f;
    float turnVelocity;

    [Header ("Status Flags")]
	public bool isOnGround;
    public bool isJumping;

    [Header("Environment Check Properties")]
	public float groundDistance = 0.6f;	
    public LayerMask groundLayer;

    Vector3 fallVelocity;
    public float gravity = -9.81f;

    public Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        PhysicsCheck();

        if (isOnGround && fallVelocity.y < 0f)
        {
            fallVelocity.y = -2f;
        }

        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(Horizontal, 0, Vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

        }
        fallVelocity.y +=gravity * Time.deltaTime;
        if (fallVelocity.y < maxFallSpeed) fallVelocity.y = maxFallSpeed;
        controller.Move(fallVelocity * Time.deltaTime);

        animator.SetBool("IsOnGround", isOnGround);
        animator.SetFloat("Speed", direction.magnitude);
        
    }

    void PhysicsCheck()
    {
        isOnGround = false;
        isOnGround = Raycast(new Vector3(0f, 0f, 0f), Vector3.down, groundDistance, groundLayer);

        if (Input.GetButtonDown("Jump") && !isJumping && isOnGround)
        {
            jumpTime = Time.time + jumpHoldDuration;
            fallVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJumping = true;
        }
        if (isJumping)
		{
			if (jumpTime <= Time.time && isOnGround) isJumping = false;
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

    public void inflictDamage(float damage)
    {
        currentHealth-=damage;
        //gameManager.updateHealthUI(currentHealth);
    }
}
