using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public Character unit;
    public Transform hitPoint;
    public Transform carryPoint;
    public void Update()
    {
        if (unit.hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
