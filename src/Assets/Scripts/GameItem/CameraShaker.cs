using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShaker : MonoBehaviour
{
    public void Shake()
    {
        transform.DOShakePosition(0.2f);
    }
}
