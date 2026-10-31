using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainTextContral : MonoBehaviour
{
    private Sequence mySequence; // 保存引用便于管理
    public BlackBack blackBack;
    // Start is called before the first frame update
    void Start()
    {
;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDelay()
    {
        if (mySequence != null && mySequence.IsActive())
            mySequence.Kill();
        mySequence = DOTween.Sequence();
        mySequence.AppendInterval(5f);

        mySequence.AppendCallback(() =>
        {
            blackBack.GetInBlankback(Color.black, "你好啊",0.3f);
        });
        mySequence.AppendInterval(2f);
        mySequence.AppendCallback(() =>
        {
            blackBack.GetInBlankback(Color.black, "这里是依绫社", 0f);
        });
        mySequence.AppendInterval(3f);
        mySequence.AppendCallback(() =>
        {
            blackBack.GetInBlankback(Color.black, "如你所见，这是一个很简陋的南北同人向游戏", 0f);
        });
        mySequence.AppendInterval(2f);
        mySequence.AppendCallback(() =>
        {
            blackBack.GetInBlankback(Color.black, "话不多说，请你开始接下来的旅途吧", 0f);
        });
        mySequence.AppendInterval(2f);
        mySequence.AppendCallback(() =>
        {
            blackBack.GetInBlankback(new Color(0,0,0,0), "killtext", 0.2f);
        });
        mySequence.SetAutoKill(false);
        mySequence.SetUpdate(true);     
    }

    public void GameIsBecome()
    {
        StartDelay();
    }
}
