using UnityEngine;

public class rain_player : MonoBehaviour
{
    public float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * -speed * Time.deltaTime;
        }
    }
}
