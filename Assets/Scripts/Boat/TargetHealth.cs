using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool Isdead = false;

    public HealthBar healthBar; //deze gaat naar mijn Healthbar script//
    void Start()
    {
        currentHealth = maxHealth; // Zet het leven van het object helemaal vol

        if (healthBar != null) // Als er een levensbalk bestaat (bestaat die wel? even checken)
        {
            healthBar.SetMaxHealth(maxHealth); // ...dan zorgen we dat de levensbalk ook laat zien dat het leven vol is.
        }




    }
    public void TakeDamage(int damage) // Dit gebeurt als het object geraakt wordt en schade krijgt.
    {
        currentHealth -= damage; // Haal het aantal schadepunten van het leven af.

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Zorg dat het leven niet onder 0 of boven het maximum kan gaan.
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) // Als het leven op is (0 of minder)
        {
            Die(); // ...dan roep je de Die() functie aan (het object gaat dood).
        }

    }
    void Die() // Dit is wat er gebeurt als het object doodgaat.
    {
        Isdead = true;

        Debug.Log(gameObject.name + " is dood"); // Laat in de console zien dat dit object dood is gegaan.
        Destroy(gameObject); // Verwijder het object uit het spel (het is nu weg).

        if (Isdead)
        {
            SceneManager.LoadScene(0);  //Laad de "GameScene" in met de LoadScene methode
            Debug.Log("speler begint opnieuw");
        }
    }
}
