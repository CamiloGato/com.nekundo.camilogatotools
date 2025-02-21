using System;
using UnityEngine;

namespace Reactive.Runtime.Variables
{
    public abstract class ReactiveVariableSoBase : ScriptableObject
    {
        public abstract Type GetValueType();
        public abstract object GetValueAsObject();
    }
    
    public abstract class ReactiveVariableSo<T> : ReactiveVariableSoBase
    {
        [Serializable]
        public enum ReloadType
        {
            None,
            Awake,
            Enable,
            Start
        }
        
        public event Action<T> OnValueChanged;
        
        [SerializeField] private ReloadType reloadOn;
        [SerializeField] private T initialValue;
        
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (_value == null)
                {
                    return;
                }
                
                if (!_value.Equals(value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public override Type GetValueType()
        {
            return typeof(T);
        }

        public override object GetValueAsObject()
        {
            return Value;
        }
    }
}