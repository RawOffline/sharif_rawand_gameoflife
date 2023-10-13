using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void SimulateGame()
    {
        PlayerPrefs.SetInt("isGamePaused", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void EmptyGame()
    {
        PlayerPrefs.SetInt("isGamePaused", 0);
        SceneManager.LoadScene("SampleScene");
    }
}
