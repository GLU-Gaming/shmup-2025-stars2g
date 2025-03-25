using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public GameObject winUI;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWin();
        }
    }

    public void PlayerWin()
    {
        winUI.SetActive(true);
        Time.timeScale = 0f;
        if (player.TryGetComponent<PlayerMovement>(out var controller))
        {
            controller.enabled = false;
        }
    }
}

