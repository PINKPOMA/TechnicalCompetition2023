using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject RankUI;
    public void StartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("InGame");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void RnjkUIButton()
    {
        if (RankUI.active)
        {
            RankUI.SetActive(false);
        }
        else
        {
            RankUI.SetActive(true);
        }
    }
}
