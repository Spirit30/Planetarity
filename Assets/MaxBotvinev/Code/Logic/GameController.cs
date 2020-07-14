using Game.Data;
using Game.Data.Persistance;
using Game.Logic.Factory;
using Game.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Logic
{
    class GameController : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] PlanetFactory planetFactory;
        [SerializeField] HitEffectFactory hitEffectFactory;
        [SerializeField] SunView sun;
        [SerializeField] MenuUIView menuUIView;
        [SerializeField] GameOverUIView gameOverUIView;

        #endregion

        #region DATA

        [SerializeField] GameConfig config;
        [SerializeField] float detroyMissilesMapDistance;

        #endregion

        #region VARIABLES

        List<Planet> planets = new List<Planet>();
        Queue<Planet> planetsToDestroy = new Queue<Planet>();

        List<Missile> missiles = new List<Missile>();
        Queue<Missile> missilesToDestroy = new Queue<Missile>();

        public bool IsGameStarted => planets.Count > 0;

        int currentOrbitIndex = -1;

        #endregion

        #region GETTERS / SETTERS

        public Planet UserPlanet { get; set; }
        public IReadOnlyList<Planet> Planets => planets;

        public GameState ExportGameState()
        {
            return new GameState
            {
                planets = Planets.Select((Planet p) => ExportPlanetState(p)).ToList()
            };
        }

        PlanetState ExportPlanetState(Planet planet)
        {
            return new PlanetState
            {
                isUser = planet == UserPlanet,
                direction = planet.Direction,
                orbitIndex = planet.OrbitIndex,

                meshIndex = planet.MeshIndex,
                colorIndex = planet.ColorIndex,
                size = planet.View.Size,
                name = planet.UIView.Name,

                position = planet.transform.position,
                life = planet.Life
            };
        }

        public void ImportGameState(GameState gameState)
        {
            foreach(var planetState in gameState.planets)
            {
                ImportPlanetState(planetState);
            }

            TogglePause();
        }

        public void ImportPlanetState(PlanetState planetState)
        {
            Planet planet = planetFactory.GetPlanet(
                planetState.meshIndex, 
                planetState.colorIndex, 
                planetState.size, 
                planetState.name);

            planet.IsAI = !planetState.isUser;
            planet.Direction = planetState.direction;
            planet.Orbit = config.GetOrbit(planetState.orbitIndex);
            planet.OrbitIndex = planetState.orbitIndex;

            planet.transform.position = planetState.position;
            planet.Life = planetState.life;

            if(planetState.isUser)
            {
                UserPlanet = planet;
            }

            planets.Add(planet);
        }

        #endregion

        #region INTERFACE

        public void StartNewGame()
        {
            InitPlanets();

            InitOrbits();

            TogglePause();
        }

        public void TogglePause()
        {
            Pause.IsPaused = !Pause.IsPaused;
        }

        public void AddMissile(Missile missile)
        {
            missiles.Add(missile);
        }

        public void DestroyPlanet(Planet planet)
        {
            planetsToDestroy.Enqueue(planet);
            ShowDestroyPlanetEffect(planet);
        }

        public void Reload()
        {
            Pause.IsPaused = true;
            DependencyInjector.Reload();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion

        #region IMPLEMENTATION

        void InitPlanets()
        {
            var userPlanet = planetFactory.GetPlayerPlanet();
            userPlanet.IsAI = false;
            planets.Add(userPlanet);
            UserPlanet = userPlanet;

            int planetsCount = Random.Range(config.MinPlanetsCount, config.MaxPlanetsCount);

            for (int i = 1; i <= planetsCount; ++i)
            {
                var enemyPlanet = planetFactory.GetRandomPlanet();
                enemyPlanet.IsAI = true;
                planets.Add(enemyPlanet);
            }
        }

        void InitOrbits()
        {
            for (int i = 0; i < planets.Count; ++i)
            {
                Planet planet = planets[i];
                planet.Direction = i % 2 == 0 ? 1 : -1;
                planet.Orbit = GetOrbit(out int orbitIndex);
                planet.OrbitIndex = orbitIndex;
            }
        }

        float GetOrbit(out int orbitIndex)
        {
            orbitIndex = GetOrbitIndex();
            return config.GetOrbit(orbitIndex);
        }

        int GetOrbitIndex()
        {
            ++currentOrbitIndex;

            if(currentOrbitIndex >= GameConfig.MAX_ORBITS_COUNT)
            {
                currentOrbitIndex = 0;
            }

            return currentOrbitIndex;
        }

        void ProcessMissiles()
        {
            foreach(Missile missile in missiles)
            {
                if (missile.IsJustSpawned)
                {
                    continue;
                }
                else if (IsMissileHitSun(missile))
                {
                    OnHitSun(missile);
                    DestroyMissile(missile);
                }
                else if (IsMissileOutOfMap(missile))
                {
                    DestroyMissile(missile);
                }
                else
                {
                    foreach (Planet planet in planets)
                    {
                        if(planet == missile.PlanetOwner)
                        {
                            continue;
                        }
                        else if (IsMissileHitPlanet(missile, planet))
                        {
                            OnHitPlanet(planet);
                            planet.OnDamage(missile);
                            DestroyMissile(missile);
                        }
                        else
                        {
                            missile.ApplyGravity(planet.View);
                        }
                    }

                    missile.ApplyGravity(sun);
                }
            }

            TryDestroyMissilesNow();
        }

        bool IsMissileHitPlanet(Missile missile, Planet planet)
        {
            return Vector3.Distance(missile.transform.position, planet.transform.position) < planet.View.Radius;
        }

        bool IsMissileHitSun(Missile missile)
        {
            return Vector3.Distance(missile.transform.position, sun.transform.position) < sun.Radius;
        }

        bool IsMissileOutOfMap(Missile missile)
        {
            return Vector3.Distance(missile.transform.position, Vector3.zero) > detroyMissilesMapDistance;
        }

        void OnHitPlanet(Planet planet)
        {
            hitEffectFactory.GetHitEffect(planet.transform.position, planet);
        }

        void OnHitSun(Missile missile)
        {
            hitEffectFactory.GetHitEffect(missile.transform.position, sun);
        }

        void DestroyMissile(Missile missile)
        {
            missilesToDestroy.Enqueue(missile);
        }

        void TryDestroyMissilesNow()
        {
            while(missilesToDestroy.Count > 0)
            {
                var missile = missilesToDestroy.Dequeue();
                missiles.Remove(missile);
                missile.PutToPool();
            }
        }

        void ProcessPlanets()
        {
            TryDestroyPlanetsNow();

            List<Missile> userMissiles = missiles.FindAll((Missile m) => m.PlanetOwner == UserPlanet);
            float averageUserMissilesSpeed = userMissiles.Count > 0 ? userMissiles.Average((Missile m) => m.Speed) : 0;

            foreach (var planet in planets)
            {
                Vector3 modeDelta = planet.transform.position - planet.PreviousPosition;
                Vector3 prediction = Quaternion.Euler(0, 0, 90 * planet.Direction) * (planet.transform.position + modeDelta * averageUserMissilesSpeed);
                planet.Aim.SetPosition(prediction);
            }
        }

        void ShowDestroyPlanetEffect(Planet planet)
        {
            for(int i = 0; i < 5; ++i)
            {
                hitEffectFactory.GetHitEffect(Random.onUnitSphere * planet.View.Radius, planet.View.Color);
            }
        }

        void TryDestroyPlanetsNow()
        {
            while (planetsToDestroy.Count > 0)
            {
                var planet = planetsToDestroy.Dequeue();
                planets.Remove(planet);
                planet.PutToPool();

                if (planet == UserPlanet)
                {
                    gameOverUIView.Open(false, planets.First().View.Color);
                    menuUIView.ShowDepthOfField(true);
                    menuUIView.ShowPauseButton(false);
                }
                else if(planets.Count == 1)
                {
                    gameOverUIView.Open(true, planets.First().View.Color);
                    menuUIView.ShowDepthOfField(true);
                    menuUIView.ShowPauseButton(false);
                }
            }
        }

        #endregion

        #region UNITY EVENTS

        void Awake()
        {
            DependencyInjector.Add(this);
        }

        void Update()
        {
            if(!Pause.IsPaused)
            {
                ProcessMissiles();
                ProcessPlanets();
            }
        }

        #endregion
    }
}