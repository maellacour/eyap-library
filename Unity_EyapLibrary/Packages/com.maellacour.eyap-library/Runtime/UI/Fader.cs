namespace EyapLibrary.UI
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityAtoms.BaseAtoms;

	public class Fader : MonoBehaviour
	{
		[SerializeField] private Image _fadeImage;
		[SerializeField] private float _fadeDuration;

		[SerializeField] private BoolEvent _fadeCompleted;

		public void Fade(bool isFadingIn) => StartCoroutine(FadeCoroutine(isFadingIn));
		/// <summary>
		/// Fade from clear to black.
		/// </summary>
		public void FadeIn() => StartCoroutine(FadeCoroutine(true));
		/// <summary>
		/// Fade from black to clear.
		/// </summary>
		public void FadeOut() => StartCoroutine(FadeCoroutine(false));
		/// <summary>
		/// Fade from clear to black, then back to clear once the first fade ends.
		/// </summary>
		public void FadeInOut() => StartCoroutine(FadeSequenceCoroutine(true));
		/// <summary>
		/// Fade from black to clear, then back to black once the first fade ends.
		/// </summary>
		public void FadeOutIn() => StartCoroutine(FadeSequenceCoroutine(false));

		IEnumerator FadeSequenceCoroutine(bool isFadeInFirst)
		{
			yield return FadeCoroutine(isFadeInFirst);
			yield return FadeCoroutine(!isFadeInFirst);
		}

		IEnumerator FadeCoroutine(bool isFadeIn)
		{
			for (float i = 0; i <= 1; i += Time.deltaTime / _fadeDuration)
			{
				_fadeImage.color = new Color(0, 0, 0, isFadeIn ? i : 1 - i);
				yield return null;
			}
			_fadeImage.color = new Color(0, 0, 0, isFadeIn ? 1 : 0);

			_fadeCompleted.Raise(isFadeIn);
		}

	}
}
