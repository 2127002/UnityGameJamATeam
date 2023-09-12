using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private Rigidbody rigidbody;
    private float moveSpeed;
    private float maxMoveSpeed;
    private Transform transform;

    public PlayerMover(Rigidbody rigidbody, float moveSpeed, float maxMoveSpeed, Transform transform)
    {
        this.rigidbody = rigidbody;
        this.moveSpeed = moveSpeed;
        this.transform = transform;
    }

    public void Move()
    {
        if (rigidbody.velocity.x >= maxMoveSpeed)
        {
            rigidbody.AddForce(transform.right * moveSpeed);
        }
    }

    public void Jump(float jumpPower, bool isGround)
    {
        if (isGround == false) return;

        rigidbody.AddForce(transform.up * jumpPower);
    }
}
