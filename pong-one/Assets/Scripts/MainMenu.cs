using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button tutButton;
    private AudioSource OddEO;
    public AudioClip SFClick;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        OddEO = this.GetComponent<AudioSource>();
        tutButton.onClick.AddListener(Tutorial);
    }

    protected void PlayGame()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        SceneManager.LoadScene("PlayMenu");
    }
    protected void QuitGame()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        Application.Quit();
    }
    protected void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    void Update()
    {
        
    }
}
