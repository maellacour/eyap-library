namespace EyapLibrary.TMPHelpers.InputField
{
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TMPInputFieldSmartDisplayer : TMPInputFieldDisplayerBase
	{
		[SerializeField] protected string _textFormat = "{0}";

		public override void UpdateTextField(string value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value));
		}

		public override void UpdateTextField(int value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(float value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(bool value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector3 value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector2 value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(object value)
		{
			_tmpInputField?.SetText(string.Format(_textFormat, value.ToString()));
		}
	}
}
