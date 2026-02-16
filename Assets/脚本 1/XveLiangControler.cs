using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XveLiangControler : MonoBehaviour
{
    public TextMeshProUGUI tmpHealthText;
    public int currentHealth = HealthManager.HealthChange(0);
    public int maxHealth = 100;

    void Update()
    {
        currentHealth = HealthManager.HealthChange(0);
        tmpHealthText.text = $"{currentHealth}/{maxHealth}";
    }
}
