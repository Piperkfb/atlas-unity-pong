using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainMenu : MonoBehaviour
{
    public Button PlayAgain;
    public Button Back;
    // Start is called before the first frame update
    void Start()
    {
        PlayAgain.onClick.AddListener(Restart);
        Back.onClick.AddListener(GoBack);
    }

    // Update is called once per frame
    protected void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    protected void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
