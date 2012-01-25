using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class MultiBuilder : EditorWindow
{

	static MultiBuilder w;
	static string path = "BuildFile";
	private static Texture2D backGroundImage;
	private static Texture2D windowsImage;
	private static Texture2D macImage;
	private static Texture2D webImage;
	private static Texture2D iOSImage;
	private static Texture2D androidImage;
	private static Texture2D flashImage;
	private const string MULTI_BUILD_WINDOWS = "MultiBuildWindows";
	private const string MULTI_BUILD_MAC = "MultiBuildMac";
	private const string MULTI_BUILD_WEBPLAYER = "MultiBuildWebPlayer";
	private const string MULTI_BUILD_IOS = "MultiBuildiOS";
	private const string MULTI_BUILD_ANDROID = "MultiBuildAndroid";
	private const string MULTI_BUILD_FLASH = "MultiBuildFlash";
	private static string[] scene;

	[MenuItem("MultiBuilder/MultiBuilderMenu", false, 100000)]
	static void Init ()
	{
		GetPlatformData ();
		
		
		w = (MultiBuilder)EditorWindow.GetWindow (typeof(MultiBuilder));
		w.maxSize = new Vector2 (550, 600);
		w.minSize = new Vector2 (550, 600);
		w.ShowPopup ();
	}

	static void GetPlatformData ()
	{
		if (EditorPrefs.HasKey (MULTI_BUILD_WINDOWS))
			isWindows = EditorPrefs.GetBool (MULTI_BUILD_WINDOWS);
		
		if (EditorPrefs.HasKey (MULTI_BUILD_MAC))
			isMacOS = EditorPrefs.GetBool (MULTI_BUILD_MAC);
		
		if (EditorPrefs.HasKey (MULTI_BUILD_WEBPLAYER))
			isWebPlayer = EditorPrefs.GetBool (MULTI_BUILD_WEBPLAYER);
		
		if (EditorPrefs.HasKey (MULTI_BUILD_IOS))
			isiOS = EditorPrefs.GetBool (MULTI_BUILD_IOS);
		
		if (EditorPrefs.HasKey (MULTI_BUILD_ANDROID))
			isAndroid = EditorPrefs.GetBool (MULTI_BUILD_ANDROID);
		
		if (EditorPrefs.HasKey (MULTI_BUILD_FLASH))
			isFlash = EditorPrefs.GetBool (MULTI_BUILD_FLASH);
	}

	static void SetPlatformData ()
	{
		EditorPrefs.SetBool (MULTI_BUILD_WINDOWS, isWindows);
		EditorPrefs.SetBool (MULTI_BUILD_MAC, isMacOS);
		EditorPrefs.SetBool (MULTI_BUILD_WEBPLAYER, isWebPlayer);
		EditorPrefs.SetBool (MULTI_BUILD_IOS, isiOS);
		EditorPrefs.SetBool (MULTI_BUILD_ANDROID, isAndroid);
		EditorPrefs.SetBool (MULTI_BUILD_FLASH, isFlash);
		GetPlatformData ();
	}

	private static bool isWindows, isMacOS, isWebPlayer, isiOS, isAndroid, isFlash;
	private Vector2 scroll = Vector2.zero;
	static string ImageFolderPath = "Assets/Editor/MultiBuilder/Images/";
	private static EditorBuildSettingsScene[] editorScenes = EditorBuildSettings.scenes;

	void OnGUI ()
	{
		
	
		GUIStyle style = new GUIStyle ();
		style.fontStyle = FontStyle.Bold;
		editorScenes = EditorBuildSettings.scenes;
		if (backGroundImage != null) {
			EditorGUI.DrawPreviewTexture (new Rect (20, 30, backGroundImage.width, backGroundImage.height), backGroundImage);
		} else
			backGroundImage = (Texture2D)AssetDatabase.LoadAssetAtPath (ImageFolderPath + "MultiBuilderBackgroundImage.png", typeof(Texture2D));
		GUILayout.Space (34);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Space (25);
		EditorStyles.label.fontStyle = FontStyle.Bold;
		EditorGUILayout.LabelField ("Scenes In Build  ", "  Preview Only");
		EditorStyles.label.fontStyle = FontStyle.Normal;
		EditorGUILayout.EndHorizontal ();
		GUILayout.Space (-3);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Space (25);
		scroll = EditorGUILayout.BeginScrollView (scroll, GUILayout.Width (Screen.width - 43), GUILayout.Height (Screen.height / 3 + 29));
		
		for (int i = 0; i < editorScenes.Length; i++) {
			EditorGUILayout.BeginHorizontal ();
			editorScenes [i].enabled = GUILayout.Toggle (editorScenes [i].enabled, editorScenes [i].path);
			Rect r = GUILayoutUtility.GetRect (15, 15);
			EditorGUI.LabelField (new Rect (Screen.width - 90, r.y, r.width + 5, r.height + 5), "" + i, "");
			EditorGUILayout.EndHorizontal ();
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndScrollView ();
		GUILayout.Space (23);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Space (23);
		
		if (backGroundImage != null) {
			EditorGUI.DrawPreviewTexture (new Rect (20, 50 + backGroundImage.height, backGroundImage.width / 2, backGroundImage.height), backGroundImage);
		} else
			backGroundImage = (Texture2D)AssetDatabase.LoadAssetAtPath (ImageFolderPath + "MultiBuilderBackgroundImage.png", typeof(Texture2D));
		EditorStyles.label.fontStyle = FontStyle.Bold;
		EditorGUILayout.LabelField ("Platform", "", GUILayout.Width (backGroundImage.width / 2));
		EditorStyles.label.fontStyle = FontStyle.Normal;
		EditorGUILayout.EndHorizontal ();
		GUILayout.Space (4);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Space (20);
		
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.BeginHorizontal ();
		windowsImage = SetPlatformImage (windowsImage, "Windows.png");
		isWindows = EditorGUILayout.Toggle ("Windows", isWindows);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		macImage = SetPlatformImage (macImage, "Mac.png");
		isMacOS = EditorGUILayout.Toggle ("Mac", isMacOS);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		webImage = SetPlatformImage (webImage, "WebPlayer.png");
		isWebPlayer = EditorGUILayout.Toggle ("WebPlayer", isWebPlayer);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		iOSImage = SetPlatformImage (iOSImage, "iPhone.png");
		isiOS = EditorGUILayout.Toggle ("iOS", isiOS);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		androidImage = SetPlatformImage (androidImage, "Android.png");
		isAndroid = EditorGUILayout.Toggle ("Android", isAndroid);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		flashImage = SetPlatformImage (flashImage, "Flash.png");
		isFlash = EditorGUILayout.Toggle ("Flash", false);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.BeginVertical ();
		
		EditorGUILayout.EndVertical ();
		GUILayout.Space (40);
		EditorGUILayout.BeginVertical (GUILayout.Width (100));
		EditorStyles.label.fontStyle = FontStyle.Bold;
		EditorGUILayout.LabelField ("BuildOptions", "Preview Only", GUILayout.Width (backGroundImage.width / 2));
		EditorStyles.label.fontStyle = FontStyle.Normal;
		GUILayout.Space (20);
		EditorGUILayout.Toggle ("Development", EditorUserBuildSettings.development);
		EditorGUILayout.Toggle ("AutoConnect Profiler", EditorUserBuildSettings.connectProfiler);
		EditorGUILayout.Toggle ("Script Debugging", EditorUserBuildSettings.allowDebugging);
		EditorGUILayout.Toggle ("Symlink Unity Libraries", EditorUserBuildSettings.symlinkLibraries);
		EditorGUILayout.Toggle ("Streamed", EditorUserBuildSettings.webPlayerStreamed);
		EditorGUILayout.Toggle ("Offline Deployment", EditorUserBuildSettings.webPlayerOfflineDeployment);
		EditorGUILayout.EndVertical ();
		EditorGUILayout.EndHorizontal ();
		GUILayout.Space (20);
		Rect br = GUILayoutUtility.GetRect (Screen.width / 2, 25);
		if (GUI.Button (new Rect ((Screen.width / 4) * 3 - 20, Screen.height - 60, Screen.width / 4, 25), "Build")) {
			BuildStart ();
		}
		SetPlatformData ();
	}

	static Texture2D SetPlatformImage (Texture2D tex, string texURL)
	{
		Rect rect = GUILayoutUtility.GetRect (32, 32);
		if (tex != null) {
			EditorGUI.DropShadowLabel (new Rect (rect.x + 10, rect.y - 2, 32, 32), new GUIContent (tex));
		} else
			tex = (Texture2D)AssetDatabase.LoadAssetAtPath (ImageFolderPath + texURL, typeof(Texture2D));
		
		return tex;
	}

	static void BuildStart ()
	{
		
		string editorPath = "";
		string projectPath = "";
		string command = "";
		if (Application.platform.Equals (RuntimePlatform.OSXEditor)) {
			editorPath = UnityEditor.EditorApplication.applicationContentsPath + "/MacOS/Unity";
			projectPath = Application.dataPath.Replace ("/Assets", "");
			command = "Assets/Editor/MultiBuilder/UnityMultiBuilder.sh \"" + editorPath + "\" \"" + projectPath + "\"";
		}
		if (Application.platform.Equals (RuntimePlatform.WindowsEditor)) {
			editorPath = UnityEditor.EditorApplication.applicationContentsPath.Replace ("Data", "Unity.exe");
			projectPath = Application.dataPath.Replace ("/Assets", "");
			command = " \"" + editorPath + "\" \"" + projectPath + "\"";
			
		}
		if (isWindows)
			command += " MultiBuilder.BuildWindows";
		if (isMacOS)
			command += " MultiBuilder.BuildMacOS";
		if (isWebPlayer)
			command += " MultiBuilder.BuildWebPlayer";
		if (isiOS)
			command += " MultiBuilder.BuildiOS";
		if (isAndroid)
			command += " MultiBuilder.BuildAndroid";
		if (isFlash)
			command += " MultiBuilder.BuildFlashPlayer";
		string filename = "";
		if (Application.platform.Equals (RuntimePlatform.OSXEditor))
			filename = "sh";
		if (Application.platform.Equals (RuntimePlatform.WindowsEditor)) {
			filename = "\"" + projectPath + "/Assets/Editor/MultiBuilder/UnityMultiBuilder.bat\"";
		}
		System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo (filename, command);
		procStartInfo.RedirectStandardOutput = false;
		procStartInfo.UseShellExecute = true;
		procStartInfo.CreateNoWindow = true;
		
		System.Diagnostics.Process proc = new System.Diagnostics.Process ();
		proc.StartInfo = procStartInfo;
		
		#if UNITY_3_4
		Debug.Log ("Unity 3.4");
		if (EditorUtility.DisplayDialog ("Save and Build", "Do you want to Save and Build?", "Yes", "No")) {
			EditorApplication.SaveScene (EditorApplication.currentScene);
			proc.Start ();
		}
		#endif
		
		#if UNITY_3_5
		Debug.Log ("Unity 3.5");
		if (EditorUtility.DisplayDialog ("Save and Build", "Do you want to Save and Build?", "Yes", "No")) {
			EditorApplication.SaveScene ();
			proc.Start ();
		}
		#endif
	}

	private static void SetScenes ()
	{
		Directory.CreateDirectory (path);
		List<string> l = new List<string> ();
		foreach (EditorBuildSettingsScene s in editorScenes)
			if (s.enabled)
				l.Add (s.path);
		
		scene = l.ToArray ();
	}

	public static void BuildWindows ()
	{
		SetScenes ();
		Debug.Log ("BuildWindows");
		if (!BuildWindows (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static void BuildMacOS ()
	{
		SetScenes ();
		Debug.Log ("BuildMacOS");
		if (!BuildMacOS (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static void BuildWebPlayer ()
	{
		SetScenes ();
		Debug.Log ("BuildWebPlayer");
		if (!BuildWebPlayer (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

//	public static  void BuildFlashPlayer ()
//	{
//		SetScenes ();
//		Debug.Log ("BuildFlashPlayer");
//		if (!BuildFlashPlayer (false))
//			EditorApplication.Exit (1);
//		EditorApplication.Exit (0);
//	}

	public static void BuildiOS ()
	{
		SetScenes ();
		Debug.Log ("BuildiOS");
		if (!BuildiOS (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static void BuildAndroid ()
	{
		SetScenes ();
		Debug.Log ("BuildAndroid");
		if (!BuildAndroid (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	private static bool BuildWindows (bool release)
	{
		BuildOptions opt = BuildOptions.None;
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		
		if (EditorUserBuildSettings.development)
			opt |= BuildOptions.Development;
		if (EditorUserBuildSettings.connectProfiler)
			opt |= BuildOptions.ConnectWithProfiler;
		if (EditorUserBuildSettings.allowDebugging)
			opt |= BuildOptions.AllowDebugging;
		
		string folder = path + "/Window";
		Directory.CreateDirectory (folder);
		string errorMsg = BuildPipeline.BuildPlayer (scene, folder + "/Windows.exe", BuildTarget.StandaloneWindows, opt);
		
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( Windows ) Success.");
			return true;
		}
		
		return false;
	}

	private static bool BuildMacOS (bool release)
	{
		BuildOptions opt = BuildOptions.None;
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		if (EditorUserBuildSettings.development)
			opt |= BuildOptions.Development;
		if (EditorUserBuildSettings.connectProfiler)
			opt |= BuildOptions.ConnectWithProfiler;
		if (EditorUserBuildSettings.allowDebugging)
			opt |= BuildOptions.AllowDebugging;
		string folder = path + "/Mac";
		Directory.CreateDirectory (folder);
		string errorMsg = BuildPipeline.BuildPlayer (scene, folder + "/Mac.app", BuildTarget.StandaloneOSXIntel, opt);
		
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( Mac ) Success.");
			return true;
		}
		
		return false;
	}

	private static bool BuildWebPlayer (bool release)
	{
		BuildOptions opt = BuildOptions.None;
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		if (EditorUserBuildSettings.webPlayerStreamed)
			opt |= BuildOptions.BuildAdditionalStreamedScenes;
		if (EditorUserBuildSettings.webPlayerOfflineDeployment)
			opt |= BuildOptions.WebPlayerOfflineDeployment;
		//if (EditorUserBuildSettings.webPlayerNaClSupport)
		//	opt |= BuildOptions.;
		
		if (EditorUserBuildSettings.development)
			opt |= BuildOptions.Development;
		if (EditorUserBuildSettings.connectProfiler)
			opt |= BuildOptions.ConnectWithProfiler;
		if (EditorUserBuildSettings.allowDebugging)
			opt |= BuildOptions.AllowDebugging;
		string errorMsg = BuildPipeline.BuildPlayer (scene, path + "/WebPlayer", BuildTarget.WebPlayer, opt);
		
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( Web ) Success.");
			return true;
		}
		
		return false;
	}

//	private static bool BuildFlashPlayer (bool release)
//	{
//		BuildOptions opt = BuildOptions.None;
//			if (EditorUserBuildSettings.development)
//			opt |= BuildOptions.Development;
//		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
//		string folder = path + "/Flash";
//		Directory.CreateDirectory (folder);
//		string errorMsg = BuildPipeline.BuildPlayer (scene, folder + "/Flash.swf", BuildTarget.FlashPlayer, opt);
//		
//		if (string.IsNullOrEmpty (errorMsg)) {
//			Debug.Log ("Build( Flash ) Success.");
//			return true;
//		}
//		
//		return false;
//	}
	private static bool BuildiOS (bool release)
	{
		BuildOptions opt = BuildOptions.SymlinkLibraries;
		if (EditorUserBuildSettings.development)
			opt |= BuildOptions.Development;
		if (EditorUserBuildSettings.connectProfiler)
			opt |= BuildOptions.ConnectWithProfiler;
		if (EditorUserBuildSettings.allowDebugging)
			opt |= BuildOptions.AllowDebugging;
		
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		string errorMsg = BuildPipeline.BuildPlayer (scene, path + "/iOS", BuildTarget.iPhone, opt);
		// errorMsgがない場合は成功
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( iOS ) Success.");
			return true;
		}
		Debug.Log ("Build( iOS ) ERROR!");
		Debug.LogError (errorMsg);
		return false;
	}

	private static bool BuildAndroid (bool release)
	{
		Debug.Log ("Start Build( Android )");
		
		BuildOptions opt = BuildOptions.None;
		if (EditorUserBuildSettings.development)
			opt |= BuildOptions.Development;
		if (EditorUserBuildSettings.connectProfiler)
			opt |= BuildOptions.ConnectWithProfiler;
		if (EditorUserBuildSettings.allowDebugging)
			opt |= BuildOptions.AllowDebugging;
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		string folder = path + "/Android";
		Directory.CreateDirectory (folder);
		string errorMsg = BuildPipeline.BuildPlayer (scene, folder + "/Android.apk", BuildTarget.Android, opt);
		// errorMsgがない場合は成功
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( Android ) Success.");
			return true;
			
		}
		Debug.Log ("Build( Android ) ERROR!");
		Debug.LogError (errorMsg);
		
		return false;
	}
	
}
