using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DryManager : MonoBehaviour
{
    private List<PlantManager> under = new();

    public TMP_Text textColor;

    private float baseopactiy = 0.5f;

    private float startTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //transform.position = new Vector3(0f, 0f, transform.position.z);
        startTime = Time.time;
        Vector3 targetPos = Camera.main.transform.position + 4f * new Vector3(1.5f * (Random.value - 0.5f), Random.value - 0.5f);
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(targetPos.x, targetPos.y));
        Destroy(gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlantManager pm in under)
        {
            if (pm.phase == 1)
            {
                pm.health -= Time.deltaTime;
                //pm.UpdateVisualHealth();
            }
        }
        if (Time.time - startTime < 1f)
        {
            float alpha = Time.time - startTime;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha * baseopactiy);
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha * baseopactiy);
        }
        else if (Time.time - startTime < 14f)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, baseopactiy);
        }
        else if (Time.time - startTime < 15f)
        {
            float alpha = 15f - (Time.time - startTime);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha * baseopactiy);
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha * baseopactiy);
        }
        else
        {
            Destroy(gameObject);
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
