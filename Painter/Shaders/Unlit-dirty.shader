Shader "Unlit/dirty"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_DirtyTex("DirtyTex", 2D) = "white" {}
		//_MaskTex("MaskTex", 2D) = "white" {}
		_Darker ("Darker", Range(0, 1)) = 1
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0) 
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 100
		Cull Off
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha 

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				
				float3 normalDir : TEXCOORD2;
				float4 posWorld : TEXCOORD3;
			};

			sampler2D _DirtyTex, _MainTex;
			float4 _MainTex_ST,_DirtyTex_ST;
			fixed _Darker;
			float4 _RimColor; 
			float _RimPower;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.uv2, _DirtyTex);
				
				o.normalDir = UnityObjectToWorldNormal(v.normal);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 Rim = (pow(1.0-max(0,dot(i.normalDir, viewDirection)),_RimPower)*_RimColor);
			
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv.xy);
				fixed4 col1 = tex2D(_DirtyTex, i.uv.zw);
				//fixed4 col2 = tex2D(_MaskTex, i.uv.zw);
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				
				col.rgb *= col1.rgb;
				col.rgb += Rim;
				col.rgb *=_Darker;
				return col;
			}
			ENDCG
		}
	}
}
