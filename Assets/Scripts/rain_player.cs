using UnityEngine;

public class rain_player : MonoBehaviour
{
    public float speed = 5f;

    private float xinput;
    private float yinput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = xinput * speed;
    }

    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
    }
}
