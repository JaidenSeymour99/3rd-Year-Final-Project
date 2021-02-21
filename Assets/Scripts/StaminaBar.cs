using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //setting the max stamina so the slider knows the max stamina is the full bar
    public void SetMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;

        fill.color = gradient.Evaluate(1f);
    }

    //setting the current stamina so the slider knows how much of the bar to fill up
    public void SetStamina(float stamina)
    {
        slider.value = stamina;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
