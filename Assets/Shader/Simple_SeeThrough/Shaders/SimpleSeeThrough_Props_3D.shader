Shader "Simple SeeThrough/Props 3D"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

Category 
{
	SubShader
	{
	   //Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    Tags { "RenderType"="Opaque" }
        Pass
        {

       // Blend SrcAlpha OneMinusSrcAlpha
		Cull Off Lighting Off ZWrite On
		//AlphaTest Greater 0.1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _MainTex_ST;

			struct appdata_t
			{
				fixed4 vertex : POSITION;
				fixed2 texcoord : TEXCOORD0;
				//fixed4 color : COLOR;
			};

			struct v2f
			{
				fixed4 vertex : SV_POSITION;
				fixed2 texcoord : TEXCOORD0;
				//fixed4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				//o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				return tex;
			}
			
			ENDCG
		}
	}//FallBack "Diffuse"
}
}