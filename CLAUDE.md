# CLAUDE.md — eyap-library

Personal Unity package library by Mael Lacour. A collection of reusable runtime utilities and editor tools shared across Unity projects.

---

## Behavioral guidelines

**Tradeoff:** These guidelines bias toward caution over speed. For trivial tasks, use judgment.

### 1. Think before coding

Don't assume. Don't hide confusion. Surface tradeoffs.

Before implementing:
- State assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them — don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

### 2. Simplicity first

Minimum code that solves the problem. Nothing speculative.

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

### 3. Surgical changes

Touch only what you must. Clean up only your own mess.

When editing existing code:
- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it — don't delete it.

When your changes create orphans:
- Remove imports/variables/functions that **your** changes made unused.
- Don't remove pre-existing dead code unless asked.

Every changed line should trace directly to the user's request.

### 4. Goal-driven execution

Define success criteria. Loop until verified.

Transform tasks into verifiable goals:
- "Add validation" → "Write tests for invalid inputs, then make them pass"
- "Fix the bug" → "Write a test that reproduces it, then make it pass"
- "Refactor X" → "Ensure tests pass before and after"

For multi-step tasks, state a brief plan:
```
1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
```

---

## Project layout

```
eyap-library/
├── CLAUDE.md                          ← this file
├── Unity_EyapLibrary/
│   └── Packages/
│       └── com.maellacour.eyap-library/
│           ├── package.json           ← name: com.maellacour.eyap-library, version: 2.2.1
│           ├── CHANGELOG.md           ← Keep a Changelog format, Semantic Versioning
│           ├── Editor/                ← Editor-only tools (EyapLibrary.Editor asmdef)
│           ├── Runtime/
│           │   ├── AtomsHelpers/
│           │   ├── CommonManagers/
│           │   ├── Data/
│           │   ├── Extensions/
│           │   ├── FilesManagement/
│           │   ├── GeometryAndPlacement/
│           │   │   └── RandomPoints/
│           │   ├── TMPHelpers/
│           │   ├── Types/
│           │   ├── UI/
│           │   └── Utils/
│           └── Samples~/
│               └── SceneManagementDemo/
```

---

## Modules

| Namespace | Asmdef | Summary |
|---|---|---|
| `EyapLibrary.AtomsHelpers` | EyapLibrary.AtomsHelpers | MonoBehaviour wrappers for Unity Atoms variables (OnEnableInvoke, Referencers) |
| `EyapLibrary.CommonManagers` | EyapLibrary.CommonManagers | Game-level managers, e.g. pause menu |
| `EyapLibrary.Data` | EyapLibrary.Data | Data persistence abstraction: JsonSaver, BinarySaver, EncryptedJsonSaver |
| `EyapLibrary.Extensions` | EyapLibrary.Extensions | Extension methods: GameObject, String, Transform, Enumerable |
| `EyapLibrary.FilesManagement` | EyapLibrary.FilesManagement | File system utilities (e.g. find latest file matching a pattern) |
| `EyapLibrary.GeometryAndPlacement` | EyapLibrary.GeometryAndPlacement | Geometric helpers: CircleHelper (evenly spaced points on a circle) |
| `EyapLibrary.GeometryAndPlacement.RandomPoints` | EyapLibrary.GeometryAndPlacement.RandomPoints | Uniform and Poisson disk random point generation in 2D/3D |
| `EyapLibrary.TMPHelpers` | EyapLibrary.TMPHelpers | TextMeshPro display helpers: TMPTextDisplayer, TMPInputFieldDisplayer |
| `EyapLibrary.Types` | EyapLibrary.Types | Custom serializable value types: IntVector2, IntVector3 |
| `EyapLibrary.UI` | (default assembly) | UI components: Fader, StringParserToInt |
| `EyapLibrary.Utils` | EyapLibrary.Utils | Singleton, PersistentSingleton, MathUtils, EnumUtils, VersionDisplayer |
| `EyapLibrary.Editor` | EyapLibrary.Editor | Editor-only tools (see below) |

### Editor tools (`Editor/`)

