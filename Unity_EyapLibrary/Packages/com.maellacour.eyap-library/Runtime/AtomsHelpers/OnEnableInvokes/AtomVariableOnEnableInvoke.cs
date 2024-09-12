namespace EyapLibrary.AtomsHelpers.OnEnableInvokes
{
	using UnityAtoms;
	using UnityEngine;
	using UnityEngine.Events;

	public abstract class AtomVariableOnEnableInvoke<T, V, UER> : MonoBehaviour
		where V : AtomBaseVariable<T>
		where UER : UnityEvent<T>
	{

		[SerializeField] protected V _variable;

		/// <summary>
		/// The Unity Event responses.
		/// NOTE: This variable is public due to this bug: https://issuetracker.unity3d.com/issues/events-generated-by-the-player-input-component-do-not-have-callbackcontext-set-as-their-parameter-type. Will be changed back to private when fixed (this could happen in a none major update).
		/// </summary>
		public UER _unityEventResponse = null;

		protected virtual void OnEnable()
		{
			_unityEventResponse?.Invoke(_variable.Value);
		}
	}
}
