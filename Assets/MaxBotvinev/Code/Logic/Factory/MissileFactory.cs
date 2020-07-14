using UnityEngine;

namespace Game.Logic.Factory
{
    class MissileFactory : Factory
    {
        #region GETTERS

        public Missile GetMissileForPlanet(Vector3 point, Planet planet)
        {
            Missile missile = (Missile)Get(point);
            missile.Config = planet.Turret.CurrentMissileConfig;
            missile.PlanetOwner = planet;
            missile.View.Color = planet.View.Color;
            return missile;
        }

        #endregion

        #region UNITY EVENTS

        void Awake()
        {
            DependencyInjector.Add(this);
        }

        #endregion
    }
}