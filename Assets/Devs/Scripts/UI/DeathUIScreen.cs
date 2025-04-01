using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathUIScreen : MonoBehaviour
{
    Transitions Transitions;

    private void Start()
    {
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
    }

    string MenuName = "Menu"; //Name of the Menu Scene
    [SerializeField] string LevelScene;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DoTransit()
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(LevelScene);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(MenuName);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Menu");
    }

}
