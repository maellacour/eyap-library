namespace EyapLibrary.LocalizationHelpers
{
	using System;
	using UnityAtoms.BaseAtoms;
	using UnityEngine.Localization;

	/// <summary>
	/// Smart String variable reading its value from a Unity Atoms <see cref="IntVariable"/>.
	/// </summary>
	[Serializable]
	[DisplayName("Atom Int Variable")]
	public sealed class IntAtomSmartVariable : AtomSmartVariableBase<int, IntVariable, IntEvent>
	{ }
}
