using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    private int experience;
    public int neededExp;
    public int playerLevel;
    public int expPoints;
    private Swordman player;
    private PlayerAttack playerAttack;

    [Header("Attributes")]
    [SerializeField] private int healthUpgrade;
    [SerializeField] private int damageUpgrade;
    [SerializeField] private float moveSpeedUpgrade;
    [SerializeField] private Button[] upgradeButtons;

    private void Awake() 
    {
        player = GetComponent<Swordman>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start() 
    {
        expPoints = 0;
        playerLevel = 1;
        neededExp = GetNeededExp(playerLevel);
    }

    private void Update()
    {
        if (expPoints >= 1)
        {
            foreach (Button button in upgradeButtons)
            {
                button.interactable = true;
            }
        }
        else
        {
            foreach (Button button in upgradeButtons)
            {
                button.interactable = false;
            }
        }
    }

    public void AddExp(int exp)
    {
        experience += exp;
        if(experience >= neededExp)
        {
            playerLevel++;
            experience = 0;
            neededExp = GetNeededExp(playerLevel);
            expPoints++;
        }
    }

    int GetNeededExp(int currLevel)
    {
        int neededExpForLvlUp = playerLevel * 100;
        return neededExpForLvlUp;
    }

    public void IncreaseHealth()
    {
        if(expPoints > 0)
        {
            player.AddHealth(healthUpgrade);
            expPoints--;
        }
    }

    public void IncreaseMoveSpeed()
    {
        if(expPoints > 0)
        {
            player.AddSpeed(moveSpeedUpgrade);
            expPoints--;
        }
    }

    public void IncreaseDamage()
    {
        if(expPoints > 0)
        {
            playerAttack.AddDamage(damageUpgrade);
            expPoints--;
        }
    }
}
 