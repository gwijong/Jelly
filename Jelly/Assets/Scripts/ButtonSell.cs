using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSell : MonoBehaviour
{
    public static bool CursorOnSellButton { get; private set; }
    public void OnPointerEnter()
    {
        CursorOnSellButton = true;
        GameObject.Find("NoticeManager").GetComponent<NoticeManager>().Msg("sell");
    }

    public void OnPointerExit()
    {
        CursorOnSellButton = false;
    }





}
