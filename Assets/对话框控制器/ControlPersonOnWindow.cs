using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ControlPersonOnWindow : MonoBehaviour
{
    private CanvasGroup self;
    int c;
    int d;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        self = GetComponent<CanvasGroup>();
        self = GetComponent<CanvasGroup>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("ControlPersonOnWindow: È±ÉÙ SpriteRenderer ×é¼þ");
    }
    void Start()
    {
        self.interactable = false;
        self.blocksRaycasts = false;
        spriteRenderer.color = new Color32(135, 135, 135, 255);

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("type", c);
        anim.SetInteger("player",d);
    }
    public void ChangePerson (int a,int b)
    {
        c = a; d = b;
        if(a==0 | b==0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0); 
        }
        else { spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1); }
    }
    public void Takeon(int s)
    {
        Vector2 Place =transform.position;
        if (s == 1)
        {
            Place.y = Place.y + 0.3f;
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
        if (s==2)
        {
            Place.y= Place.y - 0.3f;
            spriteRenderer.color = new Color32(135, 135, 135, 255);
        }
        transform.MoveTo(Place.x, Place.y,0.1f);
        


    }
}
