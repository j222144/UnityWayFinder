﻿namespace Mapbox.Editor
{
    using Mapbox.Unity.Map;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Style))]
    public class StyleOptionsDrawer : PropertyDrawer
    {
        static float lineHeight = EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUILayout.Space(-lineHeight);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Id"), label);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Reserve space for the total visible properties.
            return lineHeight;
        }
    }
}
