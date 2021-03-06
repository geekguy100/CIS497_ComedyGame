Shader "psx/vertexlit" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_bias ("Clip Bias", Range (0, 1)) = 0.75
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200
            Cull Off
            
			Pass {
			Lighting On
				CGPROGRAM

					#pragma vertex vert
					#pragma fragment frag
					#include "UnityCG.cginc"
					#pragma target 2.0
					#pragma multi_compile_fog
                    #define USING_FOG (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))

					struct v2f
					{
						fixed4 pos : POSITION;
						half4 color : COLOR0;
						half4 colorFog : COLOR1;
						float2 uv_MainTex : TEXCOORD0;
						half3 normal : TEXCOORD1;
					};

                    uniform fixed _bias;
					float4 _MainTex_ST;
					uniform half4 unity_FogStart;
					uniform half4 unity_FogEnd;

					v2f vert(appdata_full v)
					{
						v2f o;
                        UNITY_SETUP_INSTANCE_ID(v);
                        //float distance = length(mul(UNITY_MATRIX_MV,v.vertex));
                        float distance = length(UnityObjectToClipPos(v.vertex));
                        				    
                            float4 snapToPixel = UnityObjectToClipPos(v.vertex);
                            float4 vertex = snapToPixel;
                            //Vertex snapping
                            vertex.xyz = snapToPixel.xyz / snapToPixel.w;
                            vertex.x = floor(160 * vertex.x) / 160;
                            vertex.y = floor(120 * vertex.y) / 120;
                            vertex.xyz *= snapToPixel.w;
                            o.pos = vertex;
                            						   
                            //Affine Texture Mapping
                            float4 affinePos = vertex; //vertex;				
                            o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                            o.uv_MainTex *= distance + (vertex.w*(UNITY_LIGHTMODEL_AMBIENT.a * 8)) / distance / 2;
                            o.normal = distance + (vertex.w*(UNITY_LIGHTMODEL_AMBIENT.a * 8)) / distance / 2;
                        
						//Vertex lighting 
					//	o.color =  float4(ShadeVertexLights(v.vertex, v.normal), 1.0);
					o.color = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true), 1.0);
						o.color *= v.color;

						//Fog
						/*
						float4 fogColor = unity_FogColor;

						float fogDensity = (unity_FogEnd - distance) / (unity_FogEnd - unity_FogStart);
						o.normal.g = fogDensity;
						o.normal.b = 1;

						o.colorFog = fogColor;
						o.colorFog.a = clamp(fogDensity,0,1);
                        
                        */
                        float3 eyePos = UnityObjectToViewPos(v.vertex);
                        float fogCoord = length(eyePos.xyz);
                        UNITY_CALC_FOG_FACTOR_RAW(fogCoord);
                        o.colorFog = saturate(unityFogFactor);
                        
						//Cut out polygons
						if (distance > unity_FogStart.z + unity_FogColor.a * 255)
						{
							o.pos.w = 0;
						}
						return o;
					}

					sampler2D _MainTex;

					fixed4 frag(v2f IN) : COLOR
					{

						fixed4 tex = tex2D(_MainTex, IN.uv_MainTex / IN.normal.r)*IN.color;


                        clip( tex.a - _bias );
                        
                        fixed4 col = tex;
                        col.a = 1.0f;
                        #if USING_FOG
                            col.rgb = lerp(unity_FogColor.rgb, col.rgb, IN.colorFog);
						#endif
						
		                //half4 color = c*(IN.colorFog.a);
		                //color.rgb += IN.colorFog.rgb*(1 - IN.colorFog.a);
		                return col;
					}
				ENDCG
			}
	}
}