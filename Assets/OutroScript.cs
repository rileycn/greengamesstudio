using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroScript : MonoBehaviour
{
    void Start()
    {
        //Restart();
        
    }

    public void Restart()
    {

        GameInfo.year = 1;
        GameInfo.fert = 0;
        GameInfo.water = 0;
        GameInfo.seed = 0;
        GameInfo.cash = 0;

        SceneManager.LoadScene("MainMenu");
    }
}
