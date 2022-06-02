using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsToTurnON : MonoBehaviour
{
    public GameObject[] important;

    private void Start() {
        for (int i = 0; i < important.Length; i++)
        {
            important[i].SetActive(true);
        }
    }
}
