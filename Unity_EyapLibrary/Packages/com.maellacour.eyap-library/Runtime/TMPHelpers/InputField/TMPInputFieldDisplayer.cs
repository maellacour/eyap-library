namespace EyapLibrary.TMPHelpers.InputField
{
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TMPInputFieldDisplayer : TMPInputFieldDisplayerBase
	{
		public override void UpdateTextField(string value)
		{
			_tmpInputField?.SetText(value);
		}

		public override void UpdateTextField(int value)
		{
			_tmpInputField?.SetText(value.ToString());
		}

		public override void UpdateTextField(float value)
		{
			_tmpInputField?.SetText(value.ToString());
		}

		public override void UpdateTextField(bool value)
		{
			_tmpInputField?.SetText(value.ToString());
		}

		public override void UpdateTextField(Vector3 value)
		{
			_tmpInputField?.SetText(value.ToString());
		}

		public override void UpdateTextField(Vector2 value)
		{
			_tmpInputField?.SetText(value.ToString());
		}

		public override void UpdateTextField(object value)
		{
			_tmpInputField?.SetText(value.ToString());
		}
	}
}
