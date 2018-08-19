using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LinkableAssetAttribute))]
public class LinkAssetDrawer : PropertyDrawer 
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LinkableAssetAttribute localAttribute = this.attribute as LinkableAssetAttribute;
        
        if(localAttribute == null)
        {
            base.OnGUI(position, property, label);
            return;
        }

        string filter = string.Format("{0} t:{1}", property.stringValue, localAttribute.targetType);

        string[] assetUids = AssetDatabase.FindAssets(filter);
        if (assetUids == null)
        {
            base.OnGUI(position, property, label);
            return;
        }

        string targetPath = null;
        foreach(string assetUid in assetUids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetUid);
            if(assetPath.Contains(property.stringValue + "."))
            {
                targetPath = assetPath;
                break;
            }
        }

        if (String.IsNullOrEmpty(targetPath))
        {
            base.OnGUI(position, property, label);
            return;
        }

        UnityEngine.Object targetObject = AssetDatabase.LoadAssetAtPath(targetPath, localAttribute.targetType);
        UnityEngine.Object returnObject = EditorGUI.ObjectField(position, new GUIContent(label), targetObject, localAttribute.targetType, false);
        if(returnObject != targetObject)
        {
            property.stringValue = returnObject.name;
        }
    }
}
