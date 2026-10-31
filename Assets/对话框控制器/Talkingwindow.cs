using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class Talkingwindow : MonoBehaviour
{
    private Coroutine typeCoroutine;
    public CanvasGroup self;
    public CanvasGroup botton;
    public TextControl text;
    public CanvasGroup Texts;
    public bool beenCleck;
    public float speet =0.05f;
    public ControlPersonOnWindow left;
    public ControlPersonOnWindow right;
    public int ttt;
    public int ttf;
    // Start is called before the first frame update
    void Start()
    {
        ShowDown();
        left.ChangePerson(0, 0);
        right.ChangePerson(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (beenCleck)
        {
            ShowDown();
            beenCleck = false;
        }
    }
void ShowUp()
    {
        self.interactable = true;
        self.blocksRaycasts = true;
        botton.blocksRaycasts = true;
        botton.interactable = true;
        botton.DOFade(1f,0.3f);
        self.DOFade(1f, 0.3f);
        Texts.DOFade(1f, 0.3f);
    }
void ShowDown()
    {
        self.interactable = false;
        self.blocksRaycasts = false;
        botton.blocksRaycasts = false;
        botton.interactable = false;
        botton.DOFade(0f, 0.3f);
        self.DOFade(0f, 0.3f);
        Texts.DOFade(0f, 0.3f);
    }
    public void TalkWindowText(string t)
    {
        ShowUp();
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }

        typeCoroutine = StartCoroutine(TextOn(t));
        left.ChangePerson(ttt,ttt);
        right.ChangePerson(ttf,ttf);
    }
    IEnumerator TextOn(string tt)
    {
        int longth1 = tt.Length;
        char[] a = new char[longth1];

        for (int i = 0; i < longth1;)
        {
            a[i] = tt[i];
            text.textChangeTo(new string(a,0,i+1));
            yield return new WaitForSecondsRealtime(speet);
            i++;
        }

    }
    public void BeenCleck()
    {
        beenCleck = true;
    }
    public void textCleck(string t)
    {

    }
    
}
