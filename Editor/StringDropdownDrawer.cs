namespace P3k.StringDropdown.Editor
{
#if UNITY_EDITOR
   using System;
   using System.Collections.Generic;
   using System.Linq;

   using UnityEditor;

   using UnityEngine;

   /// <summary>
   ///    PropertyDrawer for <see cref="StringDropdownAttribute" />.
   ///    Resolves the sibling <see cref="StringDropdownSource" /> field on the root SerializedObject
   ///    and renders the string property as a popup. Falls back to a text field when no labels are available.
   /// </summary>
   [CustomPropertyDrawer(typeof(StringDropdownAttribute))]
   internal sealed class StringDropdownDrawer : PropertyDrawer
   {
      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
      {
         if (property.propertyType != SerializedPropertyType.String)
         {
            EditorGUI.PropertyField(position, property, label);
            return;
         }

         var attr = (StringDropdownAttribute) attribute;
         var options = GetLabelOptions(property, attr.SourceFieldName);

         if (options is {Length: > 0})
         {
            var current = property.stringValue ?? string.Empty;
            var currentIndex = Array.IndexOf(options, current);
            if (currentIndex < 0)
            {
               currentIndex = 0;
            }

            var newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);
            property.stringValue = options[Mathf.Clamp(newIndex, 0, options.Length - 1)];
         }
         else
         {
            property.stringValue = EditorGUI.TextField(position, label, property.stringValue);
         }
      }

      /// <summary>
      ///    Locates the sibling <see cref="StringDropdownSource" /> and returns sorted, unique label strings.
      /// </summary>
      private static string[] GetLabelOptions(SerializedProperty property, string sourceFieldName)
      {
         var sourceProp = property.serializedObject.FindProperty(sourceFieldName);
         var source = sourceProp?.objectReferenceValue as StringDropdownSource;
         if (!source)
         {
            return null;
         }

         var labels = source.Labels;
         if (labels is not {Count: > 0})
         {
            return null;
         }

         var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

         for (var i = 0; i < labels.Count; i++)
         {
            var s = labels[i];
            if (!string.IsNullOrWhiteSpace(s))
            {
               set.Add(s.Trim());
            }
         }

         if (set.Count == 0)
         {
            return null;
         }

         var list = new List<string>(set);
         list.Sort(StringComparer.OrdinalIgnoreCase);
         return list.ToArray();
      }
   }
#endif
}