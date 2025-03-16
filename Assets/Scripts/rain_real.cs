using UnityEngine;

public class rain_real : MonoBehaviour
{
    public static int rain_collected = 0;
    public int rain_needed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // stop spawning
        if (rain_collected >= rain_needed) {
            Object.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            rain_collected += 1;
        }
        Object.Destroy(this.gameObject);
    }
}
