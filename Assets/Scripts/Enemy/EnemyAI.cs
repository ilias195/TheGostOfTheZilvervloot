using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform Player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    public int health = 100;
    public float moveSpeed = 3f;

    //patrolling

    public Vector2 Walkpoint;
    bool walkPointSet;
    bool Walkset; //Controleren wanneer de Enemy wel of niet moet lopen//
    public float WalkpointRange; //Om de aftsand te controleren wanneer de Enemy moet lopen//

    //Attack
    public float TimeBewtweenAttacks;
    bool AlreadyAttacked; //controleren of de Enemey heeft geattacked//
    public GameObject projectile;//maakt een verwijzing naar een prefab van een projectiel (zoals een kogel, vuurbal(kan je zien in Unity inspector)

    //States 

    public float sightRange, attackingRange; //sightRange: is voor de maximale afstand wat de speler kan zien)AttackingRange:De afstand wanneer de Enemy begint aan te vallen)

    public bool PlayerInSightRange, PlayerInAttackRange; // om te kijken dat de speler binnen de SightRange & PlayerInAttackRange zit.

    void Start()
    {
        Physics2D.gravity = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {

        Player = GameObject.FindWithTag("Player").transform;

    }


    private void Update()
    {
        //Check for sight and attack range
        PlayerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, WhatIsPlayer); //controleert of de speler binnen het zichtbereik van de vijand is door een onzichtbare bol te tekenen rond de vijand.

        PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, attackingRange, WhatIsPlayer);//controleert of de speler dicht genoeg bij is om aan te vallen.

        Debug.Log("In zicht? " + PlayerInSightRange + ", In aanval bereik? " + PlayerInAttackRange);

        if (PlayerInSightRange == false)
        {
            Debug.Log("Patroling");
            Patroling();
        }
        else if (PlayerInSightRange)
        {
            if (PlayerInAttackRange)
            {
                Debug.Log("AttackPlayer");
                AttackPlayer();//De vijand valt aan als hij de speler ziet en deze dichtbij genoeg is.
            }
            else
            {
                Debug.Log("ChasePlayer");
                ChasePlayer();//Als de vijand de speler ziet maar hij is nog te ver weg, gaat hij achter de speler aan.
            }
        }


    }
    private void Patroling()
    {

        //Walkpoint reached



        if (!walkPointSet) SearchWalkPoint(); //Als er nog geen patrouillepunt is, zoek er een nieuwe punt//

        if (walkPointSet)
        {
            Vector2 direction = ((Vector2)Walkpoint - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed; // de vijand beweeegt met de aanbelovende snelheid//

            float distance = Vector2.Distance(transform.position, Walkpoint); // Bereken de afstand tussen de vijand en het patrouillepunt.//
            if (distance < 0.5f)
            {
                walkPointSet = false;
                rb.linearVelocity = Vector2.zero; // stop beweging als doel bereikt is
                Debug.Log("Doelpunt bereikt. Stoppen met bewegen.");
            }
        }
    }
    void SearchWalkPoint()
    {


        //caculate random point in range

        float randomY = Random.Range(-WalkpointRange, WalkpointRange);
        float randomX = Random.Range(-WalkpointRange, WalkpointRange);
        Vector2 potentialPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);


        RaycastHit2D hit = Physics2D.Raycast(potentialPoint, Vector2.down, 2f, WhatIsGround);

        if (hit.collider != null)
        {
            Walkpoint = potentialPoint;
            walkPointSet = true;// Patrouillepunt bereikt, zet de status op false zodat er een nieuw punt gezocht wordt.//
        }
        else
        {
            Debug.Log("Geen geldige grond gevonden voor patrouillepunt.");
        }
    }
    private void ChasePlayer()
    {

        Vector2 delta = ((Vector2)Player.position - (Vector2)transform.position);
        Vector2 direction = delta.normalized;
        rb.linearVelocity = direction * moveSpeed;
        Debug.Log(direction + " " + Player.position + " " + transform.position + " " + delta);

    }

    private void AttackPlayer()
    {
        ///make sure enemy doesn't move


        rb.linearVelocity = Vector2.zero; // Stop de beweging van de vijand.//
        Vector2 dir = ((Vector2)Player.position - (Vector2)transform.position).normalized;
        transform.right = dir;// // Draai de vijand zodat hij naar de speler kijkt (de rechterkant van het object wijst naar de speler).


        if (!AlreadyAttacked) // Als de vijand nog niet heeft aangevallen..., dan gaat hij nu aanvallen.


        {


            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            Debug.Log(dir);
            projRb.AddForce(dir * 10f, ForceMode2D.Impulse);//// Schiet het projectiel naar de speler.


            //Cooldown
            AlreadyAttacked = true;//Zet AlreadyAttacked op true, zodat hij niet meteen nog een keer kan schieten.

            Invoke(nameof(ResetAttack), TimeBewtweenAttacks);//Wacht een tijdje (bijv. 1 seconde), en dan wordt ResetAttack() automatisch aangeroepen.


        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false; //Het zet de aanval van de vijand weer terug naar false, zodat hij opnieuw mag aanvallen.
    }

    public void TakeDamage(int damage)
    {
        health -= damage;//trekt je aantal health van de gezondheid.



        if (health <= 0) Invoke(nameof(DestroyEnemy), 5f);//Als de gezondheid op of onder nul komt, start je een timer van 5 seconden
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);//Wordt de vijand uit het spel verwijderd.

    }

}
