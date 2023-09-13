using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBeats : MonoBehaviour
{

    [SerializeField]
    private bool IsBpmAction;

    [SerializeField]
    private int bpm;

    private float time = 0;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsBpmAction)
        {
            time += Time.deltaTime;
            if (time >= 60f / bpm)
            {
                time -= 60f / bpm;
                animator.CrossFade("Beat", 0f);
            }
        }
    }

    public void BeatsStart()
    {
        IsBpmAction = true;
        time = 0;
    }

    public void BeatsEnd()
    {
        IsBpmAction = false;
    }
}
