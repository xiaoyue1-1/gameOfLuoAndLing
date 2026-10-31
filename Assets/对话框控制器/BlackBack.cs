using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class BlackBack : MonoBehaviour
{
    public CanvasGroup BlankBack;
    public CanvasGroup text3;
    public float speet;
    public Image BlankBack1;
    public TextMeshProUGUI textOfBlankBack;
    public TextControl text2;
    // Start is called before the first frame update
    void Start()
    {
        GetInBlankback(new Color(0, 0, 0, 0), "",0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetInBlankback(Color a, string text,float speed)
    {
        if (text == "killtext")
        {
            text3.interactable = false;
            text3.blocksRaycasts = false;
            text3.alpha = 0f;
        }
        else
        {
            text3.interactable = true;
            text3.blocksRaycasts = true;
            text3.alpha = 1f;
        }
        StartCoroutine(TextOn(text));
        BlankBack1.DOColor(a,speed);

        if (a.a == 0)
        {
            BlankBack.interactable = false;
            BlankBack.blocksRaycasts = false;
        }
        else
        {
            BlankBack.interactable = true;
            BlankBack.blocksRaycasts = true;
        }
    }
    IEnumerator TextOn(string tt)
    {
        int longth1 = tt.Length;
        char[] a = new char[longth1];

        for (int i = 0; i < longth1;)
        {
            a[i] = tt[i];
            text2.textChangeTo(new string(a, 0, i + 1));
            yield return new WaitForSecondsRealtime(speet);
            i++;
        }


    }
}
