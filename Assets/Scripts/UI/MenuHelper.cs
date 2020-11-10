using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuHelper : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
