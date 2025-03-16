using UnityEngine;

public class make_cactus : MonoBehaviour
{
    public GameObject cactus;
    public float time_to_spawn;
    private float spawn_countdown;
    public float min_height;
    public float max_height;
    public float time_to_spawn_randomizer;

    // So that the cactus will be deleted upon win
    public GameObject everything;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_countdown = time_to_spawn;
    }

    // Update is called once per frame
    void Update()
    {
        // randomly spawn at certain height range
        float random_height = Random.Range(min_height, max_height);
        transform.position = new Vector3(transform.position.x, random_height, transform.position.z);
        // spawn countdown
        spawn_countdown -= Time.deltaTime;
        if(spawn_countdown <= 0)
        {
            spawn_countdown = Random.Range(time_to_spawn - time_to_spawn_randomizer, time_to_spawn + time_to_spawn_randomizer);
            Instantiate(cactus, transform.position, transform.rotation, everything.transform);
        }
    }
}
