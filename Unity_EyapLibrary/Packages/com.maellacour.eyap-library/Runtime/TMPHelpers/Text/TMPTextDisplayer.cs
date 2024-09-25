namespace EyapLibrary.TMPHelpers
{
	using TMPro;
	using UnityEngine;

	public class TMPTextDisplayer : TMPTextDisplayerBase
	{
		public override void UpdateTextField(string value)
		{
			_tmpText?.SetText(value);
		}

		public override void UpdateTextField(int value)
		{
			_tmpText?.SetText(value.ToString());
		}

		public override void UpdateTextField(float value)
		{
			_tmpText?.SetText(value.ToString());
		}

		public override void UpdateTextField(bool value)
		{
			_tmpText?.SetText(value.ToString());
		}

		public override void UpdateTextField(Vector3 value)
		{
			_tmpText?.SetText(value.ToString());
		}

		public override void UpdateTextField(Vector2 value)
		{
			_tmpText?.SetText(value.ToString());
		}

		public override void UpdateTextField(object value)
		{
			_tmpText?.SetText(value.ToString());
		}
	}
}
