namespace EyapLibrary.TMPHelpers.Tests
{
	using NUnit.Framework;
	using EyapLibrary.TMPHelpers;
	using TMPro;
	using UnityEditor;
	using UnityEngine;

	[TestFixture]
	public class TMPTextSmartDisplayerTests
	{
		private GameObject _gameObject;
		private TextMeshProUGUI _tmpText;
		private TMPTextSmartDisplayer _displayer;

		[SetUp]
		public void SetUp()
		{
			_gameObject = new GameObject("TMPTextSmartDisplayerTests");
			_tmpText = _gameObject.AddComponent<TextMeshProUGUI>();
			_displayer = _gameObject.AddComponent<TMPTextSmartDisplayer>();
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(_gameObject);
		}

		[Test]
		public void UpdateTextField_String_InsertsValueInFormat()
		{
			SetTextFormat("Name: {0}");

			_displayer.UpdateTextField("Eyap");

			Assert.AreEqual("Name: Eyap", _tmpText.text);
		}

		[Test]
		public void UpdateTextField_Int_AppliesFormatSpecifier()
		{
			SetTextFormat("Score: {0:D3}");

			_displayer.UpdateTextField(7);

			Assert.AreEqual("Score: 007", _tmpText.text);
		}

		[Test]
		public void UpdateTextField_Float_AppliesFormatSpecifier()
		{
			SetTextFormat("{0:F2}");

			_displayer.UpdateTextField(3.14159f);

			Assert.AreEqual(3.14159f.ToString("F2"), _tmpText.text);
		}

		[Test]
		public void UpdateTextField_NullObject_DisplaysEmptyValue()
		{
			SetTextFormat("Value: {0}");

			_displayer.UpdateTextField((object)null);

			Assert.AreEqual("Value: ", _tmpText.text);
		}

		private void SetTextFormat(string textFormat)
		{
			// Awake is not called in edit mode, so the text field is assigned here too.
			SerializedObject serializedDisplayer = new SerializedObject(_displayer);
			serializedDisplayer.FindProperty("_tmpText").objectReferenceValue = _tmpText;
			serializedDisplayer.FindProperty("_textFormat").stringValue = textFormat;
			serializedDisplayer.ApplyModifiedPropertiesWithoutUndo();
		}
	}
}
