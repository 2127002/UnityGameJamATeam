using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCore : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
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
    }

    public void Move()
    {
        mover.Move();
    }

    public void Jump()
    {
        if (jump.WasPerformedThisFrame())
        {
            mover.Jump(playerData.jumpPower, isGround);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
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