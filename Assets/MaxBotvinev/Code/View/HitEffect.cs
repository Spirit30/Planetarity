using Game.Logic;
using Game.Logic.Factory.Pooling;
using UnityEngine;

namespace Game.View
{
    class HitEffect : PoolItem
    {
        #region REFERENCES

        [SerializeField] ParticleSystem particles;

        #endregion

        #region DATA

        [SerializeField] float lifeTime;

        #endregion

        #region VARIABLES

        float lifeStartTime;

        #endregion

        #region SETTERS

        public Color Color
        {
            set
            {
                var particlesMain = particles.main;
                particlesMain.startColor = value;
            }
        }

        #endregion

        #region POOL EVENTS

        public override void OnGetFromPool()
        {
            lifeStartTime = Time.time;
        }

        #endregion

        #region UNITY EVENTS

        void OnEnable()
        {
            particles.Play();
        }

        void Update()
        {
            if(!Pause.IsPaused)
            {
                float lifePeriod = Time.time - lifeStartTime;

                if(lifePeriod > lifeTime)
                {
                    PutToPool();
                }
            }
        }

        void OnDisable()
        {
            particles.Stop();
        }

        #endregion
    }
}