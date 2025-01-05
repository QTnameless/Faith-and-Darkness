using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image bloodFill;
    public Health health;
    private void Update()
    {
        UpdateHealthBar();

    }
    public void UpdateHealthBar()
    {
        bloodFill.fillAmount = health.GetHealthProportion();
    }

}
