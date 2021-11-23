﻿namespace Mapbox.Editor
{
    using Mapbox.Unity.Map;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(TerrainColliderOptions))]
    public class TerrainColliderOptionsDrawer : PropertyDrawer
    {
        static float lineHeight = EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position.y += lineHeight;
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, lineHeight), property.FindPropertyRelative("addCollider"));

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Reserve space for the total visible properties.
            return 3.0f * lineHeight;
        }
    }
}
