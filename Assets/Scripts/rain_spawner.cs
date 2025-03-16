using UnityEngine;

public class rain_spawner : MonoBehaviour
{
    public GameObject rain;
    public float time_to_spawn;
    private float spawn_countdown;
    public float min_left;
    public float max_right;
    public float time_to_spawn_randomizer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_countdown = time_to_spawn;
    }

    // Update is called once per frame
    void Update()
    {
        // randomly spawn at certain height range
        float random_x = Random.Range(min_left, max_right);
        transform.position = new Vector3(random_x, transform.position.y, transform.position.z);
        // spawn countdown
        spawn_countdown -= Time.deltaTime;
        if(spawn_countdown <= 0)
        {
            spawn_countdown = Random.Range(time_to_spawn - time_to_spawn_randomizer, time_to_spawn + time_to_spawn_randomizer);
            Instantiate(rain, transform.position, transform.rotation);
        }
    }
}
