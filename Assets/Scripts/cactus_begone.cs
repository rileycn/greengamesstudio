using UnityEngine;

public class cactus_begone : MonoBehaviour
{
    public static bool destroy = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy) {
            Object.Destroy(this.gameObject);
        }
    }
}
