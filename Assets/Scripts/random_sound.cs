using UnityEngine;

public class random_sound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandom() {
        int random = Random.Range(0, 3);
        if (random == 0){
            source.PlayOneShot(clip1);
        }
        else if (random == 1) {
            source.PlayOneShot(clip2);
        }
        else {
            source.PlayOneShot(clip3);
        }
    }
}
