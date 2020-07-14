using UnityEngine;

namespace Game.View
{
    class PlanetView : GravityBody
    {
        #region REFERENCES

        [SerializeField] MeshFilter meshFilter;
        [SerializeField] MeshRenderer[] meshRenderers;
        [SerializeField] SpriteRenderer[] spriteRenderers;
        [SerializeField] LineRenderer lineRenderer;

        #endregion

        #region VARIABLES

        Color color;
        float size;

        #endregion

        #region GETTERS / SETTERS

        public Mesh Mesh
        {
            set
            {
                meshFilter.mesh = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                UpdateColor();
            }
        }

        public float Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                transform.localScale = Vector3.one * size;
            }
        }

        public float Radius => meshRenderers[0].bounds.extents.x;

        public override float Gravity => CoreGravity * Radius;

        #endregion

        #region VIEW

        void UpdateColor()
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor("_EmissionColor", color);
            }

            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = color;
            }

            lineRenderer.startColor =
                lineRenderer.endColor = color;
        }

        #endregion
    }
}