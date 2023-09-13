using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ColorSpectrum : MonoBehaviour
{

    private AudioSpectrum spectrum;

    [SerializeField]
    private float scaleLevel;

    [SerializeField]
    private GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        spectrum = this.GetComponent<AudioSpectrum>();

        if (spectrum == null) {
            Debug.LogError("AudioSpectrumをアタッチしてください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject o in objects)
        {
            var scale = o.transform.localScale;
            scale.y = spectrum.Levels[i] * scaleLevel;
            
            if(scale.y <= 0.5) scale.y = 0.5f;
            if(scale.y >= 2.5) scale.y = 2.5f;

            o.transform.localScale = scale;

            i++;
        }
    }
}
