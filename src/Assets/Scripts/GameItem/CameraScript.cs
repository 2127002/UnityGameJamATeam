using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript
{
    // playerとカメラの位置関係を保持
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時でのplayerとカメラの位置関係を記憶
        offset = transform.position - playerTransform.position;
    }

    private Transform transform, playerTransform;

    public CameraScript(Transform transform, Transform playerTranceform)
    {
        this.transform = transform;
        this.playerTransform = playerTranceform;
        offset = transform.position - playerTranceform.position;
    }

    public void CameraMove(Vector3 playerPos)
    {
        // 現在位置からplayerと新しいカメラの位置を作成
        Vector3 vector = playerPos + offset;
        // 縦方向は固定
        vector.y = transform.position.y;
        // カメラの位置を移動
        transform.position = vector;
    }
}
