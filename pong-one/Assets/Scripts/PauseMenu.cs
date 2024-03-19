using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;
    public Button resumeButton;
    public GameObject PMenu;
    private AudioSource OddEO;
    public AudioClip SFClick;
    // Start is called before the first frame update
    void Start()
    {

        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(MainMenu);
        resumeButton.onClick.AddListener(Resume);
        OddEO = this.GetComponent<AudioSource>();
    }
  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log ("Es ca pe");
            if (!PMenu.activeInHierarchy)
            {
                Pause();
            }
            else
            {
                
                Resume();
            }
        }
    }
        public void Pause()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        Time.timeScale = 0;
        PMenu.SetActive(true);
    }
    public void Resume()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        Time.timeScale = 1;
        PMenu.SetActive(false);
    }
        public void MainMenu()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
        public void Restart()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }
}
