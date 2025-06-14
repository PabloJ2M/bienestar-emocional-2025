namespace UnityEngine.Animations
{
    public class TweenScale : TweenTransform
    {
        [SerializeField, Range(0, 2)] private float _scaleFactor = 1f;

        protected virtual void Start()
        {
            _from = _to = _transform.localScale;
            _to *= _scaleFactor;
        }

        public void ScaleOn() => _tweenCore.Play(true);
        public void ScaleOff() => _tweenCore.Play(false);

        protected override void OnPerformePlay(bool value)
        {
            CancelTween();

            //create tween animation
            LTDescr tween = LeanTween.scale(_self, value ? _to : _from, _tweenCore.Time).setEase(_animationCurve);
            if (_tweenCore.IgnoreTimeScale) tween.setIgnoreTimeScale(true);
            if (_tweenCore.Delay != 0) tween.setDelay(_tweenCore.Delay);
            if (_tweenCore.IsLoop) tween.setLoopPingPong(-1);
            tween.setOnUpdateVector3(OnUpdate);
            tween.setOnComplete(OnComplete);

            //saved tween ID
            _tweenID = tween.uniqueId;
        }
    }
}