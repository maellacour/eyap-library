namespace EyapLibrary.TMPHelpers
{
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TMPTextSmartDisplayer : TMPTextDisplayerBase
	{
		[SerializeField] protected string _textFormat = "{0}";

		public override void UpdateTextField(string value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value));
		}

		public override void UpdateTextField(int value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(float value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(bool value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector3 value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(Vector2 value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}

		public override void UpdateTextField(object value)
		{
			_tmpText?.SetText(string.Format(_textFormat, value.ToString()));
		}
	}
}
