namespace EyapLibrary.FilesManagement.Tests
{
	using System;
	using System.IO;
	using System.Linq;
	using NUnit.Framework;
	using UnityEngine;
	using UnityEngine.TestTools;

	public class FileUtilityTests
	{
		private string _testDirectoryPath;
		private string _testFilePath1;
		private string _testFilePath2;

		[SetUp]
		public void SetUp()
		{
			_testDirectoryPath = Path.Combine(Path.GetTempPath(), "EyapLibrary.FilesManagement.Tests." + Guid.NewGuid().ToString("N"));
			_testFilePath1 = Path.Combine(_testDirectoryPath, "TestFile1.txt");
			_testFilePath2 = Path.Combine(_testDirectoryPath, "TestFile2.txt");

			Directory.CreateDirectory(_testDirectoryPath);
			File.WriteAllText(_testFilePath1, "Test content 1");
			File.WriteAllText(_testFilePath2, "Test content 2");

			// Back-to-back writes can share the same timestamp, so set them explicitly.
			DateTime olderTime = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			File.SetLastWriteTimeUtc(_testFilePath1, olderTime);
			File.SetLastWriteTimeUtc(_testFilePath2, olderTime.AddHours(1));
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(_testDirectoryPath, true);
		}

		[Test]
		public void GetLatestFileMatchingPattern_ReturnsLatestFile()
		{
			// Arrange
			FileInfo expectedFile = new FileInfo(_testFilePath2);

			// Act
			var actualFile = FileUtility.GetLatestFileMatchingPattern(_testDirectoryPath, "*.txt");

			// Assert
			Assert.AreEqual(expectedFile.FullName, actualFile.FullName);
		}

		[Test]
		public void GetLatestFileMatchingPattern_NoMatchingFiles_ReturnsNull()
		{
			// Act
			var actualFile = FileUtility.GetLatestFileMatchingPattern(_testDirectoryPath, "*.png");

			// Assert
			Assert.IsNull(actualFile);
		}
	}
}
