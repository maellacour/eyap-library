namespace EyapLibrary.UI
{
	using UnityEngine;

	public class StringParserToInt : MonoBehaviour
	{
		[SerializeField] private UnityEvent<int> _unityEventResponse;

		public void ParseFromString(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new System.ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
			}

			int newValue = int.Parse(value);
			_unityEventResponse.Raise(newValue);
		}
	}
}
