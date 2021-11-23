﻿namespace Mapbox.Editor
{
    using Mapbox.Unity.Map;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(RangeTileProviderOptions))]
    public class RangeTileProviderOptionsDrawer : PropertyDrawer
    {
        static float lineHeight = EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            foreach (var item in property)
            {
                var subproperty = item as SerializedProperty;
                EditorGUILayout.PropertyField(subproperty, true);
                position.height = lineHeight;
                position.y += lineHeight;
            }
        }
    }
}