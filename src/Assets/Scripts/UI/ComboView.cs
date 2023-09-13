using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboView : MonoBehaviour
{
    private TextMeshProUGUI comboText;

    void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
    }

    public void ComboViewer(string combo)
    {
        comboText.text = combo;
    }
}