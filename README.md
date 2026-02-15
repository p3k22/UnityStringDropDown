# P3k.StringDropdown

A lightweight Unity package that turns any serialized `string` field into a
dropdown populated from a reusable `ScriptableObject` asset.

No custom inspector required — works automatically via a `PropertyDrawer`.

## Requirements

- Unity 2020.1+

## Installation

### Git URL

Add to your `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.p3k.unitystringdropdown": "https://github.com/p3k22/UnityStringDropDown.git"
  }
}
```

### Manual

Copy the `Runtime` and `Editor` folders into your project. If you are not using
assembly definitions, delete the included `.asmdef` files.

## Setup

### 1. Create a labels source

Right-click in the Project window and select **Create > P3k > String Dropdown Source**.

Fill in the labels list in the Inspector — no code required.

### 2. Add a reference to the source on the owning object

On the `MonoBehaviour` or `ScriptableObject` that will be inspected, add a
serialized `StringDropdownSource` field and assign your asset:

```csharp
using P3k.StringDropdown;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
   [SerializeField]
   private StringDropdownSource _labelsSource;
}
```

### 3. Decorate string fields with the attribute

```csharp
[StringDropdown("_labelsSource")]
[SerializeField]
private string _category;
```

The argument `"_labelsSource"` is the name of the `StringDropdownSource` field
from step 2. It must exist on the **root** `SerializedObject` (the
`MonoBehaviour` or `ScriptableObject` being inspected).

### Full example

```csharp
using P3k.StringDropdown;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
   [SerializeField]
   private StringDropdownSource _labelsSource;

   [StringDropdown("_labelsSource")]
   [SerializeField]
   private string _category;

   [StringDropdown("_labelsSource")]
   [SerializeField]
   private string _tag;
}
```

## Behaviour

| Condition | Result |
|---|---|
| Source assigned and has labels | Dropdown popup |
| Source is `null` or has no labels | Plain text field |
| Root object has no field matching the name | Plain text field |
| Attribute placed on a non-string field | Default drawer |

Duplicate and whitespace-only labels are automatically filtered out. The dropdown
entries are sorted alphabetically (case-insensitive).

## Nested Classes

The attribute works on string fields inside nested `[Serializable]` classes. The
source field is always resolved from the root `SerializedObject`, not the nested
class.

```csharp
using System;
using System.Collections.Generic;
using P3k.StringDropdown;
using UnityEngine;

[Serializable]
public class Item
{
   [StringDropdown("_labelsSource")]
   [SerializeField]
   private string _name;
}

[CreateAssetMenu(menuName = "My Project/Inventory")]
public class Inventory : ScriptableObject
{
   [SerializeField]
   private StringDropdownSource _labelsSource;

   [SerializeField]
   private List<Item> _items = new List<Item>();
}
```

In this example `Inventory` holds one `_labelsSource` reference. Every `Item` in
the list resolves the same source via
`property.serializedObject.FindProperty("_labelsSource")` on the root
`Inventory` object.

## Assembly References

If your project uses assembly definitions, reference:

- **Runtime:** `P3k.StringDropdown`
- **Editor:** `P3k.StringDropdown.Editor`

## Architecture

| Component | Layer | Description |
|---|---|---|
| `StringDropdownSource` | Runtime | `ScriptableObject` holding the serialized list of label strings. Create assets via the **Create** menu. |
| `StringDropdownAttribute` | Runtime | `PropertyAttribute` storing the source field name. |
| `StringDropdownDrawer` | Editor | `PropertyDrawer` that resolves the source asset, reads its labels, and renders a popup or text field fallback. |

## License

Free to use and extend in your projects.