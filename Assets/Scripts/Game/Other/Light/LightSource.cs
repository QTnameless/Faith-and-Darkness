using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public abstract  class LightSource : MonoBehaviour
{
    // Start is called before the first frame update
    public Light2D light2D; // The Light2D component
    public  bool isOn ;
    public float maxDuration;
    public float duration;// duration of the lightsource 
    public float drainRate;
    /*
    public float intensity ;
    public float range ;
    public Color lightColor;
    */
    protected virtual void Start()
    {
         light2D = GetComponent<Light2D>();
       
        
         //UpdateLightProperties();
        
    }
    /*
    public virtual void UpdateLightProperties()
    {
        if (light2D == null) return;
        light2D.intensity = intensity;
        light2D.pointLightOuterRadius = range;
        light2D.color = lightColor;
    }
    */
    protected virtual void UpdateLightIntensity(float intensity)
    {
        
            light2D.intensity = intensity;
    }
    protected void ToggleLight(bool state)
    {
        isOn = state;
        light2D.enabled = state;
        
    }
    public float GetDurationProportion()
    {
        return Mathf.Clamp01(duration / maxDuration);
    }
    
}
