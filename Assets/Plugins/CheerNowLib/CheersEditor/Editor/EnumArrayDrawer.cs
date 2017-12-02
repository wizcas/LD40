/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System;
using UnityEditor;
using UnityEngine;

namespace Cheers
{
    [CustomPropertyDrawer(typeof(EnumArray))]
    public class ArrayLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var newLabel = label;
            var arrayIndex = ParseIndex(label.text);
            var a = attribute as EnumArray;
            if (arrayIndex >= 0 && a != null)
            {
                string txtLabel = null;
                if (a.enumType != null && a.enumType.IsEnum && arrayIndex < Enum.GetValues(a.enumType).Length)
                {
                    txtLabel = Enum.GetValues(a.enumType).GetValue(arrayIndex).ToString();
                }
                else if (a.names != null && arrayIndex < a.names.Length)
                {
                    txtLabel = a.names[arrayIndex];
                }
                if (txtLabel != null)
                {
                    newLabel = new GUIContent(txtLabel);
                }
            }
            EditorGUI.PropertyField(position, property, newLabel, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private int ParseIndex(string label)
        {
            var pre = "Element ";
            if (label.StartsWith(pre))
            {
                var idx = label.Substring(pre.Length);
                return int.Parse(idx);
            }
            return -1;
        }
    }
}