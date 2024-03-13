using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    protected void PlayGame()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    protected void QuitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        
    }
}
