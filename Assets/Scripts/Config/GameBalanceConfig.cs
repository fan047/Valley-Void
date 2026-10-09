using System;
using System.Collections.Generic;
using UnityEngine;

    [Serializable]
    public class PlayerBalanceData
    {
        [Min(0f)] public float controlSpeed = 50f;
        [Min(0f)] public float xClampRange = 30f;
        [Min(0f)] public float yClampRange = 20f;
        [Min(0f)] public float lowerYClampFactor = 0.5f;
        public float controlPitchFactor = 15f;
        public float controlRollFactor = 18f;
        [Min(0f)] public float rotationSpeed = 10f;
    }

    [Serializable]
    public class EnemyBalanceData
    {
        public EnemyType type;
        [Min(1f)] public int maxHitPoints = 3;
        [Min(0f)] public int scoreValue = 10;
    }

    [Serializable]
    public class WeaponBalanceData
    {
        [Min(1)] public int dualLaserDamage = 1;
        [Min(1)] public int beamDamage = 1;
        [Min(1f)] public float targetDistance = 250f;
        [Min(1f)] public float beamRange = 1000f;
        [Min(0.05f)] public float beamHitInterval = 0.25f;
    }

    [CreateAssetMenu(fileName = "GameBalanceConfig", menuName = "Valley Void/Game Balance Config")]
    public class GameBalanceConfig : ScriptableObject
    {
        public PlayerBalanceData player = new PlayerBalanceData();
        public List<EnemyBalanceData> enemies = new List<EnemyBalanceData>();
        public WeaponBalanceData weapon = new WeaponBalanceData();

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

        public bool TryValidateEnemies(out string error)
        {
            if (enemies == null)
            {
                error = "敌人配置列表为空";
                return false;
            }

            var types = new HashSet<EnemyType>();

            foreach (EnemyBalanceData entry in enemies)
            {
                if (entry == null)
                {
                    error = "敌人配置中有空条目";
                    return false;
                }

                if (!types.Add(entry.type))
                {
                    error = $"敌人类型 {entry.type} 配置重复";
                    return false;
                }

                if (entry.maxHitPoints < 1 || entry.scoreValue < 0)
                {
                    error = $"敌人 {entry.type} 的血量或分数无效";
                    return false;
                }
            }

            error = null;
            return true;
         }
    }