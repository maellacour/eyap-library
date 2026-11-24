namespace EyapLibrary.LocalizationHelpers
{
	using System;
	using UnityAtoms.BaseAtoms;
	using UnityEngine.Localization;

	/// <summary>
	/// Smart String variable reading its value from a Unity Atoms <see cref="FloatVariable"/>.
	/// </summary>
	[Serializable]
	[DisplayName("Atom Float Variable")]
	public sealed class FloatAtomSmartVariable : AtomSmartVariableBase<float, FloatVariable, FloatEvent>
	{ }
}
