namespace EyapLibrary.Editor
{
	using System;
	using System.IO;
	using UnityEditor;
	using UnityEditor.ShortcutManagement;
	using UnityEngine;

	public static class SceneScreenshot
	{
		// Anchored to the Unity project root (next to Assets/, Packages/, etc.)
		private static string OutputDir => Path.Combine(Application.dataPath, "..", "Screenshots");

		[Shortcut("Screenshots/Take Both", KeyCode.S, ShortcutModifiers.Control | ShortcutModifiers.Alt)]
		[MenuItem("Tools/Screenshots/Both")]
		private static void TakeBoth()
		{
			TakeSceneViewCapture();
			TakeGameViewCapture();
		}

		[MenuItem("Tools/Screenshots/Scene View")]
		private static void TakeSceneViewCapture()
		{
			var sceneView = SceneView.lastActiveSceneView;
			if (sceneView == null)
			{
				Debug.LogWarning("[SceneScreenshot] No active Scene View found.");
				return;
			}

			int w = (int)sceneView.position.width;
			int h = (int)sceneView.position.height;
			SaveCameraCapture(sceneView.camera, w, h, "SceneView");
		}

		[MenuItem("Tools/Screenshots/Game View")]
		private static void TakeGameViewCapture()
		{
			var cam = Camera.main ?? FindFirstActiveCamera();
			if (cam == null)
			{
				Debug.LogWarning("[SceneScreenshot] No active camera found for Game View.");
				return;
			}

			SaveCameraCapture(cam, Screen.width, Screen.height, "GameView");
		}

		private static Camera FindFirstActiveCamera()
		{
			foreach (var cam in Camera.allCameras)
				if (cam.enabled) return cam;
			return null;
		}

		private static void SaveCameraCapture(Camera cam, int width, int height, string label)
		{
			var rt = new RenderTexture(width, height, 24);
			var prevTarget = cam.targetTexture;
			cam.targetTexture = rt;
			cam.Render();
			cam.targetTexture = prevTarget;

			RenderTexture.active = rt;
			var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			tex.Apply();
			RenderTexture.active = null;
			UnityEngine.Object.DestroyImmediate(rt);

			Directory.CreateDirectory(OutputDir);

			string path = Path.Combine(OutputDir, $"{label}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
			File.WriteAllBytes(path, tex.EncodeToPNG());
			UnityEngine.Object.DestroyImmediate(tex);

			Debug.Log($"[SceneScreenshot] {label} saved to {Path.GetFullPath(path)}");
		}
	}
}
