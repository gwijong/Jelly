using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSell : MonoBehaviour
{
    public static bool CursorOnSellButton { get; private set; }
    bool currentOnSellButton;
    public void LateUpdate()
    {
        CursorOnSellButton = currentOnSellButton;
    }
    public void OnPointerEnter()
    {
        currentOnSellButton = true;
        NoticeManager.Msg("sell");
    }

    public void OnPointerExit()
    {
        currentOnSellButton = false;
    }





}
