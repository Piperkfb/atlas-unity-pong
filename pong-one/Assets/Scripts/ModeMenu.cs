using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour
{
    public Button VSCom;
    public Button VSP2;
    public Button Back;
    static public bool ComOn;
    private AudioSource OddEO;
    public AudioClip SFClick;
    // Start is called before the first frame update
    void Start()
    {
        VSCom.onClick.AddListener(ComGame);
        VSP2.onClick.AddListener(P2Game);
        Back.onClick.AddListener(GoBack);
        OddEO = this.GetComponent<AudioSource>();
    }

    protected void ComGame()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        ComOn = true;
        SceneManager.LoadScene("Game");
    }
    protected void P2Game()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        ComOn = false;
        SceneManager.LoadScene("Game");
    }
    protected void GoBack()
    {
        OddEO.clip = SFClick;
        OddEO.Play();
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
