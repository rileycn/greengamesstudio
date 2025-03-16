using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject plantObj;

    public List<PlantManager> plants = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = -5; x <= 5; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                GameObject newPlant = Instantiate(plantObj);
                newPlant.transform.parent = transform;
                newPlant.transform.localPosition = new Vector3(x * 1.25f, y * 1.25f);
                plants.Add(newPlant.GetComponent<PlantManager>());
            }
        }
    }

    public void ResetHealth()
    {
        foreach (PlantManager pm in plants)
        {
            if (pm.phase >= 1) {
                pm.health = pm.maxhealth;
                pm.continueOn = Time.time + 5f + Random.value * 5f;
                pm.NextPhase();
            }
        }
    }

    
}
