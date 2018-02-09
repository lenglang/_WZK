Shader "Unlit/mask"
{
	Properties
	{
		_MainTex1 ("MainTex1", 2D) = "white" {}
		_MainTex2("MainTex2", 2D) = "white" {}
		_MaskTex3("MaskTex3", 2D) = "white" {}
		//_Darker ("Darker", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex2, _MainTex1, _MaskTex3;
			float4 _MainTex1_ST;
			//fixed _Darker;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col1 = tex2D(_MainTex1, i.uv);
				fixed4 col2 = tex2D(_MainTex2, i.uv);
				fixed4 col3 = tex2D(_MaskTex3, i.uv);
				fixed4 col = lerp(col1, col2, col3);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
				//return col*_Darker;
			}
			ENDCG
		}
	}
}
