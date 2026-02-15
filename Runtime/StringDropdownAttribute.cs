namespace P3k.StringDropdown
{
   using System;
   using System.Linq;

   using UnityEngine;

   /// <summary>
   ///    Draws a string field as a dropdown populated from a sibling <see cref="StringDropdownSource" />
   ///    reference. Falls back to a plain text field when no source is assigned or the source has no labels.
   /// </summary>
   [AttributeUsage(AttributeTargets.Field)]
   public sealed class StringDropdownAttribute : PropertyAttribute
   {
      /// <summary>
      ///    Name of a sibling serialized field (on the same object or parent SerializedObject)
      ///    that holds a <see cref="StringDropdownSource" /> reference.
      /// </summary>
      public string SourceFieldName { get; }

      /// <param name="sourceFieldName">
      ///    The name of the sibling field referencing a <see cref="StringDropdownSource" />.
      /// </param>
      public StringDropdownAttribute(string sourceFieldName)
      {
         SourceFieldName = sourceFieldName;
      }
   }
}