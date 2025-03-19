using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIScreen : MonoBehaviour
{

    [SerializeField] string MenuName; //Name of the Menu Scene
    [SerializeField] string LevelScene;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(MenuName);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(LevelScene);
    }
}
