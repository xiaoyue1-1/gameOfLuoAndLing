using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class IsCleck : MonoBehaviour
{
    public CanvasGroup image;
    public CanvasGroup image2;
    public CanvasGroup image3;
    public CanvasGroup image4;

    public bool gameBecomeing;
    void Start()
    {
        gameBecomeing = false;
    }
    public void OnButtonClick()
    {
        gameBecomeing = true;
        image.interactable = false;
        image2.interactable = false;
        image.blocksRaycasts = false;
        image2.blocksRaycasts = false;
        image4.blocksRaycasts = false;
        image4.interactable = false;
        Debug.Log("¿ªÊ¼ÓÎÏ·");
        image.DOFade(0f, 0.5f);
        image2.DOFade(0f, 0.4f);
        image3.DOFade(0f, 0.4f);
        image4.DOFade(0f, 0.4f);
        image3.blocksRaycasts = false;
        image3.interactable = false;


    }

}
