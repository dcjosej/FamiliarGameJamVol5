Shader "Hidden/OldSchoolPixelFX" {
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
sampler3D _ClutTex;

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
	float4 c = tex2D(_MainTex, i.uv);
	c.rgb = tex3D(_ClutTex, parseUVW(c)).rgb;
	return c;
}

float4 fragLinear(v2f i) : SV_Target 
{ 
	float4 c = tex2D(_MainTex, i.uv);
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