using Game.Data;
using Game.Logic.Factory;
using UnityEngine;

namespace Game.Logic
{
    class Turret : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Planet planet;
        [SerializeField] Transform cannon;
        MissileFactory missileFactory;

        #endregion

        #region DATA

        [SerializeField] MissileConfig[] missilesData;

        #endregion

        #region VARIABLES

        Vector3 aimPoint;
        float lastFireTime;

        #endregion

        #region GETTERS / SETTERS

        public MissileConfig CurrentMissileConfig { get; private set; }

        MissileConfig RandomMissileConfig
        {
            get
            {
                int randomIndex = Random.Range(0, missilesData.Length);
                return missilesData[randomIndex];
            }
        }

        public Vector3 AimPoint
        {
            get
            {
                return aimPoint;
            }
            set
            {
                aimPoint = value;
                transform.LookAt(aimPoint, Vector3.back);
            }
        }

        #endregion

        #region UNITY EVENTS

        void Awake()
        {
            missileFactory = DependencyInjector.Get<MissileFactory>();
        }

        #endregion

        #region INTERFACE

        public void ChooseMissiles()
        {
            CurrentMissileConfig = RandomMissileConfig;
            ResetVariables();
        }

        public void TryFire()
        {
            float lastFirePeriod = Time.time - lastFireTime;

            if (lastFirePeriod > CurrentMissileConfig.Cooldown)
            {
                lastFireTime = Time.time;

                Fire();
            }
        }

        #endregion

        #region IMPLEMENTATION

        void ResetVariables()
        {
            lastFireTime = 0;
        }

        void Fire()
        {
            missileFactory.GetMissileForPlanet(cannon.transform.position, planet).AimPoint = AimPoint;
        }

        #endregion
    }
}