using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCore playerCore;
    private bool isGame = false;
    private Camera mainCamera;
    private CameraScript cameraScript;
    private LevelMove levelMove;
    [SerializeField] private List<Transform> levels = new List<Transform>();

    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        isGame = true;
        mainCamera = Camera.main;

        cameraScript = new CameraScript(mainCamera.transform, playerCore.transform);
        levelMove = new LevelMove(levels[index], 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame == true)
        {
            playerCore.Jump();
            levelMove.Move();
        }
    }
}