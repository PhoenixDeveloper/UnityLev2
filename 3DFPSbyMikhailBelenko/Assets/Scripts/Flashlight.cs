using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : BaseObject
{
    private KeyCode control = KeyCode.F;

    private float timeout = 10;
    private Light _light;
    private float currentTime;
    private float currReloadTime;
    private Material _lightMaterial;
    
    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _lightMaterial = GetMaterial;
    }

    private void ActiveFlashLight(bool val)
    {
        _light.enabled = val;
    }

    void Update()
    {
        if(Input.GetKeyDown(control))
        {
            ActiveFlashLight(_light.enabled ? false : true);
        }

        if(_light.enabled)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeout)
            {
                currentTime = 0;
                ActiveFlashLight(false);
            }
        }
    }
}
