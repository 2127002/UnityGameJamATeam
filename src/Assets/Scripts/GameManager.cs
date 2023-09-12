using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCore playerCore;
    private bool isGame = false;
    // Start is called before the first frame update
    void Start()
    {
        isGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame == true)
        {
            playerCore.Move();
            playerCore.Jump();
        }
    }
}