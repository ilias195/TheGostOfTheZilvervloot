using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector2 Offset;

    public void EnemyHealth(int health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);//Toont de healthbar alleen als de vijand niet op volle gezondheid is

        // Stelt de maximumwaarde en huidige waarde van de healthbar in.
        slider.maxValue = maxHealth;
        slider.value = health;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);//Verandert de kleur van de healthbar afhankelijk van hoeveel levenspunten de vijand nog heeft.
    }

    void Update()
    {
        Debug.Log("hoi");
        if (Camera.main != null)//Zorgt ervoor dat de healthbar altijd boven het vijand-object blijft staan op het scherm, ongeacht de camerabeweging.
        {
            Vector3 worldPosition = transform.parent.position + (Vector3)Offset;
            slider.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        }
    }
}
