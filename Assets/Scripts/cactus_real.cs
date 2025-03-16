using UnityEngine;

public class cactus_real : MonoBehaviour
{
    // So that the cactus will be deleted upon win
    public GameObject finish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.transform.position.x >= finish.transform.position.x) {
            Object.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        Object.Destroy(this.gameObject);
    }
}
