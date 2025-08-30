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

        audioMixer.SetFloat(AudioMixerParams.Bgm, Mathf.Log10(bgmVolume) * 20);
        // 데시벨로 변경하기위함

        if (bgmVolume <= 0.001f)
            audioMixer.SetFloat(AudioMixerParams.Bgm, -80);
    }

    public void SfxAudioControl()
    {
        sfxVolume = sfxSlider.value;

        if (muteToggle.isOn)
            return;

        audioMixer.SetFloat(AudioMixerParams.Sfx, Mathf.Log10(sfxVolume) * 20);

        if (sfxVolume <= 0.001f)
            audioMixer.SetFloat(AudioMixerParams.Sfx, -80);
    }

    public void ToggleSound()
    {
        if (muteToggle.isOn)
        {
            audioMixer.SetFloat(AudioMixerParams.Bgm, -80);
            audioMixer.SetFloat(AudioMixerParams.Sfx, -80);
        }
        else
        {
            audioMixer.SetFloat(AudioMixerParams.Bgm, Mathf.Log10(bgmVolume) * 20);
            audioMixer.SetFloat(AudioMixerParams.Sfx, Mathf.Log10(sfxVolume) * 20);
        }
    }

    public void SetSliderValue()
    {
        sfxSlider.value = sfxVolume;
        bgmSlider.value = bgmVolume;
    }
}