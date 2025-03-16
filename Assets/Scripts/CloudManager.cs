using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private List<PlantManager> under = new();

    public Sprite sprite1;
    public Sprite sprite2;

    private float speed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //transform.position = new Vector3(0f, 0f, transform.position.z);
        float randomDir = Mathf.PI * 2f * Random.value;
        Vector2 newPos = new Vector2(10f * Mathf.Sin(randomDir), 6f * Mathf.Cos(randomDir));
        GetComponent<Rigidbody2D>().MovePosition(newPos);
        Vector3 targetPos = Camera.main.transform.position + 1f * new Vector3(1.5f * (Random.value - 0.5f), Random.value - 0.5f);
        Vector3 look = (new Vector2(targetPos.x, targetPos.y) - newPos).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = speed * new Vector2(look.x, look.y);
        Destroy(gameObject, 120f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlantManager pm in under)
        {
            if (pm.phase == 1)
            {
                pm.health += 4f * Time.deltaTime;
                if (pm.health > pm.maxhealth)
                {
                    pm.health = pm.maxhealth;
                }
                //pm.UpdateVisualHealth();
                
            }
        }
        GetComponent<SpriteRenderer>().sprite = (Time.time % 1f > 0.5f) ? sprite1 : sprite2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlantManager pm;
        if (collision.TryGetComponent<PlantManager>(out pm))
        {
            under.Add(pm);
            pm.raining = true;
            if (pm.fertilized)
            {
                if (Random.value < 0.2f) {
                    pm.fertilized = false;
                    pm.UpdateVisualFert();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlantManager pm;
        if (collision.TryGetComponent<PlantManager>(out pm))
        {
            if (under.Contains(pm))
            {
                under.Remove(pm);
                pm.raining = false;
            }
        }
    }

}
