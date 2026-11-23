namespace EyapLibrary.LocalizationHelpers.Tests
{
	using System;
	using NUnit.Framework;
	using EyapLibrary.LocalizationHelpers;
	using UnityAtoms.BaseAtoms;
	using UnityEngine;
	using UnityEngine.Localization;
	using UnityEngine.Localization.SmartFormat;
	using UnityEngine.Localization.SmartFormat.Core.Formatting;
	using IVariable = UnityEngine.Localization.SmartFormat.PersistentVariables.IVariable;
	using Object = UnityEngine.Object;

	[TestFixture]
	public class AtomSmartVariableTests
	{
		private IntVariable _atom;
		private IntAtomSmartVariable _smartVariable;
		private int _valueChangedCount;

		[SetUp]
		public void SetUp()
		{
			_atom = ScriptableObject.CreateInstance<IntVariable>();
			_smartVariable = new IntAtomSmartVariable { Variable = _atom };
			_valueChangedCount = 0;
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(_atom.Changed);
			Object.DestroyImmediate(_atom);
		}

		[Test]
		public void GetSourceValue_ReturnsAtomValue()
		{
			// Outside of play mode, an Atoms variable returns its initial value.
			_atom.InitialValue = 5;

			Assert.AreEqual(5, _smartVariable.GetSourceValue(null));
		}

		[Test]
		public void GetSourceValue_NoAtom_ReturnsNull()
		{
			IntAtomSmartVariable smartVariable = new IntAtomSmartVariable();

			Assert.IsNull(smartVariable.GetSourceValue(null));
		}

		[Test]
		public void ValueChanged_AtomChanges_IsRaised()
		{
			_smartVariable.ValueChanged += _ => _valueChangedCount++;

			_atom.Value = 3;

			Assert.AreEqual(1, _valueChangedCount);
		}

		[Test]
		public void ValueChanged_AfterUnsubscribe_IsNotRaised()
		{
			Action<IVariable> handler = _ => _valueChangedCount++;
			_smartVariable.ValueChanged += handler;
			_smartVariable.ValueChanged -= handler;

			_atom.Value = 3;

			Assert.AreEqual(0, _valueChangedCount);
		}

		[Test]
		public void ValueChanged_NoAtom_DoesNotThrow()
		{
			IntAtomSmartVariable smartVariable = new IntAtomSmartVariable();
			Action<IVariable> handler = _ => _valueChangedCount++;

			Assert.DoesNotThrow(() =>
			{
				smartVariable.ValueChanged += handler;
				smartVariable.ValueChanged -= handler;
			});
		}

		[Test]
		public void Variable_SetWhileSubscribed_ListensToNewAtom()
		{
			IntVariable otherAtom = ScriptableObject.CreateInstance<IntVariable>();
			_smartVariable.ValueChanged += _ => _valueChangedCount++;

			_smartVariable.Variable = otherAtom;
			_valueChangedCount = 0;
			_atom.Value = 3;
			otherAtom.Value = 4;

			Assert.AreEqual(1, _valueChangedCount);

			Object.DestroyImmediate(otherAtom.Changed);
			Object.DestroyImmediate(otherAtom);
		}

		[Test]
		public void SmartString_ResolvesValueAndTracksVariable()
		{
			_atom.InitialValue = 5;

			string result = FormatSmartString("Score: {score:D3}", "score", _smartVariable, out FormatCache cache);

			Assert.AreEqual("Score: 005", result);
			// The variables listed here are the ones a LocalizedString listens to for refreshing itself.
			CollectionAssert.Contains(cache.VariableTriggers, _smartVariable);
		}

		[TestCase(1, "1 point")]
		[TestCase(12, "12 points")]
		public void SmartString_Plural_UsesAtomValue(int score, string expected)
		{
			_atom.InitialValue = score;

			string result = FormatSmartString("{score:plural:1 point|{} points}", "score", _smartVariable, out _);

			Assert.AreEqual(expected, result);
		}

		[TestCase(true, "Ready")]
		[TestCase(false, "Waiting")]
		public void SmartString_BoolCondition_UsesAtomValue(bool isReady, string expected)
		{
			BoolVariable boolAtom = ScriptableObject.CreateInstance<BoolVariable>();
			boolAtom.InitialValue = isReady;
			BoolAtomSmartVariable smartVariable = new BoolAtomSmartVariable { Variable = boolAtom };

			string result = FormatSmartString("{isReady:Ready|Waiting}", "isReady", smartVariable, out _);

			Assert.AreEqual(expected, result);

			Object.DestroyImmediate(boolAtom);
		}

		[Test]
		public void SmartString_NoAtom_DisplaysEmptyValue()
		{
			string result = FormatSmartString("Score: {score}", "score", new IntAtomSmartVariable(), out _);

			Assert.AreEqual("Score: ", result);
		}

		private static string FormatSmartString(string format, string variableName, IVariable variable, out FormatCache cache)
		{
			SmartFormatter formatter = Smart.CreateDefaultSmartFormat();
			cache = new FormatCache
			{
				Format = formatter.Parser.ParseFormat(format, formatter.GetNotEmptyFormatterExtensionNames()),
				LocalVariables = new LocalizedString { { variableName, variable } }
			};

			return formatter.FormatWithCache(ref cache, format, null);
		}
	}
}
