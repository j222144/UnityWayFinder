﻿namespace Mapbox.Editor
{
    using Mapbox.Unity.Map;
    using UnityEditor;
    using UnityEngine;

    public class ModelingSectionDrawer
    {
        private string objectId = "";
        bool showModeling
        {
            get
            {
                return EditorPrefs.GetBool(objectId + "VectorSubLayerProperties_showModeling");
            }
            set
            {
                EditorPrefs.SetBool(objectId + "VectorSubLayerProperties_showModeling", value);
            }
        }
        static float _lineHeight = EditorGUIUtility.singleLineHeight;

        public void DrawUI(SerializedProperty subLayerCoreOptions, SerializedProperty layerProperty, VectorPrimitiveType primitiveTypeProp)
        {

            objectId = layerProperty.serializedObject.targetObject.GetInstanceID().ToString();

            EditorGUILayout.BeginVertical();
            showModeling = EditorGUILayout.Foldout(showModeling, new GUIContent { text = "Modeling", tooltip = "This section provides you with options to fine tune your meshes" });
            if (showModeling)
            {
                GUILayout.Space(-_lineHeight);
                EditorGUILayout.PropertyField(subLayerCoreOptions);

                if (primitiveTypeProp == VectorPrimitiveType.Line)
                {
                    GUILayout.Space(-_lineHeight);
                    var lineGeometryOptions = layerProperty.FindPropertyRelative("lineGeometryOptions");
                    EditorGUILayout.PropertyField(lineGeometryOptions);
                }

                if (primitiveTypeProp != VectorPrimitiveType.Point && primitiveTypeProp != VectorPrimitiveType.Custom)
                {
                    GUILayout.Space(-_lineHeight);
                    var extrusionOptions = layerProperty.FindPropertyRelative("extrusionOptions");
                    extrusionOptions.FindPropertyRelative("_selectedLayerName").stringValue = subLayerCoreOptions.FindPropertyRelative("layerName").stringValue;
                    EditorGUILayout.PropertyField(extrusionOptions);

                    EditorGUI.BeginChangeCheck();
                    var snapToTerrainProperty = subLayerCoreOptions.FindPropertyRelative("snapToTerrain");
                    snapToTerrainProperty.boolValue = EditorGUILayout.Toggle(snapToTerrainProperty.displayName, snapToTerrainProperty.boolValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorHelper.CheckForModifiedProperty(subLayerCoreOptions);
                    }
                }

                if (primitiveTypeProp != VectorPrimitiveType.Point)
                {
                    EditorGUI.BeginChangeCheck();
                    var combineMeshesProperty = subLayerCoreOptions.FindPropertyRelative("combineMeshes");
                    combineMeshesProperty.boolValue = EditorGUILayout.Toggle(combineMeshesProperty.displayName, combineMeshesProperty.boolValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorHelper.CheckForModifiedProperty(subLayerCoreOptions);
                    }
                }

                if (primitiveTypeProp != VectorPrimitiveType.Point && primitiveTypeProp != VectorPrimitiveType.Custom)
                {
                    GUILayout.Space(-_lineHeight);

                    var colliderOptionsProperty = layerProperty.FindPropertyRelative("colliderOptions");
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(colliderOptionsProperty);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Debug.Log("Collider UI changed");
                        EditorHelper.CheckForModifiedProperty(colliderOptionsProperty);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }



    }
}
