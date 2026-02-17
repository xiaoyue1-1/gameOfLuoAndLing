using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XveLiangControler : MonoBehaviour
{
    public TextMeshProUGUI tmpHealthText;
    public int currentHealth = HealthManager.HealthChange(0);
    public int maxHealth = HighHealthManager.HighHealthChange(0);
    public bool change = false;
    public int healthChange = 0;
    void Update()
    {
        currentHealth = HealthManager.HealthChange(0);
        maxHealth = HighHealthManager.HighHealthChange(0);
        tmpHealthText.text = $"{currentHealth}/{maxHealth}";
        if (change)
        {
            HighHealthManager.HighHealthChange(healthChange);
            HealthManager.HealthChange(healthChange);
            change = false;
        }
    }
}