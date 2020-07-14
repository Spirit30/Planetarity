using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    class GameConfig
    {
        #region DATA

        public const int MAX_ORBITS_COUNT = 5;

        [SerializeField, Range(2, 3)] int minPlanetsCount;
        [SerializeField, Range(4, MAX_ORBITS_COUNT)] int maxPlanetsCount;

        [SerializeField] float[] orbits = new float[MAX_ORBITS_COUNT];

        #endregion

        #region GETTERS

        public int MinPlanetsCount => minPlanetsCount;
        public int MaxPlanetsCount => maxPlanetsCount;

        public float GetOrbit(int i)
        {
            return orbits[i];
        }

        #endregion
    }
}