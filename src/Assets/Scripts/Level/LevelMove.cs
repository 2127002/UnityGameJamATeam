using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMove
{
    Transform transform;
    float moveSpeed;
    public LevelMove(Transform transform, float moveSpeed)
    {
        this.transform = transform;
        this.moveSpeed = moveSpeed;
    }

    public void Move()
    {
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }
}