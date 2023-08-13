using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasedUI : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject upgradeMenu;
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

    public void UpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        PlayerManipulation(false);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        PlayerManipulation(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        PlayerManipulation(true);
    }

    void PlayerManipulation(bool playerState)
    {
        FindObjectOfType<ChangeWeapon>().enabled = playerState;
        FindObjectOfType<Swordman>().enabled = playerState;
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<PlayerAttack>().enabled = playerState;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
