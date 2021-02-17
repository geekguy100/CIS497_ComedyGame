// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FX/MirrorReflection"
{
	Properties
	{
	    _Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		[HideInInspector] _ReflectionTex ("", 2D) = "white" {}
		_RefStrength ("Reflection Strength", Range (0, 1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
 
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 refl : TEXCOORD1;
				float4 pos : SV_POSITION;
			};
			float4 _MainTex_ST;
			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (pos);
				o.uv = TRANSFORM_TEX(uv, _MainTex);
				o.refl = ComputeScreenPos (o.pos);
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _ReflectionTex;
			float _RefStrength;
			fixed4 _Color;
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.uv) * _Color;
				fixed4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.refl));
				return tex + (refl * _RefStrength);
			}
			ENDCG
	    }
	}
}
