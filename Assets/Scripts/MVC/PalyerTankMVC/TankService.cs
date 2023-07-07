using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoBehaviour
{
    [SerializeField] private TankView tankView;

    [SerializeField] private TankTypeScriptableObjectList tanklist;
    private TankController tankController;
    
=======
    [SerializeField] private float tankSpeed = 15;


    private void Start()
    {
        CreateTank();
    }

    private void CreateTank()
    {

        tankController = new TankController(new TankModel(TankRandomize()), tankView);
    }

    private TankTypeScriptableObject TankRandomize()
    {
        int index = UnityEngine.Random.Range(0, tanklist.list.Count);
        TankTypeScriptableObject tankTypeScriptableObject = tanklist.list[index];
        
        return tankTypeScriptableObject;
=======
        TankModel tankModel = new TankModel(tankSpeed);
        TankController tankController = new TankController(tankModel, tankView);

    }
}