using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerCore : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform body;

    private Rigidbody rig;
    private PlayerMover mover;
    PlayerInput playerInput;
    private InputAction move, jump;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.currentActionMap["Move"];
        jump = playerInput.currentActionMap["Jump"];

        rig = GetComponent<Rigidbody>();

        mover = new PlayerMover(rig, playerData.moveSpeed, playerData.maxMoveSpeed, transform);

        anim.SetBool("run", true);
    }

    public void Move()
    {
        mover.Move();
    }

    float jumpPower = 0;
    public void Jump()
    {
        if (jump.WasPerformedThisFrame() && isGround == true)
        {
            jumpPower = playerData.jumpPower;
            anim.SetBool("jump", true);
            body.DOScaleY(1.5f, 0.2f);
        }

        if (jump.IsPressed())
        {
            jumpPower -= Time.deltaTime * playerData.jumpPower * 5;
            mover.Jump(jumpPower);
        }

        if (jumpPower <= 0)
        {
            jumpPower = 0;
        }

        if (isGround == false)
        {
            mover.SetGravity();
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);

            body.DOScaleY(0.8f, 0.15f).OnComplete(() =>
            body.DOScaleY(1.2f, 0.05f).OnComplete(() =>
            body.DOScaleY(1.0f, 0.05f)
            )
            );

            isGround = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}