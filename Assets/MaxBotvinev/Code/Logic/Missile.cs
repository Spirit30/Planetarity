using Game.Data;
using Game.Logic.Factory.Pooling;
using Game.View;
using UnityEngine;

namespace Game.Logic
{
    class Missile : PoolItem
    {
        #region REFERENCES

        [SerializeField] MissileView view;
        GameController game;

        #endregion

        #region DATA

        [SerializeField] float baseSpeed;
        [SerializeField] float baseDamage;
        MissileConfig config;

        #endregion

        #region VARIABLES

        float lifeTime;

        #endregion

        #region GETTERS / SETTERS

        public MissileView View => view;

        public MissileConfig Config
        {
            set
            {
                config = value;
            }
        }

        public Planet PlanetOwner { get; set; }

        public Vector3 AimPoint
        {
            set
            {
                transform.LookAt(value, Vector3.back);
            }
        }

        public bool IsJustSpawned { get; private set; }

        public float Speed
        {
            get
            {
                float accelerationCoef = 1.0f + Mathf.Lerp(config.Acceleration, 0, lifeTime);
                return baseSpeed * accelerationCoef / config.Weight;
            }
        }

        public float Damage => baseDamage * config.Weight;

        #endregion

        #region POOL EVENTS

        public override void OnGetFromPool()
        {
            IsJustSpawned = true;

            lifeTime = 0;

            View.PlaySound();

            game.AddMissile(this);
        }

        public override void OnPutToPool()
        {
            View.OnPutToPool();

            base.OnPutToPool();
        }

        #endregion

        #region UNITY EVENTS

        void Awake()
        {
            game = DependencyInjector.Get<GameController>();
        }

        void Update()
        {
            if(!Pause.IsPaused)
            {
                IsJustSpawned = false;

                Move();
            }
        }

        #endregion

        #region INTERFACE

        public void ApplyGravity(GravityBody gravityBody)
        {
            Vector3 gravityDirection = gravityBody.transform.position - transform.position;
            float greavityForce = gravityDirection.sqrMagnitude * gravityBody.Gravity;
            
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(gravityDirection, Vector3.back),
                greavityForce * Time.deltaTime);
        }

        #endregion

        #region IMPLEMENTATION

        void Move()
        {
            lifeTime += Time.deltaTime;
            
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

        #endregion
    }
}