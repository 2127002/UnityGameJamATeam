using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGround
{
    public bool GetIsGround(Vector3 direction, Vector3 origin)
    {
        Ray ray = new Ray(origin, direction); // Rayを生成;

        RaycastHit hit;
        bool isGround = false;
        if (Physics.Raycast(ray, out hit, 2.0f)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            isGround = hit.collider.gameObject.CompareTag("Ground");
        }

        return isGround;
    }
}