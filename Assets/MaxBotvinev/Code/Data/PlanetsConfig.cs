using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    class PlanetsConfig
    {
        #region REFERENCES

        [SerializeField] Mesh[] meshes;

        #endregion

        #region DATA

        [SerializeField] Color[] colors;

        [SerializeField, Range(0.1f, 0.5f)] float minSize;
        [SerializeField, Range(0.5f, 1.0f)] float maxSize;

        #endregion

        #region GETTERS

        public int MeshesCount => meshes.Length;
        public int ColorsCount => colors.Length;
        public float MinSize => minSize;
        public float MaxSize => maxSize;

        public Mesh GetMesh(int i)
        {
            return meshes[i];
        }

        public Color GetColor(int i)
        {
            return colors[i];
        }

        #endregion
    }
}