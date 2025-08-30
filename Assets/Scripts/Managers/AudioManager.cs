using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [Space]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle muteToggle;

    private float bgmVolume;
    private float sfxVolume;

    string bgmName = "musicVol";
    string sfxName = "sfxVol";


    public void Start()
    {
        bgmVolume = 1f;
        sfxVolume = 1f;
        muteToggle.isOn = false;
        SetSliderValue();
    }

    public void BgmAudiocontrol()
    {
        bgmVolume = bgmSlider.value;

        if (muteToggle.isOn)
            return;

        audioMixer.SetFloat(bgmName, Mathf.Log10(bgmVolume) * 20);

        if (bgmVolume <= 0.001f)
            audioMixer.SetFloat(bgmName, -80);
    }

    public void SfxAudioControl()
    {
        sfxVolume = sfxSlider.value;

        if (muteToggle.isOn)
            return;

        audioMixer.SetFloat(sfxName, Mathf.Log10(sfxVolume) * 20);

        if (sfxVolume <= 0.001f)
            audioMixer.SetFloat(sfxName, -80);
    }

    public void ToggleSound()
    {
        if (muteToggle.isOn)
        {
            audioMixer.SetFloat(bgmName, -80);
            audioMixer.SetFloat(sfxName, -80);
        }
        else
        {
            audioMixer.SetFloat(bgmName, Mathf.Log10(bgmVolume) * 20);
            audioMixer.SetFloat(sfxName, Mathf.Log10(sfxVolume) * 20);
        }
    }

    public void SetSliderValue()
    {
        sfxSlider.value = sfxVolume;
        bgmSlider.value = bgmVolume;
    }
}