namespace EyapLibrary.LocalizationHelpers
{
	using System;
	using UnityAtoms.BaseAtoms;
	using UnityEngine.Localization;

	/// <summary>
	/// Smart String variable reading its value from a Unity Atoms <see cref="BoolVariable"/>.
	/// </summary>
	[Serializable]
	[DisplayName("Atom Bool Variable")]
	public sealed class BoolAtomSmartVariable : AtomSmartVariableBase<bool, BoolVariable, BoolEvent>
	{ }
}
