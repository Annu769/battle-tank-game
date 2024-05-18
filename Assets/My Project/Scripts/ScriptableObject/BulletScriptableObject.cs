using System.Collections;
using System.Collections.Generic;
using BattleTank;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = " ScriptableObject/NewBulletSo",order = 4)]
public class BulletScriptableObject : ScriptableObject
{

    public int damage;
    public int range;

    public BulletView bulletView;
}


