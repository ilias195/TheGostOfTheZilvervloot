using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;//hiermee spreek je de UI aan//

    public void SetMaxHealth(int health)
    {
        Slider.maxValue = health;//hiermee is de maximale waarde vastgesteld 
        Slider.value = health; //hiermee begint de health//
    }
    public void SetHealth(int health) //hiermee kan je de gezondheid hoeveelheid//
    {
        Slider.value = health;
    }
}
