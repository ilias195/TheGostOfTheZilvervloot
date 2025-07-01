using UnityEngine;

public class BoatMove : MonoBehaviour
{
    private float movementspeed = 0;
    [SerializeField] private float rotspeed;
    [SerializeField] private float versnel;
    [SerializeField] private float afremmen;
    [SerializeField] private float maxspeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)) //als ik W indruk moet ik naar voor
        {
            movementspeed += Mathf.Abs(versnel);
            if (movementspeed >= maxspeed)
            {
                movementspeed = maxspeed;
            }
        }
        else if (movementspeed > 0f)
        {
            movementspeed -= afremmen;

        }
        if (movementspeed > 0.01f)
        {
            transform.Rotate(transform.forward * rotspeed * Input.GetAxis("Horizontal"));

        }


        transform.Translate(transform.up * movementspeed * Time.deltaTime);
        //  Debug.Log(movementspeed);
    }
}
