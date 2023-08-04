using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasedUI : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;
    GameObject[] weapons;

    void Awake()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        FindObjectOfType<Swordman>().enabled = false;
        FindObjectOfType<ChangeWeapon>().enabled = false;
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<PlayerAttack>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        FindObjectOfType<ChangeWeapon>().enabled = true;
        FindObjectOfType<Swordman>().enabled = true;
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<PlayerAttack>().enabled = true;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void GoToMenu()
    //{
    //    SceneManager.LoadScene(0);
    //}
}
