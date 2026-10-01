using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBalanceData
{
    public float controlSpeed = 50f;
    public float xClampRange = 30f;
    public float yClampRange = 20f;
    public float controlPitchFactor = 15f;
    public float controlRollFactor = 18f;
    public float rotationSpeed = 10f;
}

[Serializable]
public class EnemyBalanceData
{
    public EnemyType type;
    public int maxHitPoints = 3;
    public int scoreValue = 10;
}

[CreateAssetMenu(fileName = "GameBalanceConfig", menuName = "Valley Void/Game Balance Config")]
public class GameBalanceConfig : ScriptableObject
{
    public PlayerBalanceData player = new PlayerBalanceData();
    public List<EnemyBalanceData> enemies = new List<EnemyBalanceData>();
    public bool TryGetEnemyBalance(EnemyType type, out EnemyBalanceData balance)
{
    foreach (EnemyBalanceData entry in enemies)
    {
        if (entry != null && entry.type == type)
        {
            balance = entry;
            return true;
        }
    }

    balance = null;
    return false;
}
}