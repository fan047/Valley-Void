using UnityEngine;

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleDamageSource : MonoBehaviour
    {
        [SerializeField] private GameBalanceConfig balanceConfig;

        public int Damage =>
            balanceConfig != null && balanceConfig.weapon != null
                ? balanceConfig.weapon.dualLaserDamage
                : 0;

        private void Awake()
        {
            if (balanceConfig == null || balanceConfig.weapon == null)
                Debug.LogError("粒子激光未指定武器平衡配置", this);
        }
    }