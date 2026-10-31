using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Windows;

public class WindowsAndTextControler : MonoBehaviour
{
    public int charactersPerPage = 150; 
    private int totalPages = 1;
    public TextMeshProUGUI textGUI;
    public CanvasGroup exit1;
    public CanvasGroup next2;
    public CanvasGroup mainBackGround4;
    public int pageNumber = 0;
    public string windowText;
    public string windowWholeText;
    // Start is called before the first frame update
    void Start()
    {
        mainBackGround4.DOFade(0f,0f);
        exit1.DOFade(0f,0f);
        next2.DOFade(0f,0f);
        exit1.interactable = false;
        exit1.blocksRaycasts = false;
        next2.interactable = false;
        next2.blocksRaycasts = false;
    }

    // Update is called once per frame

    private void SetButtonState(CanvasGroup cg, bool active, float alpha)
    {
        cg.interactable = active;
        cg.blocksRaycasts = active;
        cg.DOFade(alpha, 0.3f);
    }
    private void SetUIPage(int page)
    {
        if (page <= 0)
        {
            SetButtonState(exit1, false, 0f);
            SetButtonState(next2, false, 0f);
            SetBackgroundState(false);
            textGUI.text = "";
        }
        else
        {
            SetBackgroundState(true);
            bool isLastPage = (page == totalPages);
            SetButtonState(exit1, isLastPage, isLastPage ? 1f : 0f);
            SetButtonState(next2, !isLastPage, !isLastPage ? 1f : 0f);
        }
    }
    private void SetBackgroundState(bool show)
    {
        mainBackGround4.DOFade(show ? 1f : 0f, 0.3f);
    }
    public void GetWindow(string input)
    {
        if (input == "Button")
        {
            if (string.IsNullOrEmpty(windowWholeText)) return;
            totalPages = PageManager(windowWholeText); 
            if (pageNumber < totalPages)
                pageNumber++;
        }
        else
        {
            windowWholeText = input;
            totalPages = PageManager(input);
            pageNumber = 1;
        }

        windowText = ChangeText(windowWholeText, pageNumber);
        textGUI.text = windowText;
        SetBackgroundState(true);
        SetUIPage(pageNumber);
    }


    private int PageManager(string text)
    {
        int len = text.Length;
        int pageCount = len / charactersPerPage;
        if (len % charactersPerPage != 0) pageCount++;
        return pageCount;
    }

    public string ChangeText(string fullText, int page)
    {
        int startIndex = charactersPerPage * (page - 1);
        if (startIndex >= fullText.Length) return "";
        int length = Mathf.Min(charactersPerPage, fullText.Length - startIndex);
        return fullText.Substring(startIndex, length);
    }
    public void exitNowWindow()
    {
        pageNumber = 0;
        windowWholeText = "";
        SetBackgroundState(false);
        SetUIPage(0);
    }
}

