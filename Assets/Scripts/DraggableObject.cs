using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public static DraggableObject select;

    public enum DraggableType
    {
        Seed,
        Water,
        Fert,
        Harvest
    };

    public DraggableType objectType;

    private Vector3 ogPos;
    public GameObject harvestRing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (select == this)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        if (select == null && !GameManager.main.inWarning && GameManager.main.isStarted)
        {
            select = this;
            ogPos = transform.position;
            if (objectType == DraggableType.Harvest)
            {
                GetComponent<CircleCollider2D>().radius = 1f;
                harvestRing.SetActive(true);
            } else
            {
                GetComponent<CircleCollider2D>().radius = 0.01f;
            }
        }
    }

    private void OnMouseUp()
    {
        if (select == this)
        {
            OnDrop();
            select = null;
            transform.position = ogPos;
            if (objectType == DraggableType.Harvest)
            {
                harvestRing.SetActive(false);
            }
        }
    }

    public void OnDrop()
    {
        if (PlantManager.hovers.Count <= 0)
        {
            GetComponent<CircleCollider2D>().radius = 0.5f;
            return;
        }
        PlantManager plantCrop = PlantManager.hovers[0];
        float oldDis = (transform.position - plantCrop.transform.position).magnitude;
        for (int i = 1; i < PlantManager.hovers.Count; i++)
        {
            float newDis = (transform.position - PlantManager.hovers[i].transform.position).magnitude;
            if (newDis < oldDis)
            {
                oldDis = newDis;
                plantCrop = PlantManager.hovers[i];
            }
        }
        if (objectType == DraggableType.Seed)
        {
            if (GameManager.main.seed >= 1f) {
                plantCrop.Plant();
                GameManager.main.seed--;
                GameManager.main.UpdateMeterVisuals();
            }
        } else if (objectType == DraggableType.Water)
        {
            if (GameManager.main.water >= 1f)
            {
                plantCrop.Water();
                GameManager.main.water--;
                GameManager.main.UpdateMeterVisuals();
            }
        } else if (objectType == DraggableType.Fert)
        {
            if (GameManager.main.fert >= 1f)
            {
                plantCrop.Fertilize();
                GameManager.main.fert--;
                GameManager.main.UpdateMeterVisuals();
            }
        }
        else if (objectType == DraggableType.Harvest)
        {
            for (int i = 0; i < PlantManager.hovers.Count; i++)
            {
                PlantManager.hovers[i].Harvest();
            }
            harvestRing.SetActive(false);
        }
        GetComponent<CircleCollider2D>().radius = 0.5f;
    }
}
