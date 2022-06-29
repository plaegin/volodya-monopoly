using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject window;
    [SerializeField] GameObject exitNotification;

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSavedGames()
    {
        buttons.SetActive(false);
        window.SetActive(true);
    }

    public void OpenSettings()
    {
        buttons.SetActive(false);
        window.SetActive(true);
    }

    public void GameExit()
    {
        buttons.SetActive(false);
        StartCoroutine(ExitGame());
    }

    public void CloseWindow()
    {
        window.SetActive(false);
        buttons.SetActive(true);
    }

    IEnumerator ExitGame()
    {
        exitNotification.SetActive(true);
        yield return new WaitForSeconds(3);
        exitNotification.SetActive(false);
        buttons.SetActive(true);
    }
}
