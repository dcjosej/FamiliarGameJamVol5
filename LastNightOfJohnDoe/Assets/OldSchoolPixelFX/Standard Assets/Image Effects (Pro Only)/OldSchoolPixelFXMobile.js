#if UNITY_5
import UnityStandardAssets.ImageEffects;
#endif
#pragma strict
@script ExecuteInEditMode
@script AddComponentMenu ("Image Effects/Color Adjustments/OldSchoolPixelFX Mobile")

public class OldSchoolPixelFXMobile extends PostEffectsBase
{
	public var pixelRes : Vector2 = Vector2(320,200);
	public var width : int = 320;
	public var height : int = 200;
	public var grayScaleSupression : float = 0;
	public var shader : Shader;
		
	public var useDownscale : boolean = true;
	public var useColormap : boolean = true;
	private var material : Material;

	// serialize this instead of having another 2d texture ref'ed
	public var converted3DLut2D : Texture2D = null;
	public var basedOnTempTex : String = "";

	function CheckResources () : boolean {		
		CheckSupport (false);

		material = CheckShaderAndCreateMaterial (shader, material);

		if(!isSupported)
			ReportAutoDisable ();
		return isSupported;			
	}

	function OnDisable () {
		if (material) {
			DestroyImmediate (material);
			material = null;
		}
	}

	function OnDestroy () {
		if (converted3DLut2D)
			DestroyImmediate (converted3DLut2D);
		converted3DLut2D = null;
	}

	public function SetIdentityLut () {
			var dim : int = 8;
			var newC : Color[] = new Color[dim*dim*dim];
 			var oneOverDim : float = 1.0f / (1.0f * dim - 1.0f);
 			
 			for (var b : int = 0; b < dim ; b++) {
	 			for (var g : int = 0; g < dim ; g++) {
	 				for (var r : int = 0; r < dim ; r++) {
		 				newC[r+b*dim+g*(dim*dim)] = new Color(r*oneOverDim,1.0-(g*oneOverDim),b*oneOverDim,1f);
		 			}
	 			}
 			}

			if (converted3DLut2D)
				DestroyImmediate (converted3DLut2D);
			converted3DLut2D = new Texture2D ( dim*dim,dim,TextureFormat.ARGB32, false);
			converted3DLut2D.filterMode = FilterMode.Point;
			converted3DLut2D.wrapMode = TextureWrapMode.Clamp;
			converted3DLut2D.SetPixels (newC);
			converted3DLut2D.Apply ();
			basedOnTempTex = "";		
	}

	public function ValidDimensions (tex2d : Texture2D) : boolean {
		if (!tex2d) return false;
		var h : int = tex2d.height;
		if (h != Mathf.FloorToInt(Mathf.Sqrt(tex2d.width))) {
			return false;				
		}
		return true;
	}

	public function Convert (temp2DTex : Texture2D, path : String) {
	
	

		// conversion fun: the given 2D texture needs to be of the format
		//  w * h, wheras h is the 'depth' (or 3d dimension 'dim') and w = dim * dim

		if (temp2DTex) {
			var size : int  = 8;
			var fSize : float = size;
			var lutTexture2D : Texture2D = Texture2D(size*size,size,TextureFormat.ARGB32, false);
			lutTexture2D.filterMode = FilterMode.Point;
			lutTexture2D.wrapMode = TextureWrapMode.Clamp;
			
			var colors  = new ArrayList();
			
			for (var x : int=0;x<temp2DTex.width;x++) {
				for (var y : int=0;y<temp2DTex.height;y++) {
					var tmpColor : Color  = temp2DTex.GetPixel(x,y);
					if (colors.Contains(tmpColor) == false) {
						colors.Add(tmpColor);
					}
				}
			}
			
			for (var blue : int=0;blue<size;blue++) {
				for (var green: int=0;green<size;green++) {
					for (var red: int=0;red<size;red++) {
						var mapColor : Vector3 = Vector3(red/(fSize-1),1f-green/(fSize-1),blue/(fSize-1));
						var shortestDist : float = size*size*size;
						var curColor : Vector3 = Vector3.zero;
						
						for(var c : Color in colors) {
							var vColor : Vector3  = Vector3(c.r,c.g,c.b);
							var curDist : float  = Vector3.Distance(mapColor,vColor);
							if (curDist < shortestDist) {
								if (grayScaleSupression != 0 &&  c.r == c.g && c.g == c.b) { // Gray Color
									if ((curDist+grayScaleSupression) < shortestDist) {
										shortestDist = curDist;
										curColor = vColor;
									}
								} else {
									shortestDist = curDist;
									curColor = vColor;
								}
							}
						}
						
					
						lutTexture2D.SetPixel(blue*size+red,green,new Color(curColor.x,curColor.y,curColor.z));
					}
				}
			}

			if (converted3DLut2D)
				DestroyImmediate (converted3DLut2D);
			converted3DLut2D = lutTexture2D;
			converted3DLut2D.filterMode = FilterMode.Point;
			converted3DLut2D.wrapMode = TextureWrapMode.Clamp;
			converted3DLut2D.Apply ();
			basedOnTempTex = path;
		}		
		else {
			// error, something went terribly wrong
			Debug.LogError ("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}		
	}

	function OnRenderImage (source : RenderTexture, destination : RenderTexture) {	
		if(CheckResources () == false || (useColormap == false && useDownscale == false)) {
			Graphics.Blit (source, destination);
			return;
		}
		
		if (useColormap == true) {
			if (converted3DLut2D == null) {
				SetIdentityLut ();
			}
		
			var lutWidth : int = converted3DLut2D.width;
			var lutHeight : int =  converted3DLut2D.height;
			
			material.SetFloat("_ScaleY", lutHeight);
			material.SetFloat("_Offset", 1.0f / (2.0f * lutWidth));		
			material.SetTexture("_ClutTex", converted3DLut2D);
		}
		
		if (useDownscale == true) {
			// Scale
			source.filterMode = FilterMode.Point;
			var rt : RenderTexture = RenderTexture.GetTemporary (width, height, 0, source.format);
			rt.filterMode = FilterMode.Point;
			Graphics.Blit (source, rt);
			
			if (useColormap == true) {
				Graphics.Blit (rt, destination, material, QualitySettings.activeColorSpace == ColorSpace.Linear ? 1 : 0);	
			} else {
				Graphics.Blit (rt, destination);
			}
			RenderTexture.ReleaseTemporary (rt);
		} else {
			Graphics.Blit (source, destination, material, QualitySettings.activeColorSpace == ColorSpace.Linear ? 1 : 0);
		}
	}
}
