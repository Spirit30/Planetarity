using Game.Logic.Factory.Pooling;
using Game.View;
using UnityEngine;

namespace Game.Logic
{
    class Planet : PoolItem
    {
        #region REFERENCES

        [SerializeField] PlanetView view;
        [SerializeField] PlanetUIView uiView;
        [SerializeField] Turret turret;
        [SerializeField] AimView aim;

        #endregion

        #region DATA

        [SerializeField] float lifeAmount;
        [SerializeField] float baseSpeed;
        [SerializeField] AudioClip sound;

        #endregion

        #region VARIABLES

        float life;

        #endregion

        #region GETTERS / SETTERS
        public int MeshIndex { get; set; }
        public int ColorIndex { get; set; }
        public PlanetView View => view;
        public PlanetUIView UIView => uiView;

        public Turret Turret => turret;
        public AimView Aim => aim;

        public float Orbit
        {
            set
            {
                transform.position = Vector3.down * Direction * value;
            }
        }

        public int Direction { get; set; }

        public int OrbitIndex { get; set; }

        public Vector3 PreviousPosition { get; set; }

        public float Speed => baseSpeed / View.Size;

        public bool IsAI { get; set; }

        public float Life
        {
            get
            {
                return life;
            }
            set
            {
                life = value;
                UpdateLifeView();
            }
        }
        #endregion

        #region POOL EVENTS

        public override void OnGetFromPool()
        {
            Life = lifeAmount;

            Turret.ChooseMissiles();
        }

        #endregion

        #region UNITY EVENTS

        void Update()
        {
            if(!Pause.IsPaused)
            {
                RotateAround();
            }
        }

        void LateUpdate()
        {
            PreviousPosition = transform.position;
        }

        #endregion

        #region INTERFACE

        public void OnDamage(Missile missile)
        {
            Life -= missile.Damage;

            if(Life <= 0)
            {
                Destroy();
            }
        }

        #endregion

        #region IMPLEMENTATION

        void RotateAround()
        {
            float zAngle = Direction * Time.deltaTime * Speed;
            Vector3 rotationDelta = new Vector3(0, 0, zAngle);
            transform.position = RotateAround(transform.position, Vector3.zero, rotationDelta);
        }

        Vector3 RotateAround(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            Vector3 dir = point - pivot;
            dir = Quaternion.Euler(angles) * dir;
            point = dir + pivot;
            return point;
        }

        void Destroy()
        {
            GameAudio.PlaySound(sound);
            DependencyInjector.Get<GameController>().DestroyPlanet(this);
        }

        void UpdateLifeView()
        {
            float lifeProgress = Life / lifeAmount;
            UIView.SetLife(lifeProgress);
        }

        #endregion
    }
}