using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : BaseObject
{
    private KeyCode control = KeyCode.F;

    private float timeout = 15;
    private Light _light;
    private float currentTime;
    private Image sliderColor;
    private float currReloadTime;
    private Material _lightMaterial;

    // минимальная и максимальная интенсивность источника света
    public float min = 1;
    public float max = 8;

    public Slider slider; // элемент UI

    // цвета индикатора
    public Color maxColor = Color.white;
    public Color halfColor = Color.yellow;
    public Color minColor = Color.red;

    protected override void Awake()
    {
        base.Awake();
        slider = _canvasMainCamera.GetComponentInChildren<Slider>();
        sliderColor = slider.fillRect.GetComponentInChildren<Image>();
    }

    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _light.enabled = false;
        _light.intensity = min;
        _lightMaterial = GetMaterial;
        sliderColor.color = maxColor;
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = 100;
    }

    private void ActiveFlashLight(bool val)
    {
        _light.enabled = val;
        if (val == false)
        {
            _light.intensity = min;
        }
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
                slider.value = 0;
                ActiveFlashLight(false);
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
            }
        }

        // переводим время в диапазон значений от 0 до 100% и вычетам из 100
        slider.value = 100 - (currentTime / timeout) * 100;

        float intensity = max;
        Color curColor = maxColor;

        if (slider.value < 50) curColor = halfColor; // меняем цвет на промежуточный, если меньше 50% заряда

        if (slider.value < 20)
        {
            curColor = minColor;
            intensity = max / 2; // снижаем яркость фонарика

            if (Random.Range(0, 0.9f) > 0.5f) intensity = intensity / Random.Range(1, 6); // рандомное мерцание, перед отключением
        }

        sliderColor.color = Color.Lerp(sliderColor.color, curColor, 1.5f * Time.deltaTime);
        _light.intensity = Mathf.Lerp(_light.intensity, intensity, 3f * Time.deltaTime);
    }
}
