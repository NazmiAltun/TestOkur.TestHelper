namespace TestOkur.TestHelper
{
	using System;
	using System.IO;
	using System.Reflection;

	internal static class ProjectPathFinder
	{
		public static string GetPath(string projectRelativePath, Type startupType)
		{
			var assembly = GetAssembly(startupType);
			var projectName = assembly.GetName().Name;
			var applicationBasePath = GetAssemblyDirectory(assembly);
			var directoryInfo = new DirectoryInfo(applicationBasePath);
			string foundPath = null;

			do
			{
				directoryInfo = directoryInfo?.Parent ?? throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
				foundPath = GetProjectFilePath(projectName, directoryInfo, projectRelativePath);
			}
			while (foundPath == null);

			return foundPath;
		}

		private static string GetProjectFilePath(string projectName, DirectoryInfo directoryInfo, string projectRelativePath)
		{
			if (directoryInfo == null)
			{
				return null;
			}

			var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
			return projectDirectoryInfo.Exists &&
				CheckIfProjectFileExists(projectDirectoryInfo.FullName, projectName)
				? Path.Combine(projectDirectoryInfo.FullName, projectName)
				: null;
		}

		private static bool CheckIfProjectFileExists(
			string projectDirectoryFullName,
			string projectName)
		{
			return File.Exists(
				Path.Combine(projectDirectoryFullName, projectName, $"{projectName}.csproj"));
		}

		private static string GetAssemblyDirectory(Assembly assembly)
		{
			var codeBase = assembly.CodeBase;
			var uri = new UriBuilder(codeBase);
			var path = Uri.UnescapeDataString(uri.Path);

			return Path.GetDirectoryName(path);
		}

		private static Assembly GetAssembly(Type startupType)
		{
			while (startupType.GetTypeInfo().BaseType != typeof(object))
			{
				startupType = startupType.BaseType;
			}

			return startupType.GetTypeInfo().Assembly;
		}
	}
}
