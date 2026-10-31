using UnityEngine;
using DG.Tweening;

public class CgContral : MonoBehaviour
{
    public ImageControl Up;
    public ImageControl Down;
    public LanControl Up_lan;
    public LanControl Down_lan;

    private Sequence cgSequence;

    public Vector2 UpFirst = new Vector2(-1574.5f, 200f);
    public Vector2 DownFirst = new Vector2(1574.5f, -200f);
    public Vector2 UpLanFirst = new Vector2(-1574.5f, 100f);
    public Vector2 DownLanFirst = new Vector2(1574.5f, -100f);
    public Vector2 Uplast = new Vector2(0f, 200f);
    public Vector2 Downlast = new Vector2(0f, -200f);
    public Vector2 UpLanlast = new Vector2(0f, 100f);
    public Vector2 DownLanlast = new Vector2(0f, -100f);
    public Vector2 UpLasted = new Vector2(1574.5f, 200f);
    public Vector2 DownLasted = new Vector2(-1574.5f, -200f);
    public Vector2 UpLanLasted = new Vector2(1574.5f, 100f);
    public Vector2 DownLanLasted = new Vector2(-1574.5f, -100f);

    void Start()
    {
        Up.Move(UpFirst, 0f, 0f);
        Down.Move(DownFirst, 0f, 0f);
        Up_lan.Move(UpLanFirst, 0f, 0f);
        Down_lan.Move(DownLanFirst, 0f, 0f);
    }

    public void enterCg(float speed, float speed2)
    {

        Up.Move(UpFirst, 0f, 0f);
        Down.Move(DownFirst, 0f, 0f);
        Up_lan.Move(UpLanFirst, 0f, 0f);
        Down_lan.Move(DownLanFirst, 0f, 0f);
        if (cgSequence != null && cgSequence.IsActive())
            cgSequence.Kill();
        cgSequence = DOTween.Sequence();

        cgSequence.AppendCallback(() =>
        {
            Up.Move(UpLasted, speed, speed2);
            Down.Move(DownLasted, speed, speed2);
        });

        cgSequence.AppendInterval(0.3f);

        cgSequence.AppendCallback(() =>
        {
            Up_lan.Move(UpLanLasted, speed, speed2);
            Down_lan.Move(DownLanLasted, speed, speed2);
        });



        cgSequence.SetAutoKill(false);
    }
}