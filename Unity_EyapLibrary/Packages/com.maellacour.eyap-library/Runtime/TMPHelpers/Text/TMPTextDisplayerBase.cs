namespace EyapLibrary.TMPHelpers
{
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TextMeshProUGUI))]
	public abstract class TMPTextDisplayerBase : MonoBehaviour
	{
		[SerializeField] protected TextMeshProUGUI _tmpText;

		protected virtual void Awake()
		{
			if (_tmpText == null)
			{
				_tmpText = GetComponent<TextMeshProUGUI>();
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
