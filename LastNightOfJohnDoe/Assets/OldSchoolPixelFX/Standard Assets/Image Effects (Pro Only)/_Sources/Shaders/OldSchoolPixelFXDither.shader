Shader "Hidden/OldSchoolPixelFX Dither" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}	
		_DitherTex ("Dither (RGB)",2D) = "" {}	
		_DitherTexWith ("DitherTexWith", Int) = 4
		_DitherTexTotal ("DitherTexTotal", Int) = 16
		_ScreenWidth ("_ScreenWidth", Int) = 300
		_ScreenHeight ("_ScreenHeight", Int) = 200
	}

CGINCLUDE

#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv  : TEXCOORD0;
};

sampler2D _MainTex;
sampler2D _DitherTex;
sampler3D _ClutTex;

int _DitherTexWith;
int _DitherTexTotal;
int _ScreenWidth;
int _ScreenHeight;

float3 parseUVW(float4 c) {
	float3 uvw = float3(c.r,c.g,c.b);
	return uvw;
}

v2f vert( appdata_img v ) 
{
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.uv =  v.texcoord.xy;	
	return o;
} 


float4 frag(v2f i) : SV_Target 
{
	float2 screenParams = float2(_ScreenWidth,_ScreenHeight);
	float ditherValue = tex2D(_DitherTex, i.uv*(screenParams.xy/_DitherTexWith)).r;
	float4 c = tex2D(_MainTex, i.uv) + ditherValue*1/(_DitherTexTotal+1);
	c.rgb = tex3D(_ClutTex, parseUVW(c)).rgb;
	return c;
}

float4 fragLinear(v2f i) : SV_Target 
{ 

	float2 screenParams = float2(_ScreenWidth,_ScreenHeight);
	float ditherValue = tex2D(_DitherTex, i.uv*(screenParams.xy/_DitherTexWith)).r;
	float4 c = tex2D(_MainTex, i.uv) + ditherValue*1/(_DitherTexTotal+1);
	
	c.rgb= sqrt(c.rgb);
	c.rgb = tex3D(_ClutTex, parseUVW(c)).rgb;
	c.rgb = c.rgb*c.rgb; 
	return c;
}

ENDCG 

	
Subshader 
{
	Pass 
	{
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
	  #pragma target 3.0
      ENDCG
  	}

	Pass 
	{
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment fragLinear
	  #pragma target 3.0
      ENDCG
  	}
}

Fallback off
}
