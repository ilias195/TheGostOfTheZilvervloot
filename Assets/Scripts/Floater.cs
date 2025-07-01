using UnityEngine;

public class Floater : MonoBehaviour
{
   public  Rigidbody2D rb;
    public float deptjBeforeSub = 1f; //Dit is hoe diep iets mag gaan voordat het echt begint te "drijven"
    public float displacementAmount = 3f; //Dit zegt hoeveel kracht naar boven het krijgt als het onder water zit


    private void FixedUpdate()
    {
         if (transform.position.y  < 0f)
        {
            // We rekenen uit hoeveel het moet drijven:
            // Als het een beetje onder water is, krijgt het een beetje duwkracht
            // Als het diep onder water is, krijgt het veel duwkracht
            // Clamp01 zorgt dat de uitkomst altijd tussen 0 en 1 blijft, zodat het niet te gek wordt

            float displacementMultiplier = Mathf.Clamp01(transform.position.y / deptjBeforeSub) * displacementAmount;
            rb.AddForce(new Vector2(0f, Mathf.Abs(Physics2D.gravity.y) * displacementMultiplier), ForceMode2D.Force);


        }
    }
}
