using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinControls : MonoBehaviour
{

    Transitions Transitions;

    private void Start()
    {
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
    }
    public void Restartlevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(DoTransit());
        
    }
    IEnumerator DoTransit()
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DoTransit2()
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(DoTransit2());
        
    }
}
