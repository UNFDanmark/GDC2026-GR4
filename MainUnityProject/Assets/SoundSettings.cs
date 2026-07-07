using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider backgroundMusicSlider;
    public TextMeshProUGUI backgroundMusicText;
    public AudioSource backgroundMusicSource;

    void Update()
    {
        backgroundMusicSource.volume = backgroundMusicSlider.value;
        backgroundMusicText.text = backgroundMusicSlider.value*100+"%";
    }
}
