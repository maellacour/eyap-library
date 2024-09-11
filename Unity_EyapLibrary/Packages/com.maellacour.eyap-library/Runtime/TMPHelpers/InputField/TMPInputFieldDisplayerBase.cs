namespace EyapLibrary.TMPHelpers.InputField
{
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(InputField))]
	public abstract class TMPInputFieldDisplayerBase : MonoBehaviour
	{
		[SerializeField] protected TMP_InputField _tmpInputField;

		protected virtual void Awake()
		{
			if (_tmpInputField == null)
			{
				_tmpInputField = GetComponent<TextMeshProUGUI>();
			}
		}

		public abstract void UpdateTextField(string value);

		public abstract void UpdateTextField(int value);

		public abstract void UpdateTextField(float value);

		public abstract void UpdateTextField(bool value);

		public abstract void UpdateTextField(Vector3 value);
		public abstract void UpdateTextField(Vector2 value);
		public abstract void UpdateTextField(object value);
	}
}
