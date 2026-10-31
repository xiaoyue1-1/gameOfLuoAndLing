using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpControler : MonoBehaviour
{
    public Image spBar;
    public TextMeshProUGUI SpOfGUI;
    public int SpOfHigh = SpManager.SpChange(true,0);
    public int SpOfNow = SpManager.SpChange(false,0);
    public bool BecomeChange = false;
    public int ChangenumoberOfSp;
    public int ChangenumoberOfHighSp;
    public bool BecomeSet = false;
    public int SetSp;
    public int SetHighSp;

    void Update()
    {
        SpOfHigh = SpManager.SpChange(true, 0);
        SpOfNow = SpManager.SpChange(false, 0);
        SpOfGUI.text = $"{SpOfNow}/{SpOfHigh}";
        if (spBar != null) spBar.fillAmount = SpOfHigh > 0 ? Mathf.Clamp01((float)SpOfNow / SpOfHigh) : 0f;
        if (BecomeChange)
        {
            SpManager.SpChange(true, ChangenumoberOfHighSp);
            SpManager.SpChange(false, ChangenumoberOfSp);
            BecomeChange = false;
        }
        if (BecomeSet) { SpManager.SetSp(true, SetHighSp);SpManager.SetSp(false, SetSp); BecomeSet = false; }
    }
}
