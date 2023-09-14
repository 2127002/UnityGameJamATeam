using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class CountdownUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField]
    private Image countGage;

    [SerializeField]
    private TextMeshProUGUI text;

    public void CountDownView(string count, float fill)
    {
        text.text = count;
        countGage.fillAmount = fill;
    }
}
