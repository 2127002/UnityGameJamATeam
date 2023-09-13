using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using KanKikuchi.AudioManager;

public enum GameState
{
    opening,
    game,
    result,
    pause,
}

public enum Review
{
    tooFast,
    just,
    tooLate,
    miss,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int maxJumpCount;
    private GameState state;
    private Camera mainCamera;
    [SerializeField] private List<Transform> levels = new List<Transform>();

    private int index = 0;

    private float JustMeet = 0;

    private PlayerMover mover;
    private CheckIsGround checkIsGround;
    PlayerInput playerInput;
    private InputAction move, jump;

    private Transform player;
    // Start is called before the first frame update
    private async void Start()
    {
        state = GameState.game;
        mainCamera = Camera.main;

        player = Instantiate(playerData.player);
        player.transform.position = new Vector3(-8, 2, 0);
        mover = new PlayerMover(player.GetComponent<Rigidbody>(), playerData.moveSpeed, player.transform.Find("root/body").GetComponent<Animator>(), player.transform.Find("root"));

        checkIsGround = new CheckIsGround();
        playerInput = GetComponent<PlayerInput>();
        jump = playerInput.currentActionMap["Jump"];

        player.transform.Find("root/body").GetComponent<Animator>().SetBool("run", true);

        jumpCount = maxJumpCount;

        //オープニング演出を開始
        var cts = new CancellationTokenSource();
        await Beat(cts.Token);
        cts.Cancel();
    }

    private bool isGround;
    private int jumpCount;
    float jumpPower = 0;
    void FixedUpdate()
    {
        //地形移動
        // levelMove.Move();

        //ジャンプボタン長押し
        if (jump.IsPressed())
        {
            mover.Jump(jumpPower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (JustMeet > 0)
        {
            JustMeet -= Time.deltaTime;
        }
        else
        {
            JustMeet = 0;
        }

        PlayerJump();

        //ジャンプボタンを押した時
        if (jump.WasPressedThisFrame() && jumpCount > 0)
        {
            jumpPower = playerData.jumpPower;
            mover.JumpStart();

            jumpCount--;

            if (JustMeet <= 0)
            {
                Debug.Log("Miss");
            }

            if (JustMeet < 0.4f && JustMeet > 0)
            {
                Debug.Log("Good!");
            }
        }
    }

    private void PlayerJump()
    {
        //着地時
        if (isGround == false && checkIsGround.GetIsGround(-player.transform.up, player.transform.position) && player.GetComponent<Rigidbody>().velocity.y <= 0)
        {
            jumpCount = maxJumpCount;
            mover.Landing();
        }

        //着地判定
        isGround = checkIsGround.GetIsGround(-player.transform.up, player.transform.position) && player.GetComponent<Rigidbody>().velocity.y <= 0;

        //ジャンプボタン長押し
        if (jump.IsPressed())
        {
            jumpPower -= Time.deltaTime * playerData.jumpPower * 5;
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

    async UniTask Beat(CancellationToken token)
    {
        while (state == GameState.game)
        {
            await Task.Delay(329, token);
            JustMeet = 0.2f;
            await Task.Delay(100, token);
            SEManager.Instance.Play(SEPath.SYSTEM20);
        }
    }
}