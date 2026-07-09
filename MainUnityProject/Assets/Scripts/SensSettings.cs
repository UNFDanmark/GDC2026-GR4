using System;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SensSettings : MonoBehaviour
{
    public Slider sensSlider;
    public TextMeshProUGUI sensText;
    CameraController cam;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        cam.sensitivityX = sensSlider.value/2f;
        cam.sensitivityY = sensSlider.value/2f;
        sensText.text = Mathf.Round(sensSlider.value/2f*100)+"";
    }
}
