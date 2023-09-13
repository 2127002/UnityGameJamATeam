using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using KanKikuchi.AudioManager;
using DG.Tweening;

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
    [SerializeField] private float moveSpeed;
    [SerializeField] private CanvasGroup canvasGroup, countDown;
    [SerializeField] private CountdownUI countdownUI;
    [SerializeField] private ResultMenu resultMenu;

    [SerializeField] private ComboView comboView;

    private Animator anim;

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
    private Rigidbody playerRdb;
    private LevelMove levelMove;

    private int combo;

    [SerializeField]
    private MoonBeats moonBeats;
    // Start is called before the first frame update
    private async void Start()
    {
        state = GameState.opening;
        mainCamera = Camera.main;

        player = Instantiate(playerData.player);
        Camera.main.transform.parent = player.transform;
        player.transform.position = new Vector3(-8, 2, 0);
        playerRdb = player.GetComponent<Rigidbody>();
        mover = new PlayerMover(playerRdb, playerData.moveSpeed, player.transform.Find("root/body").GetComponent<Animator>(), player.transform.Find("root"));
        levelMove = new LevelMove(levels[0], moveSpeed);
        checkIsGround = new CheckIsGround();
        playerInput = GetComponent<PlayerInput>();
        jump = playerInput.currentActionMap["Jump"];

        anim = player.transform.Find("root/body").GetComponent<Animator>();

        jumpCount = maxJumpCount;

        //オープニング
        var cts_ = new CancellationTokenSource();
        await Opening(cts_.Token);
        cts_.Cancel();
    }

    private bool isGround;
    private int jumpCount;
    float jumpPower = 0;
    float pitch = 1;
    void FixedUpdate()
    {
        anim.SetBool("run", state == GameState.game);

        if (state != GameState.game) return;
        //地形移動
        levelMove.Move();

        //ジャンプボタン長押し
        if (jump.IsPressed())
        {
            mover.Jump(jumpPower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state != GameState.game) return;

        if (player.transform.position.y < -20)
        {
            state = GameState.result;
            Debug.Log("死んだ");
            resultMenu.Show();
        }

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
            SEManager.Instance.Play(SEPath.JUMP1);

            jumpPower = playerData.jumpPower;
            mover.JumpStart();

            jumpCount--;

            if (JustMeet <= 0)
            {
                // Debug.Log("Miss");
            }

            if (JustMeet < 0.4f && JustMeet > 0)
            {
                SEManager.Instance.Play(SEPath.JAN, pitch: pitch, volumeRate: 0.5f);
                pitch += 0.05f;
                AddScore(1);
                // Debug.Log("Good!");
            }
        }
    }

    private void PlayerJump()
    {
        //着地時
        if (isGround == false && checkIsGround.GetIsGround(-player.transform.up, player.transform.position) && playerRdb.velocity.y <= 0)
        {
            jumpCount = maxJumpCount;
            mover.Landing();
        }

        //着地判定
        isGround = checkIsGround.GetIsGround(-player.transform.up, player.transform.position) && playerRdb.velocity.y <= 0;

        //ジャンプボタン長押し
        if (jump.IsPressed())
        {
            jumpPower -= Time.deltaTime * playerData.jumpPower * 10;
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

    async UniTask Opening(CancellationToken token)
    {
        await Task.Delay(500, token);
        await DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 0, 0.3f).AsyncWaitForCompletion();
        await Task.Delay(500, token);
        await DOTween.To(() => countDown.alpha, (v) => countDown.alpha = v, 1.0f, 0.3f).AsyncWaitForCompletion();

        for (int i = 3; i > 0; i--)
        {
            float fill = 1.0f;
            await DOTween.To(() => fill, (v) => fill = v, 0.0f, 1.0f).OnUpdate(() => countdownUI.CountDownView(i.ToString(), fill)).AsyncWaitForCompletion();
        }

        countdownUI.CountDownView("GO", 0.0f);

        await Task.Delay(1000, token);

        DOTween.To(() => countDown.alpha, (v) => countDown.alpha = v, 0, 0.5f);

        state = GameState.game;

        BGMManager.Instance.Play(BGMPath.MOONBEET);

        moonBeats.BeatsStart();

        //判定
        var cts = new CancellationTokenSource();
        await Beat(cts.Token);
        cts.Cancel();
    }

    async UniTask Beat(CancellationToken token)
    {
        await Task.Delay(372, token);
        JustMeet = 0.3f;
        while (state == GameState.game)
        {
            await Task.Delay(522, token);
            JustMeet = 0.3f;
        }
    }

    public void Clear()
    {
        state = GameState.result;
        resultMenu.Show();
    }

    public void AddScore(int add)
    {
        combo += add;
        comboView.ComboViewer(combo.ToString());
    }
}