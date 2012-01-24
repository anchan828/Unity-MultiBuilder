using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class MultiBuilder : EditorWindow
{
	static MultiBuilder w;
	public int u=0;
	[MenuItem("MultiBuilder/MultiBuilderMenu",false,100000)]
	static void init ()
	{
		
		w = (MultiBuilder)EditorWindow.GetWindow (typeof(MultiBuilder));
	}

	private static bool isWindows = false;
	private static bool isMacOS = false;
	private static bool isWebPlayer = false;
	private static bool isiOS = false;
	private static bool isAndroid = false;
	private static bool isFlash = false;

	void OnGUI ()
	{
		EditorGUILayout.LabelField ("BuildTarget", GUILayout.Width (100));
		isWindows = EditorGUILayout.Toggle ("Windows", isWindows);
		isMacOS = EditorGUILayout.Toggle ("Mac", isMacOS);
		isWebPlayer = EditorGUILayout.Toggle ("WebPlayer", isWebPlayer);
		isiOS = EditorGUILayout.Toggle ("iOS", isiOS);
		isAndroid = EditorGUILayout.Toggle ("Android", isAndroid);
		isFlash = EditorGUILayout.Toggle ("Flash(no support)", false);
		EditorGUILayout.Space ();
		if (GUILayout.Button ("Build")) {
			BuildStart ();
		}
	}
	
	static void BuildStart ()
	{
		string editorPath = UnityEditor.EditorApplication.applicationContentsPath + "/MacOS/Unity";
		string projectPath = Application.dataPath.Replace ("/Assets", "");
		string command = "Assets/Editor/UnityMultiBuilder.sh \"" + editorPath + "\" \"" + projectPath + "\"";
		
		//Windows版　ですが多重起動できないため使えず
		//string editorPath = UnityEditor.EditorApplication.applicationContentsPath.Replace("Data","Unity.exe");
		//string projectPath = Application.dataPath.Replace ("/Assets", "");
		//string command = "Assets/Editor/UnityMultiBuilder.bat \"" + editorPath + "\" \"" + projectPath + "\"";
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
			
		System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo ("sh", command);
//		System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo ("cmd", command);
		procStartInfo.RedirectStandardOutput = false;
		procStartInfo.UseShellExecute = false;
		procStartInfo.CreateNoWindow = false;
		System.Diagnostics.Process proc = new System.Diagnostics.Process ();
		proc.StartInfo = procStartInfo;
		proc.Start ();
	}
	// ビルド対象のシーン
	private static string[] scene;
// keystore Path
//	private static string keystorePath = "batch_build/Android/piggcube.keystore";
// keystoreのパスワードはUnityEditorで設定できるが保持されないのでここに記述
//	private static string keystorePass = "hogehoge";
//private static string keyaliasPass = "hogehoge";
	
	static string path = "BuildFile";
	
	private static void SetScenes ()
	{
		Directory.CreateDirectory (path);
		scene = GetFilesMostDeep ("Assets", "*.unity");
	}
	// リリースビルド
	public static void ReleaseBuild ()
	{
	
		if (BuildiOS (true) == false)
			EditorApplication.Exit (1);
		if (BuildAndroid (true) == false)
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}
	// 開発用ビルド

	public static void BuildWindows ()
	{
		Debug.Log ("BuildWindows");
		SetScenes ();
		if (!BuildWindows (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static void BuildMacOS ()
	{
		Debug.Log ("BuildMacOS");
		SetScenes ();
		if (!BuildMacOS (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static  void BuildWebPlayer ()
	{
		Debug.Log ("BuildWebPlayer");
		SetScenes ();
		if (!BuildWebPlayer (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static  void BuildFlashPlayer ()
	{
		Debug.Log ("BuildFlashPlayer");
		SetScenes ();
		if (!BuildFlashPlayer (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	public static void BuildiOS ()
	{
		Debug.Log ("BuildiOS");
		SetScenes ();
		if (!BuildiOS (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}
	
	public static  void BuildAndroid ()
	{
		Debug.Log ("BuildAndroid");
		SetScenes ();
		if (!BuildAndroid (false))
			EditorApplication.Exit (1);
		EditorApplication.Exit (0);
	}

	private static bool BuildWindows (bool release)
	{
		BuildOptions opt = BuildOptions.None;
		// 開発用ビルドの場合のオプション設定
//		if (release == false) {
//			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
//		}
		// ビルド
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
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
		// 開発用ビルドの場合のオプション設定
//		if (release == false) {
//			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
//		}
		// ビルド
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
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
		// 開発用ビルドの場合のオプション設定
//		if (release == false) {
//			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
//		}
		// ビルド
		// シーン、出力ファイル（フォルダ）、ターゲット、オプションを指定
		string errorMsg = BuildPipeline.BuildPlayer (scene, path + "/Web", BuildTarget.WebPlayer, opt);
		
		if (string.IsNullOrEmpty (errorMsg)) {
			Debug.Log ("Build( Web ) Success.");
			return true;
		}
		
		return false;
	}

//	private static bool BuildFlashPlayer (bool release)
//	{
//		BuildOptions opt = BuildOptions.None;
//		// 開発用ビルドの場合のオプション設定
////		if (release == false) {
////			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
////		}
//		// ビルド
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
	// iOSビルド
	private static bool BuildiOS (bool release)
	{
		Debug.Log ("Start Build( iOS )");
 
		BuildOptions opt = BuildOptions.SymlinkLibraries;
		// 開発用ビルドの場合のオプション設定
//		if (release == false) {
//			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
//		}
		// ビルド
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
	// Androidビルド
	private static bool BuildAndroid (bool release)
	{
		Debug.Log ("Start Build( Android )");
 
		BuildOptions opt = BuildOptions.None;
		// 開発用ビルドの場合のオプション設定
//		if (release == false) {
//			opt |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.AllowDebugging;
//		}
		// keystoreファイルのの場所を設定
//		string keystoreName = System.IO.Directory.GetCurrentDirectory () + "/" + keystorePath;
 
		// set keystoreName
//		PlayerSettings.Android.keystoreName = keystoreName;
		// パスワードの再設定
//		PlayerSettings.Android.keystorePass = keystorePass;
		// パスワードの再設定
//		PlayerSettings.Android.keyaliasPass = keyaliasPass;

		// ビルド
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

	public static string[] GetFilesMostDeep (string stRootPath, string stPattern)
	{
		System.Collections.Specialized.StringCollection hStringCollection = (
        new System.Collections.Specialized.StringCollection ()
    );

		// このディレクトリ内のすべてのファイルを検索する
		foreach (string stFilePath in System.IO.Directory.GetFiles(stRootPath, stPattern)) {
			hStringCollection.Add (stFilePath);
		}

		// このディレクトリ内のすべてのサブディレクトリを検索する (再帰)
		foreach (string stDirPath in System.IO.Directory.GetDirectories(stRootPath)) {
			string[] stFilePathes = GetFilesMostDeep (stDirPath, stPattern);

			// 条件に合致したファイルがあった場合は、ArrayList に加える
			if (stFilePathes != null) {
				hStringCollection.AddRange (stFilePathes);
			}
		}

		// StringCollection を 1 次元の String 配列にして返す
		string[] stReturns = new string[hStringCollection.Count];
		hStringCollection.CopyTo (stReturns, 0);

		return stReturns;
	}
}
