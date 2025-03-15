using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public static DraggableObject select;

    private Vector3 ogPos;
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
        if (select == null)
        {
            select = this;
            ogPos = transform.position;
        }
    }

    private void OnMouseUp()
    {
        if (select == this)
        {
            OnDrop();
            select = null;
            transform.position = ogPos;
        }
    }

    public void OnDrop()
    {
        if (PlantManager.hovers.Count >= 1)
        {
            PlantManager plantCrop = PlantManager.hovers[0];
            plantCrop.Plant();
        }
    }
}
