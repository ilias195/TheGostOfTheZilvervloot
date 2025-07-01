using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private float movementspeed = 0;
    public float rotspeed;
    public float versnel;
    public float afremmen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) //als ik W indruk moet ik naar voor    
        {
            movementspeed += versnel;
        }
        else if (movementspeed > 0f)
        {
            movementspeed -= afremmen;

        }
        if (movementspeed > 0.001f)
        {
            transform.Rotate(transform.forward * rotspeed * Input.GetAxis("Horizontal"));

        }


        transform.Translate(transform.up * movementspeed);

    }
}
