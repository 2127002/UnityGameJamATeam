using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 0.0f, 0.3f);
    }

    public void StartButton()
    {
        DOTween.To(() => canvasGroup.alpha, (v) => canvasGroup.alpha = v, 1.0f, 0.3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Main");
        });
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
