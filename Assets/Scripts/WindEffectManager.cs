using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WindEffectManager : MonoBehaviour
{
    private Color ogcolor = Color.gray;
    public float startTime;

    public TMP_Text clickText;

    public PlantManager pm;

    private float clickTime = 3f;

    public List<SpriteRenderer> usedSprites = new();

    // audio
    public random_sound audio;
    public random_sound audio2;
    //

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = Mathf.CeilToInt(clickTime - (Time.time - startTime));
        if (timeLeft <= -1) {
            // splat sound
            audio2.PlayRandom();
            //
            pm.Die();
            gameObject.SetActive(false);
        } else {
            clickText.text = "Click! (" + timeLeft + ")";
        }
    }

    public void Activate()
    {
        foreach (SpriteRenderer sr in usedSprites)
        {
            sr.color = ogcolor;
        }
        startTime = Time.time;
    }

    private void OnMouseDown()
    {
        //button sound
        audio.PlayRandom();
        //

        gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        foreach (SpriteRenderer sr in usedSprites)
        {
            sr.color = new Color(ogcolor.r * 0.5f, ogcolor.g * 0.5f, ogcolor.b * 0.5f, ogcolor.a);
        }
    }

    private void OnMouseExit()
    {
        foreach (SpriteRenderer sr in usedSprites)
        {
            sr.color = ogcolor;
        }
    }
}
