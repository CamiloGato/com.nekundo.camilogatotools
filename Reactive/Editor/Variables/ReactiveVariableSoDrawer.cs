using System;
using Reactive.Runtime.Variables;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Reactive.Editor.Variables
{
    [CustomPropertyDrawer(typeof(ReactiveVariableSo<>), true)]
    public class ReactiveVariableSoDrawer : PropertyDrawer
    {
        private VisualElement _valueContainer;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            ObjectField objectField = new ObjectField(property.displayName)
            {
                allowSceneObjects = false,
                objectType = typeof(ReactiveVariableSoBase)
            };
            objectField.BindProperty(property);

            _valueContainer = new VisualElement { style = { paddingLeft = 10 } };

            container.Add(objectField);
            container.Add(_valueContainer);

            objectField.RegisterValueChangedCallback(evt =>
            {
                _valueContainer.Clear();
                if (evt.newValue is ReactiveVariableSoBase variable)
                {
                    DrawValueField(variable);
                }
                else
                {
                    _valueContainer.Add(new Label("Value: --"));
                }
            });

            if (property.objectReferenceValue is ReactiveVariableSoBase initialValue)
            {
                DrawValueField(initialValue);
            }

            return container;
        }

        private void DrawValueField(ReactiveVariableSoBase reactiveVariable)
        {
            Type variableType = reactiveVariable.GetValueType();
            object currentValue = reactiveVariable.GetValueAsObject();

            if (variableType == typeof(int))
            {
                IntegerField intField = new IntegerField("Value") { value = (int) currentValue };
                intField.SetEnabled(false);
                _valueContainer.Add(intField);
            }
            else if (variableType == typeof(float))
            {
                FloatField floatField = new FloatField("Value") { value = (float) currentValue };
                floatField.SetEnabled(false);
                _valueContainer.Add(floatField);
            }
            else if (variableType == typeof(string))
            {
                TextField textField = new TextField("Value") { value = (string) currentValue };
                textField.SetEnabled(false);
                _valueContainer.Add(textField);
            }
            // else if (typeof(UnityEngine.Object).IsAssignableFrom(variableType))
            // {
            //     ObjectField objectRefField = new ObjectField("Value")
            //     {
            //         allowSceneObjects = false,
            //         value = (UnityEngine.Object) currentValue,
            //         objectType = variableType
            //     };
            //     objectRefField.SetEnabled(false);
            //     _valueContainer.Add(objectRefField);
            // }
            else
            {
                _valueContainer.Add(new Label($"Value: Not Supported ({variableType.Name})"));
            }
        }
    }
}