using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/CreatePlayerParam")]
public class PlayerData : ScriptableObject
{
    public Transform player;
    public float moveSpeed;
    public float maxMoveSpeed;
    public float jumpPower;
}
