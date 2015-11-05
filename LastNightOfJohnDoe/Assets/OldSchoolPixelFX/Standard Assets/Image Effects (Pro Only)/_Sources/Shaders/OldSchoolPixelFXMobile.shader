Shader "Hidden/OldSchoolPixelFXMobile" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}			
	}

CGINCLUDE

#include "UnityCG.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv  : TEXCOORD0;
};

sampler2D _MainTex;
sampler2D _ClutTex;

float _ScaleY;
float _Offset; // Red offset

float2 parseUV(float4 c)
{
	float r = c.r/(_ScaleY+_Offset);
	float b = floor(c.b * _ScaleY)/_ScaleY;	
	float2 uv = float2(r+b,1-c.g);
	
	return uv;
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
	float4 c = tex2D(_MainTex, i.uv);
	c.rgb = tex2D(_ClutTex, parseUV(c)).rgb;
	return c;
}

float4 fragLinear(v2f i) : SV_Target 
{ 
	float4 c = tex2D(_MainTex, i.uv);
	c.rgb= sqrt(c.rgb);
	c.rgb = tex2D(_ClutTex, parseUV(c)).rgb;
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
	  #pragma target 2.0
      ENDCG
  	}

	Pass 
	{
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment fragLinear
	  #pragma target 2.0
      ENDCG
  	}
}

Fallback off
}
