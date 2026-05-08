# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Add `SceneScreenshot` editor tool (`Tools/EyapLibrary/Screenshots/`) to capture Scene View and Game View as timestamped PNGs saved to `<ProjectRoot>/Screenshots/`. Default shortcut Ctrl+Alt+S, rebindable via **Edit > Shortcuts... > Screenshots/Take Both**.

### Changed

- Move `Editor/` folder to package root (was `Runtime/Editor/`) to follow standard Unity package layout conventions.
- Group all editor menu items under `Tools/EyapLibrary/` to avoid polluting the shared `Tools/` menu root.

## [2.2.1] - 2026-05-07

### Removed

- **BREAKING** Remove `AtomColliderReferencer` class — `ColliderVariable` no longer exists in Unity Atoms 4.4.8. Any reference to this class must be deleted from consumer projects.

## [2.2.0] - 2026-02-23

### Fixed

- Package name was ambigus between eyaplibrary and eyap-library. Changed it to eyap-library to be consistent with the git url and avoid confusion.

## [2.1.1] - 2024-09-25

### Fixed

- Remove TMP component requirements in Refactor TMPHelpers.

## [2.1.0] - 2024-09-12

### Added

- Add TMPHelpers namespace structure and update related classes (see following features).
- TMPInputFieldDisplayer and TMPInputFieldDisplayerBase classes for displaying input fields in Unity.
- TMPTextDisplayer and SmartDisplayer classes for displaying text easily in Unity.
- AtomVariableOnEnableInvoke classes for different variable types.

### Changed

- Remove IntDisplayer class and add generic StringParserToInt class for UI in Unity.
- AtomsHelpers: Folder reorganisation.

## [2.0.0-pre] - 2023-11-14

### BREAKING CHANGE

- SceneManagement moved into own package.

## [1.5.1] - 2023-10-19

### Fixed

- SceneManagement: Fix ColdStartup that loaded twice the main scene.

## [1.5.0] - 2023-10-02

### Added

- SceneManagement: Add ReloadScene method on the SceneSwitcher script.

## [1.4.2] - 2023-10-02

### Fixed

- SceneManagement: Unload scene first BEFORE loading new scenes. For that, temporarily use empty scene as active.

## [1.4.1] - 2023-09-29

### Changed

- SceneManagement: Allow replay of same scene.

## [1.4.0] - 2023-09-28

### Added

- SceneManagement: Very simple behaviour to change scene.

### Changed

- CommonManagers: Pause menu is now triggered using a bool event.

## [1.3.2] - 2023-09-27

### Fixed

- Extensions: Fix the DestroyChildren for GO / Transform extension.

## [1.3.1] - 2023-09-27

### Added

- AtomsHelper: Add IntDisplayerInputField.
- SceneManagement: ColdStartup script, to start scene from the editor.
- Samples - SceneManagement: Add a new combinations for the scenes & show ColdStartup.

## [1.2.0] - 2023-09-18

Add the SceneManagement tools, so that you can load multiple scenes efficiently, based on a scriptable object requirements.

### Added

- SceneManagement: Scene loader callable from anywhere (static class).
- SceneManagement: SceneSwitcher as a persistent Singleton.
- SceneManagement: Initialization Loader, that loads scene defined in a SO.

## [1.1.0] - 2023-09-13

### Added

- Editor: Method to export all localization files as csv. Infortunately, that add the Localization package as dependency.
- Tests: Add tests for GameObjectExtensions.
- Tests: Tests for RandomPoints.UniformRandomPoint2D.

## [1.0.0] - 2023-01-05

First release of the package.
Contains common methods, helpers and classes to create application with Unity.
Some mentions :

- Helpers for the Atom package.
- Extensions for string, enumerable, monobehaviour
- Random point picker points inside a shape (2D or 3D)
- Maths utils
