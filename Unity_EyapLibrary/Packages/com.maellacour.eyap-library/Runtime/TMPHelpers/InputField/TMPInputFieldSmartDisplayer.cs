namespace EyapLibrary.TMPHelpers
{
	using TMPro;
	using UnityEngine;

	public class TMPInputFieldSmartDisplayer : TMPInputFieldDisplayerBase
	{
		[SerializeField] protected string _textFormat = "{0}";

		public override void UpdateTextField(string value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value));
		}

		public override void UpdateTextField(int value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(float value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(bool value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector3 value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector2 value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(object value)
		{
			_tmpInputField?.SetTextWithoutNotify(string.Format(_textFormat, value.ToString()));
		}
	}
}