All editor scripts live here under the `EyapLibrary.Editor` namespace with `includePlatforms: ["Editor"]`. All menu items are grouped under the top-level `Tools/EyapLibrary/` menu.

- **ExportLocalizationTableEditor** — `Tools/EyapLibrary/Localization/Export All CSV Files`: exports all Unity Localization string tables to CSV.
- **SceneScreenshot** — `Tools/EyapLibrary/Screenshots/`: captures Scene View and/or Game View as timestamped PNGs saved to `<ProjectRoot>/Screenshots/`. Default shortcut Ctrl+Alt+S (rebindable via **Edit > Shortcuts... > Screenshots/Take Both**).

### Tests

Modules that have tests: Extensions, FilesManagement, GeometryAndPlacement, GeometryAndPlacement.RandomPoints, Utils. Tests live in a `Tests/` subfolder inside the module, use NUnit, are Editor-only, and follow the naming pattern `EyapLibrary.{Module}.Tests`.

### Dependencies

- **com.unity-atoms.unity-atoms-base-atoms** 4.4.8 — reactive event/variable system used by AtomsHelpers and CommonManagers.
- **com.unity.localization** 1.3.2 — used by ExportLocalizationTableEditor.

---

## Coding conventions

These are enforced by `.editorconfig` at the package root. Follow them strictly.

### Namespace and file structure

- Every file must declare a namespace: `EyapLibrary.{Module}` (e.g. `EyapLibrary.Utils`).
- Sub-modules: `EyapLibrary.{Parent}.{Child}` (e.g. `EyapLibrary.GeometryAndPlacement.RandomPoints`).
- `using` directives go **inside** the namespace block, not at the top of the file.

```csharp
namespace EyapLibrary.Utils
{
    using System;
    using UnityEngine;

    public class MyClass { }
}
```

### Braces

Allman style — opening brace always on its own line:

```csharp
public void Method()
{
    if (condition)
    {
        // ...
    }
}
```

### Indentation

Tabs, not spaces.

### Fields and properties

- Private/protected fields: `_camelCase` with underscore prefix.
- Serialized inspector fields: `[SerializeField] private Type _fieldName;` (not public).
- Public properties: `PascalCase`.

### Class naming suffixes

| Pattern | Usage |
|---|---|
| `Base` | Abstract base classes |
| `Helper` / `Utility` | Static utility classes |
| `Saver` | Data persistence implementations |
| `Displayer` | UI display MonoBehaviours |
| `Tests` | NUnit test fixture classes |

### XML doc comments

All public members get triple-slash XML doc comments with `<summary>`, `<param>`, `<returns>` as appropriate.

### Error handling

Guard clauses at the top of methods. Throw `ArgumentException` / `ArgumentOutOfRangeException` with descriptive messages for invalid inputs. No defensive null checks for internal code — only validate at boundaries.

---

## Changelog discipline

- Format: [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
- Versioning: [Semantic Versioning](https://semver.org/)
- New additions go under `## [Unreleased]` with the appropriate section (`Added`, `Changed`, `Fixed`, `Removed`).
- On release, `[Unreleased]` is renamed to `[x.y.z] - YYYY-MM-DD`.

---

## Adding a new module

1. Create `Runtime/{ModuleName}/` with at least one `.cs` file.
2. Add an asmdef named `EyapLibrary.{ModuleName}` with `rootNamespace` left empty.
3. Use namespace `EyapLibrary.{ModuleName}` in all files.
4. If tests are warranted, add `Runtime/{ModuleName}/Tests/` with an Editor-only asmdef `EyapLibrary.{ModuleName}.Tests`.
5. Add the module to the table in this file.
6. Update `CHANGELOG.md` under `[Unreleased] > Added`.

## Adding an editor tool

1. Drop the `.cs` file in `Editor/`.
2. Use namespace `EyapLibrary.Editor`.
3. The existing `EyapLibrary.Editor.asmdef` covers it — no new asmdef needed unless the tool has a new dependency not already listed there.
4. Update `CHANGELOG.md`.
