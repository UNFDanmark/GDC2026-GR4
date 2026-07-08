using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider backgroundMusicSlider;
    public TextMeshProUGUI backgroundMusicText;
    public AudioMixer backgroundMusicSource;

    void Update()
    {
        backgroundMusicSource.SetFloat("Master", backgroundMusicSlider.value*100 - 80);
        backgroundMusicText.text = Mathf.Round(backgroundMusicSlider.value*100)+"%";
    }
}
