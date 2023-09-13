using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangoScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Clear!!");
            GameObject.Find("GameManager").GetComponent<GameManager>().Clear();
        }
    }
}
