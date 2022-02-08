using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public AudioSource BgmPlayer;
    public AudioSource SfxPlayer;

    public Slider SfxSlider;
    public Slider BgmSlider;

    public float bgm;
    public float sfx;

    [SerializeField]
    AudioClip Button;
    [SerializeField]
    AudioClip Buy;
    [SerializeField]
    AudioClip Clear;
    [SerializeField]
    AudioClip Fail;
    [SerializeField]
    AudioClip Grow;
    [SerializeField]
    AudioClip PauseIn;
    [SerializeField]
    AudioClip PauseOut;
    [SerializeField]
    AudioClip Sell;
    [SerializeField]
    AudioClip Touch;
    [SerializeField]
    AudioClip Unlock;
    void Start()
    {
        SfxSlider.value = sfx;
        BgmSlider.value = bgm;
    }

    // Update is called once per frame
    void Update()
    {
        bgm = BgmSlider.value;
        sfx = SfxSlider.value;
        BgmPlayer.volume = bgm;
        SfxPlayer.volume = sfx;
    }

    public void PlaySfxPlayer(string audioClipName)
    {
        switch (audioClipName)
        {
            case "Button":
                SfxPlayer.clip = Button;
                break;
            case "Buy":
                SfxPlayer.clip = Buy;             
                break;
            case "Clear":
                SfxPlayer.clip = Clear;
                break;
            case "Fail":
                SfxPlayer.clip = Fail;
                break;
            case "Grow":
                SfxPlayer.clip = Grow;
                break;
            case "PauseIn":
                SfxPlayer.clip = PauseIn;
                break;
            case "PauseOut":
                SfxPlayer.clip = PauseOut;
                break;
            case "Sell":
                SfxPlayer.clip = Sell;
                break;
            case "Touch":
                SfxPlayer.clip = Touch;             
                break;
            case "Unlock":
                SfxPlayer.clip = Unlock;
                break;
        }
        SfxPlayer.Play();
    }
}
