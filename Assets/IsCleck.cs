using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class IsCleck : MonoBehaviour
{
    public Image image;
    public Image image2;

    public bool gameBecomeing;
    void Start()
    {
        gameBecomeing = false;
    }
    public void OnButtonClick()
    {
        gameBecomeing = true;
        Debug.Log("¿ªÊ¼ÓÎÏ·");
        image.DOFade(0f, 0.5f);
        image2.DOFade(0f, 0.4f);

        gameObject.SetActive(false);


    }

}
