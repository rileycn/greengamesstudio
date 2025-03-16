using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    private List<Collider2D> overs = new();

    public static List<PlantManager> hovers = new();

    public int phase = 0;

    public GameObject stage1obj;
    public GameObject stage2obj;
    public GameObject stage3obj;

    public float health = 30f;
    public float maxhealth = 30f;
    private float baseopacity = 0.5f;

    public bool fertilized = false;
    public bool raining = false;

    public float continueOn = -999f;

    public GameObject fertObj;
    public WindEffectManager wem;
    public Transform healthBar;

    public Color ogcolor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ogcolor = GetComponent<SpriteRenderer>().color;
        //Plant();
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 1) {
            if (GameManager.main.season != GameManager.SeasonValue.Planting && !GameManager.main.splashUp && Time.time > continueOn) {
                health -= Time.deltaTime * (fertilized ? 0.5f : 1f);
            }
            if (health <= 0)
            {
                Die();
            }
        }
        UpdateVisualHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DraggableObject dobj;
        if (collision.TryGetComponent<DraggableObject>(out dobj)) {
            bool legal = false;
            if (dobj.objectType == DraggableObject.DraggableType.Seed && phase == 0) {
                legal = true;
            } else if (dobj.objectType == DraggableObject.DraggableType.Water && phase == 1)
            {
                legal = true;
            } else if (dobj.objectType == DraggableObject.DraggableType.Fert && phase == 1 && !fertilized && !raining)
            {
                legal = true;
            }
            else if (dobj.objectType == DraggableObject.DraggableType.Harvest && phase == 1)
            {
                legal = true;
            }
            if (legal) {
                overs.Add(collision);
                GetComponent<SpriteRenderer>().color = Color.yellow;
                if (!hovers.Contains(this))
                {
                    hovers.Add(this);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (overs.Contains(collision)) {
            overs.Remove(collision);
            if (overs.Count <= 0)
            {
                GetComponent<SpriteRenderer>().color = ogcolor;
                hovers.Remove(this);
            }
        }
    }

    public void Plant()
    {
        stage1obj.SetActive(true);
        stage2obj.SetActive(false);
        stage3obj.SetActive(false);
        health = maxhealth;
        GameManager.main.crop++;
        GameManager.main.UpdateMeterVisuals();
        phase = 1;
    }

    public void NextPhase()
    {
        stage1obj.SetActive(false);
        stage2obj.SetActive(false);
        stage3obj.SetActive(false);
        switch (GameManager.main.season)
        {
            case GameManager.SeasonValue.Planting:
                stage1obj.SetActive(true);
                break;
            case GameManager.SeasonValue.Rainy:
                stage2obj.SetActive(true);
                break;
            case GameManager.SeasonValue.Harvest:
                stage3obj.SetActive(true);
                break;
        }
    }

    public void Water()
    {
        health = maxhealth;
        UpdateVisualHealth();
    }

    public void Fertilize()
    {
        fertilized = true;
        UpdateVisualFert();
    }

    public void Harvest()
    {
        phase = 2;
        stage1obj.SetActive(false);
        stage2obj.SetActive(false);
        stage3obj.SetActive(false);
        health = maxhealth;
        fertilized = false;
        UpdateVisualFert();
    }

    public void Die()
    {
        fertilized = false;
        phase = 0;
        stage1obj.SetActive(false);
        stage2obj.SetActive(false);
        stage3obj.SetActive(false);
        GameManager.main.crop--;
        GameManager.main.UpdateMeterVisuals();
        UpdateVisualFert();
    }

    public void WindEffect()
    {
        if (phase == 1) {
            if (!(Time.time - wem.startTime <= 5f && Time.time - wem.startTime >= 0f))
            {
                wem.Activate();
                wem.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateVisualFert()
    {
        fertObj.SetActive(fertilized);
    }

    public void UpdateVisualHealth()
    {
        if (phase == 1)
        {
            if (health < 20f)
            {
                healthBar.transform.localPosition = new Vector3(-0.5f * (1f - health / maxhealth), healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);
                healthBar.transform.localScale = new Vector3(health / maxhealth, healthBar.transform.localScale.y, 1f);
            }
            if (health >= 20f)
            {
                healthBar.gameObject.SetActive(false);
            } else if (health >= 19f)
            {
                float alpha = (20f - health);
                healthBar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (alpha) * baseopacity);
                healthBar.gameObject.SetActive(true);
            } else
            {
                healthBar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, baseopacity);
                healthBar.gameObject.SetActive(true);
            }
        } else
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}
