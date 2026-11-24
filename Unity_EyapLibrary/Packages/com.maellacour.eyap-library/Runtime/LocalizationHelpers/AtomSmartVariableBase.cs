namespace EyapLibrary.LocalizationHelpers
{
	using System;
	using UnityAtoms;
	using UnityEngine;
	using UnityEngine.Localization.SmartFormat.Core.Extensions;
	using UnityEngine.Localization.SmartFormat.PersistentVariables;

	/// <summary>
	/// Base class for Smart String variables that read their value from a Unity Atoms variable.
	/// The localized string is refreshed every time the Atoms variable changes.
	/// See the README in this folder for the reason it exists and how to use it.
	/// </summary>
	/// <typeparam name="T">Type of the value held by the Atoms variable.</typeparam>
	/// <typeparam name="V">Type of the Atoms variable.</typeparam>
	/// <typeparam name="E">Type of the Changed event of the Atoms variable.</typeparam>
	[Serializable]
	public abstract class AtomSmartVariableBase<T, V, E> : IVariableValueChanged
		where V : AtomBaseVariable<T>, IGetOrCreateEvent
		where E : AtomEventBase
	{
		[SerializeField] private V _variable;

		[NonSerialized] private E _changedEvent;
		private Action<IVariable> _valueChanged;

		/// <summary>
		/// Raised when the value of the Atoms variable changes.
		/// The Atoms variable is only listened to while this event has subscribers.
		/// </summary>
		public event Action<IVariable> ValueChanged
		{
			add
			{
				if (_valueChanged == null)
				{
					RegisterToVariable();
				}

				_valueChanged += value;
			}
			remove
			{
				_valueChanged -= value;

				if (_valueChanged == null)
				{
					UnregisterFromVariable();
				}
			}
		}

		/// <summary>
		/// The Atoms variable providing the value.
		/// </summary>
		public V Variable
		{
			get => _variable;
			set
			{
				_variable = value;

				if (_valueChanged != null)
				{
					UnregisterFromVariable();
					RegisterToVariable();
					OnVariableChanged();
				}
			}
		}

		/// <summary>
		/// Gets the value used by the Smart String.
		/// </summary>
		/// <param name="selector">The selector being evaluated. Not used.</param>
		/// <returns>The value of the Atoms variable, or null when no variable is assigned.</returns>
		public object GetSourceValue(ISelectorInfo selector)
		{
			if (_variable == null)
			{
				return null;
			}

			return _variable.Value;
		}

		private void RegisterToVariable()
		{
			if (_variable == null)
			{
				return;
			}

			_changedEvent = _variable.GetOrCreateEvent<E>();
			_changedEvent.Register(OnVariableChanged);
		}

		private void UnregisterFromVariable()
		{
			if (_changedEvent == null)
			{
				return;
			}

			_changedEvent.Unregister(OnVariableChanged);
			_changedEvent = null;
		}

		private void OnVariableChanged()
		{
			_valueChanged?.Invoke(this);
		}
	}
}
