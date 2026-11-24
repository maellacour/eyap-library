# LocalizationHelpers

Use Unity Atoms variables inside localized Smart Strings, with the text refreshing by itself when a variable changes.

## Why this exists

A text such as `Score: 12` has two parts: a sentence that must be translated, and a value that changes during the game.

- `TMPTextSmartDisplayer` handles the value, but its format (`Score: {0}`) is a plain string on the component, so it cannot be translated.
- Unity Localization handles the translation with [Smart Strings](https://docs.unity3d.com/Packages/com.unity.localization@1.5/manual/Smart/SmartStrings.html) (`Score: {score}`), but it cannot use a Unity Atoms variable directly:
  - A Smart String variable is stored with `[SerializeReference]`, which cannot hold a ScriptableObject. An Atoms variable is a ScriptableObject.
  - The built-in **Object Reference** variable can point to an Atoms variable (`{score.Value}`), but the text is not refreshed when the value changes. Unity only refreshes a localized string when one of its variables raises `ValueChanged`, and nothing connects that to the Atoms `Changed` event.

The classes in this module are that missing connection. Each one wraps an Atoms variable, gives its value to the Smart String, and raises `ValueChanged` when the Atoms variable changes.

| Class | Atoms variable | Name in the inspector menu |
|---|---|---|
| `IntAtomSmartVariable` | `IntVariable` | Atom Int Variable |
| `FloatAtomSmartVariable` | `FloatVariable` | Atom Float Variable |
| `StringAtomSmartVariable` | `StringVariable` | Atom String Variable |
| `BoolAtomSmartVariable` | `BoolVariable` | Atom Bool Variable |

## How to use it

### On one text (local variable)

1. In the String Table, write the entry with a named placeholder, for example `Score: {score}`, and tick **Smart** for that entry.
2. On the TextMeshPro object, add a **Localize String Event** component (or right-click the TextMeshPro component and choose **Localize**) and select the entry in **String Reference**.
3. Under **Local Variables**, click **+** and choose **Atom Int Variable**.
4. Name it `score` (the name used in the placeholder) and assign the Atoms `IntVariable` asset.

The text now updates when the Atoms variable changes and when the language changes. No `OnEnableInvoke`, event listener or displayer component is needed on that text.

### Shared by many texts (global variable)

1. Create a group with **Assets > Create > Localization > Variables Group** and add an **Atom Int Variable** named `score` to it.
2. In **Localization Settings > String Database > Smart Format > Sources > Persistent Variables Source**, add the group with a name, for example `game`.
3. Any Smart String can then use `{game.score}`, with nothing to set up on the text itself.

### From code

```csharp
[SerializeField] private LocalizedString _scoreText;
[SerializeField] private IntVariable _score;

private void Awake()
{
	_scoreText.Add("score", new IntAtomSmartVariable { Variable = _score });
}
```

## Smart String examples

| Entry | Result |
|---|---|
| `Score: {score}` | Score: 12 |
| `Score: {score:N0}` | Score: 1,250 (formatted for the selected language) |
| `{score:plural:1 point\|{} points}` | 1 point / 12 points |
| `Time: {time:F1}s` | Time: 3.5s |
| `{isReady:Ready\|Waiting}` | Ready / Waiting |

## Good to know

- In the editor, outside of play mode, the text shows the **initial value** of the Atoms variable.
- The Atoms variable does not need a Changed event asset: one is created at runtime if none is assigned.
- An unassigned variable displays as empty text instead of raising an error.
- To support another Atoms type, add a one-line class:

```csharp
[Serializable]
[DisplayName("Atom Vector2 Variable")]
public sealed class Vector2AtomSmartVariable : AtomSmartVariableBase<Vector2, Vector2Variable, Vector2Event>
{ }
```
