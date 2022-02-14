using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Slider SfxSlider;
    public Slider BgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        SfxSlider.value = GameManager.soundmanager.sfx;
        BgmSlider.value = GameManager.soundmanager.bgm;
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        GameManager.soundmanager.bgm = BgmSlider.value;
        GameManager.soundmanager.sfx = SfxSlider.value;
    }

}
