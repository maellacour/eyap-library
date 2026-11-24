namespace EyapLibrary.LocalizationHelpers
{
	using System;
	using UnityAtoms.BaseAtoms;
	using UnityEngine.Localization;

	/// <summary>
	/// Smart String variable reading its value from a Unity Atoms <see cref="StringVariable"/>.
	/// </summary>
	[Serializable]
	[DisplayName("Atom String Variable")]
	public sealed class StringAtomSmartVariable : AtomSmartVariableBase<string, StringVariable, StringEvent>
	{ }
}
