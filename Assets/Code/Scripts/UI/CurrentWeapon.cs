using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    Image image;
    Sprite sword;
    Sprite axe;

    private void Start()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        sword = weapons[0].GetComponent<SpriteRenderer>().sprite;
        axe = weapons[1].GetComponent<SpriteRenderer>().sprite;
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (weapons[0].activeSelf)
            image.sprite = sword;
        else if (weapons[1].activeSelf)
            image.sprite = axe;
    }
}
