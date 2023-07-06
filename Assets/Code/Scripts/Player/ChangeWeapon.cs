using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] GameObject axe;

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && sword.activeSelf)
        {
            sword.SetActive(false);
            axe.SetActive(true);
        }
        else if(Input.GetMouseButtonDown(1) && axe.activeSelf) 
        {
            sword.SetActive(true);
            axe.SetActive(false);
        }
    }
}
