@CustomEditor (OldSchoolPixelFXDitherMobile)

class OldSchoolPixelFXDitherMobileEditor extends Editor 
{	
	var serObj : SerializedObject;	

	function OnEnable () {
		serObj = new SerializedObject (target);
	}

  private var tempClutTex2D : Texture2D;

  function OnInspectorGUI ()
  {         
  	serObj.Update ();
  	
  	var osp : OldSchoolPixelFXDitherMobile = (target as OldSchoolPixelFXDitherMobile);
  	
  	var oldDownScale : boolean = osp.useDownscale;
  	var oldUseColormap : boolean = osp.useColormap;
  	var updateOSP : boolean = false;
  	
	osp.useDownscale = GUILayout.Toggle(osp.useDownscale, "Use Downscale of Resolution:");
	if (osp.useDownscale != oldDownScale) { updateOSP = true; }
  
  	if (osp.useDownscale == true) {
	    var width : int = EditorGUILayout.IntField(" Resolution Width:",osp.width);
	    var height : int = EditorGUILayout.IntField(" Resolution Height:",osp.height);
	    
	    if (width != osp.width || height != osp.height) {
	    	osp.width = width;
	    	osp.height = height;
	    	updateOSP = true;
	    }
    }

	osp.useColormap = GUILayout.Toggle(osp.useColormap, "Use Custom Colormap:");
	if (osp.useColormap != oldUseColormap) { updateOSP = true; }
	
	if (osp.useColormap == true) {
	    var r : Rect; var t : Texture2D;
	
	    //EditorGUILayout.Space();
	    tempClutTex2D = EditorGUILayout.ObjectField (" Colormap ", tempClutTex2D, Texture2D, false) as Texture2D;
	    if (tempClutTex2D == null) {
	      t = AssetDatabase.LoadMainAssetAtPath(osp.basedOnTempTex) as Texture2D;
	      if (t) tempClutTex2D = t;
	    }

	
	    var tex : Texture2D = tempClutTex2D;
	


	    if (tex) 
	    {
	    	if (tex.width > 256 || tex.height > 256 || tex.width*tex.height > 16384) {
	    		EditorGUILayout.HelpBox ("Resolution of the palette is high! It is recommended to use a smaller palette since only 512 colors can be used! Palette calculation times can be VERY long if you proceed!", MessageType.Error);
	    	}
	    	
	    	var ditherTexNew : Texture2D = EditorGUILayout.ObjectField (" DitherMap ", osp.ditherTex, Texture2D, false) as Texture2D;
	    	if (ditherTexNew != osp.ditherTex) {
	    		osp.ditherTex = ditherTexNew;
	    		updateOSP = true;
	    	}
	    	
	    	
	    EditorGUILayout.LabelField("Grayscale Suppression:");
	    osp.grayScaleSupression  =  EditorGUILayout.Slider(osp.grayScaleSupression,0,Mathf.Sqrt(3)); 

	      EditorGUILayout.Separator();
		  if ((osp.basedOnTempTex != AssetDatabase.GetAssetPath (tex) && GUILayout.Button ("Convert and Apply")) || (osp.basedOnTempTex == AssetDatabase.GetAssetPath (tex) && GUILayout.Button ("Redo Convert")) )
	      {
	        var path : String = AssetDatabase.GetAssetPath (tex);
	        var textureImporter : TextureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
	        var doImport : boolean = false;
	        if (textureImporter.isReadable == false) {
	            doImport = true;
	        }
	        if (textureImporter.mipmapEnabled == true) {
	            doImport = true;
	        }
	        if (textureImporter.textureFormat != TextureImporterFormat.AutomaticTruecolor) {
	            doImport = true;          
	        }
	
	        if (doImport) 
	        {
	          textureImporter.isReadable = true;
	          textureImporter.mipmapEnabled = false;
	          textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
	          AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);
	        }
	
	        osp.Convert (tex, path);
	      }
	    }
	    
	    if (osp.basedOnTempTex != "") {
	      EditorGUILayout.HelpBox ("Using " + osp.basedOnTempTex, MessageType.Info);
	      t = AssetDatabase.LoadMainAssetAtPath(osp.basedOnTempTex) as Texture2D;
	      if (t) {
	        r = GUILayoutUtility.GetLastRect();
	        r = GUILayoutUtility.GetRect(r.width, 20);
	        r.x += r.width * 0.05f/2.0f;
	        r.width *= 0.95f;
	        GUI.DrawTexture (r, t);
	        GUILayoutUtility.GetRect(r.width, 4);
	      }
	    }
    }
	
	if (updateOSP == true) {
		// Silly update method...
		osp.enabled = false;
		osp.enabled = true;
	}
    
    if (osp.useDownscale == false && osp.useColormap == false) {
   	 EditorGUILayout.HelpBox ("No downscaling or colormap used. You can disable this Image Effect for better performance!", MessageType.Info);
    }
  	    	
  	serObj.ApplyModifiedProperties();
  }
}
