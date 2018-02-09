// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Unlit/maskDiffuse" {
Properties {	
	_MainTex1 ("MainTex1", 2D) = "white" {}
	_MainTex2("MainTex2", 2D) = "white" {}
	_MaskTex3("MaskTex3", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex2, _MainTex1, _MaskTex3;

struct Input {
	float2 uv_MainTex1;
};

void surf (Input IN, inout SurfaceOutput o) {	
	fixed4 col1 = tex2D(_MainTex1, IN.uv_MainTex1);
	fixed4 col2 = tex2D(_MainTex2, IN.uv_MainTex1);
	fixed4 col3 = tex2D(_MaskTex3, IN.uv_MainTex1);
	fixed4 col = lerp(col1, col2, col3);
	
	o.Albedo = col.rgb;
	o.Alpha = col.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
