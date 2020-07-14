using Game.Data;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Factory
{
    class PlanetFactory : Factory
    {
        #region REFERENCES

        [SerializeField] PlanetsConfig config;

        #endregion

        #region GETTERS

        public Planet GetPlayerPlanet()
        {
            return GetPlanet(0, 0, 1.0f, GetPlanetName(true));
        }

        public Planet GetRandomPlanet()
        {
            int randomMeshIndex = Random.Range(0, config.MeshesCount);
            int randomColorIndex = Random.Range(1, config.ColorsCount);
            float randomSize = Random.Range(config.MinSize, config.MaxSize);
            return GetPlanet(randomMeshIndex, randomColorIndex, randomSize, GetPlanetName(false));
        }

        public Planet GetPlanet(int meshIndex, int colorIndex, float size, string name)
        {
            Planet planet = (Planet)Get(Vector3.zero);

            planet.MeshIndex = meshIndex;
            planet.ColorIndex = colorIndex;

            planet.View.Mesh = config.GetMesh(meshIndex);
            planet.View.Color = config.GetColor(colorIndex);
            planet.View.Size = size;

            planet.UIView.Init(name, planet.View.Color, size);

            return planet;
        }

        string GetPlanetName(bool isPlayer)
        {
            return isPlayer ? "USER" : $"AI-{Guid.NewGuid().ToString().Substring(0, 4)}";
        }

        #endregion
    }
}