using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public bool isGamePaused = false;
    public TextMeshProUGUI pauseText;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isGamePaused") == 1)
            isGamePaused = false;
        else
        {
            isGamePaused = true;
            pauseText.enabled = isGamePaused;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Menu");

        if (Input.GetMouseButton(0))
            ManualSpawn();
    }

    private static void ManualSpawn()
    {
        Ray test = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(test.origin, test.direction);

        if (hit.collider != null)
        {
            GameObject clickedCell = hit.collider.gameObject;
            clickedCell.GetComponent<Cell>().isAlive = true;
        }
    }

    private void Pause()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Application.targetFrameRate = 8;
            PlayerPrefs.SetInt("isGamePaused", 1);
        }
        else
        {
            isGamePaused = true;
            Application.targetFrameRate = 60;
            PlayerPrefs.SetInt("isGamePaused", 0);
        }

        pauseText.enabled = isGamePaused;
    }
}
