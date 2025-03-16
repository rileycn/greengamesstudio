using UnityEngine;
using TMPro;

public class rain_real : MonoBehaviour
{
    
    public GameObject gameLoc;
    public TMP_Text counterLoc;
    public int rain_needed = 5;

    public rain_spawner spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // stop spawning
        if (spawner.rain_collected >= rain_needed) {
            Object.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            spawner.rain_collected += 1;
        }
        counterLoc.text = spawner.rain_collected + "/" + rain_needed;
        if (spawner.rain_collected >= rain_needed)
        {
            GameManager.main.ExitWarning();
            Destroy(gameLoc);
        }
        Object.Destroy(this.gameObject);
    }
}
