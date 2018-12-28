using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class RenderingToolWindow : EditorWindow
{
	//Texts
	[SerializeField]
 	static string windowTitle = "Render Setup";
	[SerializeField]
 	static string version = "v 1.2b";
	
	//Colors
	public Color aSkyColor = new Color (0.212f, 0.227f, 0.259f);
	public Color aEquatorColor = new Color (0.114f, 0.125f, 0.133f);
	public Color aGroundColor = new Color (0.047f, 0.043f, 0.035f);
	public Color aSolidColor = new Color (0.212f, 0.227f, 0.259f);
	public Color cBgColor = new Color (0.212f, 0.227f, 0.259f);
	public Color sunColor = new Color (0.955f, 0.929f, 0.780f);
	public Color mainLightColor;
	public Color staticLightColor;

	//Materials
	[SerializeField]
	public Material skyboxMaterial;

	//Bool
	bool storeHDR = true;
	
	//Objects
	[SerializeField]
	public Light sunLight;

	[SerializeField]
	public Light mainLight;

	[SerializeField]
	public Light solidLight;

	public Camera cam;

	//Floats
	public float sboxIntensity = 1;
	public float mLightIntensity = 1;
	public float mLightShadowStrength = 1;
	public float shdwDistance = 150;
	
	//Vectors
	Vector2 scrollPos = Vector2.zero;

	//Styles
	[SerializeField]
	GUIStyle boxStyle;

	[SerializeField]
	GUIStyle labelStyle;

	[SerializeField]
	GUIStyle textboxStyle;

	[SerializeField]
	GUIStyle infoStyle;
	
	float defaultSpacing = 10f;
	
	//Declare Enums
	RenderingToolFunctionality.enumColorSpaces selectColorSpace;
	RenderingToolFunctionality.enumEnvironmentLight environmentLight;
	RenderingToolFunctionality.enumShadowProject shadowProject;
	RenderingToolFunctionality.enumRenderPaths renderPaths;
	RenderingToolFunctionality.enumCamRenderPaths camRenderPaths;
	RenderingToolFunctionality.enumShadowTypes shadowTypes;
	RenderingToolFunctionality.enumShadowResolution shadowResolution;
	RenderingToolFunctionality.enumShadowMasks shadowMasks;
	RenderingToolFunctionality.enumLightShadowTypes lightShadowTypes;

	//References
	public RenderingToolFunctionality rtf;

	//MAIN
	[MenuItem("Window/Rendering Setup", false, 10)]

	public static void LaunchRenderingTool ()
	{
		RenderingToolWindow renderingToolWindow = new RenderingToolWindow();
		renderingToolWindow.Init();
	}

	public void Init ()
	{
		RenderingToolWindow SetUpWindow = EditorWindow.GetWindow<RenderingToolWindow>("RST");
		SetUpWindow.maxSize = new Vector2(400f,620f);
		SetUpWindow.minSize = SetUpWindow.maxSize;
		SetUpWindow.GenerateStyles();
		skyboxMaterial = RenderSettings.skybox;
		rtf = new RenderingToolFunctionality();
		
	}

	void OnEnable ()
	{
		Debug.Log("Rendering Setup Box launched");
		cam = Camera.main;
	}

	void OnDisable()
	{
		Debug.Log("Rendering Setup Box closed");
	}
	
	[SerializeField]
	//GUI
	void OnGUI ()
	{
		//Head

		EditorGUILayout.Space();
		EditorGUILayout.SelectableLabel("Rendering Setup Toolkit", boxStyle, GUILayout.Height(30), GUILayout.ExpandWidth(true));
		EditorGUILayout.SelectableLabel("Quickly setup all the visual things you need\nto start working in your scene.", labelStyle);

		EditorGUILayout.Space();

		//Main
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
		
		GUILayout.BeginVertical(GUI.skin.box);
			EditorGUILayout.SelectableLabel("Rendering paths", labelStyle);
			EditorGUILayout.Space(); 
				
				GUILayout.BeginHorizontal();
					renderPaths = (RenderingToolFunctionality.enumRenderPaths)EditorGUILayout.EnumPopup("Render path: ", renderPaths);
					EditorGUILayout.Space(); 
						if (GUILayout.Button("Set"))
							{
								rtf.SelectRenderingPath(renderPaths, cam);
							}
				GUILayout.EndHorizontal();
			
			EditorGUILayout.Space(); 

		GUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUI.skin.box);
			
			EditorGUILayout.SelectableLabel("Color space", labelStyle);
			EditorGUILayout.Space(); 
				
				GUILayout.BeginHorizontal();
					selectColorSpace = (RenderingToolFunctionality.enumColorSpaces)EditorGUILayout.EnumPopup("Color space: ", selectColorSpace);
					EditorGUILayout.Space(); 
						if (GUILayout.Button("Set"))
							{
								rtf.SelectColorSpaces(selectColorSpace);
							}
				GUILayout.EndHorizontal();

			EditorGUILayout.Space();

		EditorGUILayout.EndVertical();

		GUILayout.BeginVertical(GUI.skin.box);
			EditorGUILayout.SelectableLabel("Environment lighting", labelStyle);
			EditorGUILayout.Space(); 
				
				GUILayout.BeginHorizontal();
					environmentLight = (RenderingToolFunctionality.enumEnvironmentLight)EditorGUILayout.EnumPopup("Source: ", environmentLight);
					EditorGUILayout.Space();
						if (GUILayout.Button("Set")) 
							{
								rtf.SelectEnvironmentLight(environmentLight);
							}
				GUILayout.EndHorizontal();

			EditorGUILayout.Space(); 
					//Gradient
					if(rtf.boolGradient)
					{
							aSkyColor = EditorGUILayout.ColorField("Sky Color: ", aSkyColor);
								RenderSettings.ambientSkyColor = aSkyColor;
							aEquatorColor = EditorGUILayout.ColorField("Equator Color: ", aEquatorColor);
								RenderSettings.ambientEquatorColor = aEquatorColor;
							aGroundColor = EditorGUILayout.ColorField("Ground Color: ", aGroundColor);
								RenderSettings.ambientGroundColor = aGroundColor;

						EditorGUILayout.Space();
					}
					//Solid Color
					if(rtf.boolSolidColor)
					{
							aSolidColor = EditorGUILayout.ColorField("Solid Color: ", aSolidColor);
								RenderSettings.ambientLight = aSolidColor;
						EditorGUILayout.Space(); 
							EditorGUILayout.SelectableLabel("Optional", infoStyle);
						EditorGUILayout.Space();
							solidLight = (Light)EditorGUILayout.ObjectField("Ambient Light: ", solidLight, typeof(Light), allowSceneObjects: true);
							if (solidLight != null)
							{
								GUILayout.Space(defaultSpacing/2);
									staticLightColor = EditorGUILayout.ColorField("Light Color: ", solidLight.color);
									solidLight.color = staticLightColor;
							}
						
						EditorGUILayout.Space(); 
					}
					//Skybox
					if(rtf.boolSkybox)
					{
						//Select skybox
						skyboxMaterial = (Material)EditorGUILayout.ObjectField("Skybox Material: ", skyboxMaterial, typeof(Material), allowSceneObjects: true);
						RenderSettings.skybox = skyboxMaterial;
						EditorGUILayout.Space(); 
						//Select sun
						sunLight = (Light)EditorGUILayout.ObjectField("Sun Source: ", sunLight, typeof(Light), allowSceneObjects: true);
						RenderSettings.sun = sunLight;
						//Sun color
						if (sunLight != null)
						{
							EditorGUILayout.Space(); 
							sunColor = EditorGUILayout.ColorField("Sun Color: ", sunLight.color);
								sunLight.color = sunColor;
						}
						//Skybox intensity
						EditorGUILayout.Space(); 
						
						GUILayout.BeginHorizontal();
							sboxIntensity = EditorGUILayout.Slider("Intensity multiplier: ", sboxIntensity, 0, 8);
							RenderSettings.ambientIntensity = sboxIntensity;
						GUILayout.EndHorizontal();
						EditorGUILayout.Space(); 
					}
		GUILayout.EndVertical();
		GUILayout.BeginVertical (GUI.skin.box);
			EditorGUILayout.SelectableLabel("Shadow rendering settings", labelStyle);
			EditorGUILayout.Space(); 
					//Shadows soft and hard
				GUILayout.BeginHorizontal();
					shadowTypes = (RenderingToolFunctionality.enumShadowTypes)EditorGUILayout.EnumPopup("Shadow quality: ", shadowTypes);
					EditorGUILayout.Space();
					if (GUILayout.Button("Set"))
						{
							rtf.SelectShadowType(shadowTypes);
						}
				GUILayout.EndHorizontal();
			EditorGUILayout.Space(); 
					//Shadow resolution
				GUILayout.BeginHorizontal();
					shadowResolution = (RenderingToolFunctionality.enumShadowResolution)EditorGUILayout.EnumPopup("Shadow resolution: ", shadowResolution);
					EditorGUILayout.Space(); 
					if (GUILayout.Button("Set"))
						{
							rtf.SelectShadowResolution(shadowResolution);
						}
				GUILayout.EndHorizontal();
			EditorGUILayout.Space(); 
					//Shadow projection
				GUILayout.BeginHorizontal();
					shadowProject = (RenderingToolFunctionality.enumShadowProject)EditorGUILayout.EnumPopup("Shadow projection: ", shadowProject);
					EditorGUILayout.Space(); 
					if (GUILayout.Button("Set"))
						{
							rtf.SelectShadowProjection(shadowProject);
						}
				GUILayout.EndHorizontal();
			EditorGUILayout.Space();
					//Shadow distance
				GUILayout.BeginHorizontal();
					shdwDistance = EditorGUILayout.Slider("Shadow distance: ", shdwDistance, 0, 600);
						QualitySettings.shadowDistance = shdwDistance;
				GUILayout.EndHorizontal();
			EditorGUILayout.Space(); 
					//ShadowMask mode
				GUILayout.BeginHorizontal();
					shadowMasks = (RenderingToolFunctionality.enumShadowMasks)EditorGUILayout.EnumPopup("Shadowmask Mode: ", shadowMasks);
					EditorGUILayout.Space(); 
					if (GUILayout.Button("Set"))
						{
							rtf.SelectShadowMaskMode(shadowMasks);
						}
				GUILayout.EndHorizontal();

			EditorGUILayout.Space(); 
		GUILayout.EndVertical();
		GUILayout.BeginVertical (GUI.skin.box);
			EditorGUILayout.SelectableLabel("Override camera render settings", labelStyle);
			EditorGUILayout.Space(); 

				//Cameras setup
				cam = (Camera)EditorGUILayout.ObjectField("Camera: ", cam, typeof(Camera), allowSceneObjects: true);
			GUILayout.Space(defaultSpacing/2);
					if (cam != null)
					{
						GUILayout.BeginVertical(GUI.skin.box);
							//Deferred of Forward
							GUILayout.BeginHorizontal();
							camRenderPaths = (RenderingToolFunctionality.enumCamRenderPaths)EditorGUILayout.EnumPopup("Render path: ", camRenderPaths);
								if (GUILayout.Button("Set"))
								{
									rtf.SelectCamRenderingPath(camRenderPaths, cam);
								}
							GUILayout.EndHorizontal();
							GUILayout.Space(defaultSpacing/2);
							//HDR
							storeHDR = EditorGUILayout.Toggle("Allow HDR: ", storeHDR);
								if (!storeHDR)
								{
									cam.allowHDR = !cam.allowHDR;
								}
							GUILayout.Space(defaultSpacing/2);
							//Clear flags
							
							//Background
								cBgColor = EditorGUILayout.ColorField("Background: ", cBgColor);
									cam.backgroundColor = cBgColor;
						GUILayout.Space(defaultSpacing/2);
						GUILayout.EndVertical();
					}
		GUILayout.EndVertical();
		GUILayout.BeginVertical (GUI.skin.box);
			EditorGUILayout.SelectableLabel("Main light settings", labelStyle);
				GUILayout.Space(defaultSpacing);
					mainLight = (Light)EditorGUILayout.ObjectField("Main Light: ", mainLight, typeof(Light), allowSceneObjects: true);
				if (mainLight != null)
				{
					EditorGUILayout.Space();
						mainLightColor = EditorGUILayout.ColorField("Light Color: ", mainLight.color);
							mainLight.color = mainLightColor;
					EditorGUILayout.Space();
					GUILayout.BeginHorizontal();
						mLightIntensity = EditorGUILayout.Slider("Intensity multiplier: ", mLightIntensity, 0, 8);
							mainLight.intensity = mLightIntensity;
					GUILayout.EndHorizontal();
					EditorGUILayout.Space();
					GUILayout.BeginHorizontal();
						lightShadowTypes = (RenderingToolFunctionality.enumLightShadowTypes)EditorGUILayout.EnumPopup("Shadows: ", lightShadowTypes);
						if (GUILayout.Button("Set"))
						{
							rtf.SelectLightShadowType(lightShadowTypes, mainLight);
						}
					GUILayout.EndHorizontal();
						if (lightShadowTypes != 0)
						{
							EditorGUILayout.Space();
							GUILayout.BeginHorizontal();
							mLightShadowStrength = EditorGUILayout.Slider("Shadow strength: ", mLightShadowStrength, 0, 1);
								mainLight.shadowStrength = mLightShadowStrength;
							GUILayout.EndHorizontal();
						}
				}
				EditorGUILayout.Space();
		
		GUILayout.EndVertical();
		
		//End
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical(GUI.skin.box);
		GUILayout.Box((windowTitle + " " + version), GUILayout.Height(20), GUILayout.ExpandWidth(true));
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
		//Repaint();
	}

	void OnInspectorUpdate()
	{
		this.Repaint();
	}

	//STYLES
	public void GenerateStyles ()
	{
		boxStyle = new GUIStyle ();
		boxStyle.normal.textColor = Color.white;
		boxStyle.fontSize = 20;
		boxStyle.alignment = TextAnchor.MiddleCenter;
		boxStyle.border = new RectOffset (3, 3, 3, 3);
		boxStyle.margin = new RectOffset (2, 2, 2, 2);

		labelStyle = new GUIStyle ();
		labelStyle.normal.textColor = Color.white;
		labelStyle.wordWrap = false; 
		labelStyle.alignment = TextAnchor.MiddleCenter;

		textboxStyle = new GUIStyle ();
		textboxStyle.normal.textColor = Color.white;
		textboxStyle.wordWrap = false;

		infoStyle = new GUIStyle ();
		infoStyle.normal.textColor = Color.grey;
		infoStyle.wordWrap = false;
		infoStyle.alignment = TextAnchor.MiddleCenter;
	}
}