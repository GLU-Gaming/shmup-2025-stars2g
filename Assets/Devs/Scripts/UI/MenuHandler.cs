using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    Transitions Transitions;

    private void Start()
    {
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
        Transitions.SetTransition(false);
    }
    public void LoadLevel()
    {
        StartCoroutine(DoTransit());
    }

    IEnumerator DoTransit()
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("RegularLevel");
    }
}
