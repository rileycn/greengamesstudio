using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    private List<Collider2D> overs = new();

    public static List<PlantManager> hovers = new();

    public GameObject stage1obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        overs.Add(collision);
        GetComponent<SpriteRenderer>().color = Color.yellow;
        if (!hovers.Contains(this))
        {
            hovers.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overs.Remove(collision);
        if (overs.Count <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            hovers.Remove(this);
        }
    }

    public void Plant()
    {
        stage1obj.SetActive(true);
    }
}
