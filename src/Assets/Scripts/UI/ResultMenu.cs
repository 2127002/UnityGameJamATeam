using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup, canvasGroupmy;

    public void Show()
    {
        DOTween.To(() => canvasGroupmy.alpha, (v) => canvasGroupmy.alpha = v, 1.0f, 0.3f).OnComplete(() =>
        {
            canvasGroupmy.interactable = true;
        });
    }

    public void Retry()
    {
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 1.0f, 0.3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Main");
        });
    }

    public void ReturnStartMenu()
    {
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 1.0f, 0.3f).OnComplete(() =>
        {
            SceneManager.LoadScene("StartMenu");
        });
    }
}
