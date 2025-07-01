using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;


    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            // Debug.Log("Trigger geraakt: " + other.name); // debug log

            TargetHealth target = other.gameObject.GetComponent<TargetHealth>();//een verwijzing naar het TargetHealth-script als dat gevonden is//

            if (target != null)//?Controleer of we een geldig doelwit hebben gevonden dat schade kan krijgen.?//
            {
                // Debug.Log("Doelwit gevonden, damage toegepast");
                target.TakeDamage(damage);
            }
            // Debug.Log("Bullet wordt vernietigd.");
            Destroy(gameObject);
        }
    }


}
