using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_switch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void exit_game()
    {
        Application.Quit();
    }
}
