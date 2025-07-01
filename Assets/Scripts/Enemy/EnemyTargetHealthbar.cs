using UnityEngine;
using UnityEngine.UI;

public class EnemyTargetHealthbar : MonoBehaviour
{
    /*
    public int Hitpoints;
    public int MaxHitpoints = 5;
    public HealthBarEnemy healthBarEnemy; 
    void Start()
    {
        Hitpoints = MaxHitpoints;
        healthBarEnemy.EnemyHealth(Hitpoints,MaxHitpoints);
    }

    public void Takehit(int damage)
    {
       Hitpoints -= damage;

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
            
    }*/
    public int Hitpoints;
    public int MaxHitpoints = 5;

    public EnemyHealthbar healthBarEnemy;

    void Start()
    {
        Hitpoints = MaxHitpoints; //Wordt ��n keer uitgevoerd wanneer het object start.

        if (healthBarEnemy != null)
        {
            healthBarEnemy.EnemyHealth(Hitpoints, MaxHitpoints);//Zet de levens op het maximum.
        }
    }

    public void Takehit(int damage)
    {
        Hitpoints -= damage;//Vermindert de levenspunten met de opgegeven schade 
        Hitpoints = Mathf.Clamp(Hitpoints, 0, MaxHitpoints);//Zorgt dat levens nooit onder 0 of boven het maximum gaan.

        if (healthBarEnemy != null)//zorgt dat de healthbar wordt bijgewerkt na schade.
        {
            healthBarEnemy.EnemyHealth(Hitpoints, MaxHitpoints);
        }

        if (Hitpoints <= 0) //ls de levens op zijn, wordt het vijand-object vernietigd.
        {
            Destroy(gameObject);
        }
    }
}
