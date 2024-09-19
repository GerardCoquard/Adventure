using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Weapon _weapon;
    private Armor _armor;

    public Weapon GetWeapon()
    {
        return _weapon;
    }
    
    public Armor GetArmor()
    {
        return _armor;
    }
    
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
    
    public void SetArmor(Armor armor)
    {
        _armor = armor;
    }
}
