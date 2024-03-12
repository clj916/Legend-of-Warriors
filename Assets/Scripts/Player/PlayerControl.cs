using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputdirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float jumpForce;
    //移动速度
    public float speed;

    [Header("受伤")]
    public bool isHurt;
    public int hurtForce;
    public bool isDead;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        //Application.targetFrameRate = 120;
        inputControl.GamePlay.Jump.started += Jump;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        inputdirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!isHurt)
            Move();

    }

    public void Move()
    {
        rb.velocity = new Vector2(inputdirection.x * speed * Time.deltaTime, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;

        if (inputdirection.x > 0)
            faceDir = 1;
        else if (inputdirection.x < 0)
            faceDir = -1;

        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGroud)
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void GetHurt(Transform attackTran)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attackTran.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }
}
