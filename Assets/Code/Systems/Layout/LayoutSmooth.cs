using Unity.Mathematics;

namespace UnityEngine.UI.Effects
{
    public abstract class LayoutSmooth : HorizontalOrVerticalLayoutGroup
    {
        protected float2[] _from = new float2[0], _to = new float2[0];
        protected float _speed = 10f;

        protected abstract bool _isVertical { get; }
        protected bool _isFirstTime = true;

        protected override void Start() { base.Start(); CachePositions(); }

        #if UNITY_EDITOR
        protected override void Update() { base.Update(); if (Application.isPlaying) LerpPositions(); }
        #else
        private void Update() { if (Application.isPlaying) LerpPositions(); }
        #endif

        protected void CachePositions() { int n = rectChildren.Count; _from = new float2[n]; _to = new float2[n]; }

        public override void CalculateLayoutInputHorizontal() { base.CalculateLayoutInputHorizontal(); CalcAlongAxis(0, _isVertical); }
        public override void CalculateLayoutInputVertical() => CalcAlongAxis(1, _isVertical);
        public override void SetLayoutHorizontal() { if (!Application.isPlaying) SetChildrenAlongAxis(0, _isVertical); }
        public override void SetLayoutVertical() { if (!Application.isPlaying) SetChildrenAlongAxis(1, _isVertical); }

        private void LerpPositions()
        {
            float delta = _speed * Time.deltaTime;
            for (int i = 0; i < rectChildren.Count; i++)
            {
                float2 smoothedPosition = math.lerp(rectChildren[i].anchoredPosition, _to[i], delta);
                rectChildren[i].anchoredPosition = smoothedPosition;
            }
        }
        protected void SaveCurrentPositions()
        {
            for (int i = 0; i < rectChildren.Count; i++)
                _from[i] = rectChildren[i].anchoredPosition;
        }
    }
}