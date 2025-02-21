using System;
using Reactive.Runtime.Variables;
using UnityEditor;

namespace Reactive.Editor.Variables
{
    [CustomEditor(typeof(ReactiveVariableSo<>), true)]
    public class ReactiveVariableSoEditor : UnityEditor.Editor
    {
        private SerializedProperty _reloadType;
        private SerializedProperty _initialValue;

        private void OnEnable()
        {
            _reloadType = serializedObject.FindProperty("reloadOn");
            _initialValue = serializedObject.FindProperty("initialValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_reloadType);

            if (_reloadType.enumValueIndex != 0)
            {
                Type valueType = ((ReactiveVariableSoBase)target).GetValueType();
                DrawInitialValueField(valueType);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawInitialValueField(Type fieldType)
        {
            if (fieldType == typeof(int))
            {
                _initialValue.intValue = EditorGUILayout.IntField(
                    "Initial Value",
                    _initialValue.intValue
                );
            }
            else if (fieldType == typeof(float))
            {
                _initialValue.floatValue = EditorGUILayout.FloatField(
                    "Initial Value",
                    _initialValue.floatValue
                );
            }
            else if (fieldType == typeof(string))
            {
                _initialValue.stringValue = EditorGUILayout.TextField(
                    "Initial Value",
                    _initialValue.stringValue
                );
            }
            // else if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
            // {
            //     _initialValue.objectReferenceValue = EditorGUILayout.ObjectField(
            //         "Initial Value",
            //         _initialValue.objectReferenceValue,
            //         fieldType,
            //         true
            //     );
            // }
            else
            {
                EditorGUILayout.HelpBox("Unsupported type for Initial Value", MessageType.Warning);
            }
        }
    }
}
