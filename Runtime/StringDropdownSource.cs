namespace P3k.StringDropdown
{
   using System.Collections.Generic;
   using System.Linq;

   using UnityEngine;

   /// <summary>
   ///    ScriptableObject that supplies label strings to
   ///    <see cref="StringDropdownAttribute" /> fields.
   ///    Create an asset via <c>Create > P3k > String Dropdown Source</c> and populate the list.
   /// </summary>
   [CreateAssetMenu(menuName = "P3k/String Dropdown Source")]
   public class StringDropdownSource : ScriptableObject
   {
      [SerializeField]
      [Tooltip("The _labels presented in the dropdown.")]
      private List<string> _labels = new List<string>();

      /// <summary>
      ///    The list of _labels presented in the dropdown.
      /// </summary>
      public IReadOnlyList<string> Labels => _labels;
   }
}