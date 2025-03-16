using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WindManager : MonoBehaviour
{
    private List<PlantManager> under = new();

    public SpriteRenderer sr;

    private float startTime;

    private float baseopactiy = 0.25f;

    private float checkTime = 2f;
    private float lastCheck = -999f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //transform.position = new Vector3(0f, 0f, transform.position.z);
        bool horizontal = (Random.value > 0.5);
        if (horizontal)
        {
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(-50f, Random.value * 4f - 2f));
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(10f, 0f);
        } else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(Random.value * 8f - 4f, -50f));
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, 10f);
        }
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < 1f)
        {
            float alpha = Time.time - startTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha * baseopactiy);
        } else if (Time.time - startTime < 14f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, baseopactiy);
        }
        else if (Time.time - startTime < 15f)
        {
            float alpha = 15f - (Time.time - startTime);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha * baseopactiy);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (under.Count <= 0)
        {
            return;
        }
        if (Time.time - startTime < 19f && Time.time - startTime > 1f)
        {
            if (Time.time - lastCheck > checkTime)
            {
                lastCheck = Time.time;
                if (Random.value < 0.5f)
                {
                    List<PlantManager> validPm = new();
                    foreach (PlantManager pm in under)
                    {
                        if (pm.phase == 1)
                        {
                            validPm.Add(pm);
                        }
                    }
                    if (validPm.Count >= 1) {
                        PlantManager rpm = validPm[(int)(Random.value * validPm.Count)];
                        rpm.WindEffect();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlantManager pm;
        if (collision.TryGetComponent<PlantManager>(out pm))
        {
            under.Add(pm);
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
            }
        }
    }
}
