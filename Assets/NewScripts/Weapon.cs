using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    [SerializeField] protected JoystickSecond joystickSecond;
    protected LayerMask enemiesLayer;
    protected float attackRange;
    public int number = 1;
    public bool isEquiped; 
    
    public virtual void Strike()
    {

    }
   
}
