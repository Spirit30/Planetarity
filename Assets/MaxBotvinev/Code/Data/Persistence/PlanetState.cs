using UnityEngine;

namespace Game.Data.Persistance
{
    public class PlanetState
    {
        #region DATA

        public bool isUser;
        public int direction;
        public int orbitIndex;

        public int meshIndex;
        public int colorIndex;
        public float size;
        public string name;

        public Vector3 position;
        public float life;

        #endregion
    }
}