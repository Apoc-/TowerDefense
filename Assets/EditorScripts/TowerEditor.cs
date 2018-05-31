﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Hexen.EditorScripts
{
    [CustomEditor(typeof(Tower))]
    class TowerEditor : Editor
    {
        private string goldCostPropName = "GoldCost";
        private string descrPropName = "Description";

        private List<string> attributePorpertyNames = new List<string>
        {
            "AttackDamage",
            "AttackSpeed",
            "AttackRange"
        };

        private string weaponHeightPropName = "WeaponHeight";
        private string itemListPropName = "Items";
        private string projectilePropName = "Projectile";
        private string iconPropName = "Icon";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var towerNameLabel = new GUIContent("Tower Name");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("EntityName"), towerNameLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty(goldCostPropName));

            var descrProp = serializedObject.FindProperty(descrPropName);
            EditorGUILayout.LabelField("Description");
            EditorGUI.BeginProperty(new Rect(), null, descrProp);

            EditorGUI.BeginChangeCheck();
            GUIStyle descrAreaStyle = EditorStyles.textArea;
            descrAreaStyle.fixedHeight = 0;
            descrAreaStyle.stretchHeight = true;

            var descr = EditorGUILayout.TextArea(descrProp.stringValue, descrAreaStyle);

            if (EditorGUI.EndChangeCheck())
                descrProp.stringValue = descr;

            EditorGUI.EndProperty();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(weaponHeightPropName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(projectilePropName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(iconPropName));
            

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

            EditorGUILayout.LabelField("Attributes", EditorStyles.boldLabel);
            attributePorpertyNames.ForEach(propName =>
            {
                EditorGUILayout.BeginHorizontal();

                var attrName = serializedObject.FindProperty(propName + ".AttributeName");
                var attrValue = serializedObject.FindProperty(propName + ".baseValue");

                var attributeNameLabel = new GUIContent(attrName.stringValue);
                EditorGUILayout.PropertyField(attrValue, attributeNameLabel);

                EditorGUILayout.EndHorizontal();
            });

            serializedObject.ApplyModifiedProperties();
        }
    }
}
