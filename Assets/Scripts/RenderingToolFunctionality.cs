using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[System.Serializable]
public class RenderingToolFunctionality
{
	///Core functionality from lighting setup box
	
	//Inputs
	public RenderingPath renderToolPath;

	//Bool
	public bool boolSkybox = true;
	public bool boolGradient = false;
	public bool boolSolidColor = false;
	
	//Arrays
	public GameObject[] sceneCameras;
	
	//Int
	public int cameraIndex;
	
	//Enums

	public enum enumLightShadowTypes
	{
		NoShadows = 0,
		HardShadows = 1,
		SoftShadows = 2
	}
	public enum enumShadowTypes
	{
		DisableShadows = 0,
		HardShadows = 1,
		HardAndSoftShadows = 2
	}
	public enum enumColorSpaces
	{
		Gamma = 0,
		Linear = 1
	}

	public enum enumEnvironmentLight
	{
		Skybox = 0,
		Gradient = 1,
		Color = 2
	}

	public enum enumShadowProject
	{
		CloseFit = 0,
		StableFit = 1
	}

	public enum enumRenderPaths 
	{
		UseGraphicsSettings = 0,
		Forward = 1,
		Deferred = 2,
		LegacyDeferred = 3,
		LegacyVertexLit =4
	}

	public enum enumCamRenderPaths 
	{
		UseGraphicsSettings = 0,
		Forward = 1,
		Deferred = 2,
		LegacyDeferred = 3,
		LegacyVertexLit =4
	}

	public enum Tiers
	{
		Tier1 = 0,
		Tier2 = 1,
		Tier3 = 2
	}

	public enum enumShadowCascades
	{
		NoCascades = 0,
		TwoCascades = 1,
		FourCascades = 2
	}

	public enum enumShadowResolution
	{
		LowRes = 0,
		MediumRes = 1,
		HighRes = 2,
		VeryHighRes = 3
	}

	public enum enumShadowMasks
	{
		ShadowMask = 0,
		DistanceShadowMask = 1
	}

	public enum enumClearFlags
	{
		SkyBox = 0,
		SolidColor = 1,
		DepthOnly = 2,
		DontClear = 3
	}


	//FUNCTIONALITY
	public void SelectColorSpaces (enumColorSpaces colorSp)
	{
		switch (colorSp)
		{
			case enumColorSpaces.Gamma:
				PlayerSettings.colorSpace = ColorSpace.Gamma;
				break;
			case enumColorSpaces.Linear:
				PlayerSettings.colorSpace = ColorSpace.Linear;
				break;
		}
	}

	public void SelectEnvironmentLight (enumEnvironmentLight enviroLg)
	{
		switch (enviroLg)
		{
			case enumEnvironmentLight.Skybox:
				RenderSettings.ambientMode = AmbientMode.Skybox;
				boolSkybox = true;
				boolGradient = false;
				boolSolidColor = false;
				break;
			case enumEnvironmentLight.Gradient:
				RenderSettings.ambientMode = AmbientMode.Trilight;
				boolSkybox = false;
				boolGradient = true;
				boolSolidColor = false;
				break;
			case enumEnvironmentLight.Color:
				RenderSettings.ambientMode = AmbientMode.Flat;
				boolSkybox = false;
				boolGradient = false;
				boolSolidColor = true;
				break;
		}
	}

	public void SelectShadowProjection (enumShadowProject shwProj)
	{
		switch (shwProj)
		{
			case enumShadowProject.StableFit:
				QualitySettings.shadowProjection = ShadowProjection.StableFit;
				break;
			case enumShadowProject.CloseFit:
				QualitySettings.shadowProjection = ShadowProjection.CloseFit;
				break;
		}
	}

	public void SelectRenderingPath (enumRenderPaths rndpaths, Camera cam)
	{
		switch (rndpaths)
		{
			case enumRenderPaths.UseGraphicsSettings:
				renderToolPath = RenderingPath.UsePlayerSettings;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumRenderPaths.Forward:
				renderToolPath = RenderingPath.Forward;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumRenderPaths.Deferred:
				renderToolPath = RenderingPath.DeferredShading;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumRenderPaths.LegacyDeferred:
				renderToolPath = RenderingPath.DeferredLighting;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumRenderPaths.LegacyVertexLit:
				renderToolPath = RenderingPath.VertexLit;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
		}
	}

	public void SelectCamRenderingPath (enumCamRenderPaths rndCamPaths, Camera cam)
	{
		switch (rndCamPaths)
		{
			case enumCamRenderPaths.UseGraphicsSettings:
				renderToolPath = RenderingPath.UsePlayerSettings;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumCamRenderPaths.Forward:
				renderToolPath = RenderingPath.Forward;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumCamRenderPaths.Deferred:
				renderToolPath = RenderingPath.DeferredShading;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumCamRenderPaths.LegacyDeferred:
				renderToolPath = RenderingPath.DeferredLighting;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
			case enumCamRenderPaths.LegacyVertexLit:
				renderToolPath = RenderingPath.VertexLit;
				cam.renderingPath = renderToolPath;
				//implement the rendering path
				break;
		}
	}

	public void SelectShadowType (enumShadowTypes sdwTypes)
	{
		switch (sdwTypes)
		{
			case enumShadowTypes.DisableShadows:
				QualitySettings.shadows = ShadowQuality.Disable;
				break;
			case enumShadowTypes.HardShadows:
				QualitySettings.shadows = ShadowQuality.HardOnly;
				break;
			case enumShadowTypes.HardAndSoftShadows:
				QualitySettings.shadows = ShadowQuality.All;
				break;
		}
	}

	public void SelectLightShadowType (enumLightShadowTypes sdwLgTypes, Light light)
	{
		switch (sdwLgTypes)
		{
			case enumLightShadowTypes.NoShadows:
				light.shadows = LightShadows.None;
				break;
			case enumLightShadowTypes.HardShadows:
				light.shadows = LightShadows.Hard;
				break;
			case enumLightShadowTypes.SoftShadows:
				light.shadows = LightShadows.Soft;
				break;
		}
	}

	public void SelectShadowResolution (enumShadowResolution sdwRes)
	{
		switch (sdwRes)
		{
			case enumShadowResolution.LowRes:
				QualitySettings.shadowResolution = ShadowResolution.Low;
				break;
			case enumShadowResolution.MediumRes:
				QualitySettings.shadowResolution = ShadowResolution.Medium;
				break;
			case enumShadowResolution.HighRes:
				QualitySettings.shadowResolution = ShadowResolution.High;
				break;
			case enumShadowResolution.VeryHighRes:
				QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
				break;
		}
	}

	public void SelectShadowMaskMode (enumShadowMasks sdwMsk)
	{
		switch (sdwMsk)
		{
			case enumShadowMasks.ShadowMask:
				QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
				break;
			case enumShadowMasks.DistanceShadowMask:
				QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
				break;
		}
	}
}