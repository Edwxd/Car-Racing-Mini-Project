using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject _pauseButton;
    [SerializeField]
    private GameObject _resumeButton;
    [SerializeField]
    private GameObject _resumeMenu;
    [SerializeField]
    private GameObject _pauseMenu;


    // Start is called before the first frame update
    void Start()
    {

        //_pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);

        //_resumeMenu.SetActive(false);
        _resumeButton.SetActive(false);

    }


    public void OnPause()
    {
        Time.timeScale = 0f; //We want to stop the game immediatly, do the time scale for the pause menu is 0

        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);

        // _resumeMenu.SetActive(false);
        _resumeButton.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1.0f;

        _resumeButton.SetActive(false);
        _resumeMenu.SetActive(true);

        //_pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);

    }

    public void Reload()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
