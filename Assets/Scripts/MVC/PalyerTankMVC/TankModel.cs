using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel 
{
    private TankController tankController;

    private TankTypeScriptableObject tankTypeScriptableObject;

    public TankType TankType { get; }
    public float Speed { get; }
    public int Health { get; }
    public int SpeedLive { get { return (int)tankTypeScriptableObject.speed;} }
    public TankModel(TankTypeScriptableObject tankTypeScriptableObject)
    {
        this.tankTypeScriptableObject = tankTypeScriptableObject;
       TankType = tankTypeScriptableObject.tankType;
        Speed = tankTypeScriptableObject.speed;
        Health = tankTypeScriptableObject.maxhealth;
    }
 

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
