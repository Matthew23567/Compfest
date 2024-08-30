using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LigtManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Update()
    {
        if(Preset == null)
        {
            return;
        }
        if(Application.isPlaying )
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24;
            LightChange(TimeOfDay);
        }
    }

    private void LightChange(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent*360f)-90f,170f,0));
        }
    }


    private void OnValidate()
    {
        if (DirectionalLight != null) return;

        else
        {
            Light[] lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}


