using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Slider staminaSlider;
    public PlayerMovement playerMovement;

    void Update()
    {
        if (staminaSlider != null && playerMovement != null)
        {
            staminaSlider.value = playerMovement.CurrentStamina / playerMovement.maxStamina;
        }
    }
}
