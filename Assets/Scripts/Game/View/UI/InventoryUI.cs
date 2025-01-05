using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Image durationFill;
    public LightSource lightsource;

    private void Start()
    {
        durationFill= GetComponent<Image>();
    }
    private void Update()
    {
        UpdateDurationFill();

    }
    public void UpdateDurationFill()
    {
        durationFill.fillAmount =1-lightsource.GetDurationProportion();
    }
}
