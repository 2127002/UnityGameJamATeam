using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private Rigidbody rigidbody;
    private float moveSpeed;
    private float maxMoveSpeed;
    private Transform bodyTransform;
    private Animator anim;
    private Tweener tweener;
    public PlayerMover(Rigidbody rigidbody, float moveSpeed, Animator anim, Transform bodyTransform)
    {
        this.rigidbody = rigidbody;
        this.moveSpeed = moveSpeed;
        this.bodyTransform = bodyTransform;
        this.anim = anim;
    }

    public void Move()
    {
        if (rigidbody.velocity.x >= maxMoveSpeed)
        {
            rigidbody.velocity = new Vector3(moveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
        }
    }

    public void JumpStart()
    {
        anim.SetBool("jump", true);
        bodyTransform.DOScaleY(1.5f, 0.2f);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
    }

    public void Jump(float jumpPower)
    {
        rigidbody.AddForce(bodyTransform.parent.up * jumpPower);
    }

    public void Landing()
    {
        anim.SetBool("jump", false);

        if (tweener != null)
        {
            if (tweener.IsActive()) return;
        }

        tweener = bodyTransform.DOScaleY(0.8f, 0.15f).OnComplete(() =>
        bodyTransform.DOScaleY(1.0f, 0.05f)
        );
    }

    public void SetGravity()
    {
        rigidbody.AddForce(new Vector2(0, -5));
    }
}
