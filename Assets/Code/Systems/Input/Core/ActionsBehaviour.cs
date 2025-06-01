using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    [Flags] public enum UIInteraction { Nothing = 0, SelfOnly = 1, AllChildren = 2 }

    public abstract class ActionsBehaviour : MonoBehaviour
    {
        protected Actions _inputs;

        protected virtual void Awake() => _inputs = new Actions();
        protected virtual void OnEnable() => _inputs.Enable();
        protected virtual void OnDisable() => _inputs.Disable();
    }
    public abstract class InteractionBehaviour : ActionsBehaviour
    {
        [SerializeField] protected UIInteraction _castObjects = UIInteraction.SelfOnly;
        protected EventSystem _system;

        private LayerMask _transparent;
        private LayerMask _ignore;

        protected override void Awake()
        {
            base.Awake();
            _system = EventSystem.current;
            _ignore = LayerMask.NameToLayer("Ignore Raycast");
            _transparent = LayerMask.NameToLayer("TransparentFX");
        }

        private List<RaycastResult> BaseResults()
        {
            if (!_system) return null;

            PointerEventData data = new(_system) { position = _inputs.UI.Point.ReadValue<Vector2>() };
            List<RaycastResult> result = new();
            _system.RaycastAll(data, result);

            return result.Count > 0 ? result.RemoveLayers(_transparent) : null;
        }
        private List<RaycastResult> ClearResults()
        {
            if (_castObjects.Equals(UIInteraction.Nothing)) return null;
            var result = BaseResults().RemoveLayers(_ignore);
            
            if (!_castObjects.HasFlag(UIInteraction.SelfOnly)) result.RemoveAll(x => x.gameObject.Equals(gameObject));
            if (!_castObjects.HasFlag(UIInteraction.AllChildren)) result.RemoveAll(x => x.gameObject.transform.IsChildOf(transform));
            return result;
        }

        public bool IsPointerOverObject(GameObject element, params GameObject[] ignore)
        {
            var results = BaseResults().RemoveObjects(ignore);
            if (results == null) return false;
            return results.FirstElement() == element;
        }
        public bool IsPointerOverType<T>(out T element, params GameObject[] ignore)
        {
            var results = BaseResults();
            element = default;

            if (results == null) return false;
            var item = results.FirstElement();
            return item != null ? item.TryGetComponent(out element) : false;
        }
        protected bool IsPointerOverUI()
        {
            var results = ClearResults();
            return results != null ? results.Count > 0 : false;
        }
    }

    public static class InteractionExtension
    {
        public static GameObject FirstElement(this List<RaycastResult> list)
        {
            var result = list.OrderByDescending(r => r.sortingLayer).ThenByDescending(r => r.sortingOrder).ThenByDescending(r => r.depth);
            if (result.Count() == 0) return null;
            return result.First().gameObject;
        }
        public static List<RaycastResult> RemoveObjects(this List<RaycastResult> list, params GameObject[] objects)
        {
            if (list == null) return null;
            return list.Where(r => objects.Contains(r.gameObject)).ToList();
        }
        public static List<RaycastResult> RemoveLayers(this List<RaycastResult> list, params LayerMask[] layers)
        {
            if (list == null) return null;
            return list.Where(r => layers.Contains(r.gameObject.layer)).ToList();
        }
    }
}