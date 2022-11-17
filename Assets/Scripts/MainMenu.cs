using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit() 
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void NotImplemented() 
    {
        Debug.Log("Not Implemented :c");
    }
}
