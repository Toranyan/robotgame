Shader "Custom/Atmosphere" {
    Properties {
    
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0

		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    }
    
    SubShader {
    
    	Blend SrcAlpha OneMinusSrcAlpha
    	Cull Off
    
		Tags { "Queue"= "Transparent" "RenderType" = "Transparent" }
		CGPROGRAM
		//#pragma vertex vert
		#pragma surface surf Rim alpha
		
		
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
			//float3 worldRefl; INTERNAL_DATA
			float3 worldNormal; INTERNAL_DATA 
			float4 pos : SV_POSITION;
			float3 normal;
			
		};
		
		struct SurfaceOutputCustom {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
			half3 Custom;
		};
		
		

		//properties
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimPower;
		//float4 _SpecColor;
		float _Shininess;
		
		
		
		//void vert(inout appdata_full v, out Input o) {
		//	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		//	o.normal = v.normal;
		//}
		
		half4 LightingRim (SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten) {

			half3 h = normalize (lightDir + viewDir); //
			half diff = max (0, dot (s.Normal, lightDir)); //diffuse lighting
			//half diff = dot (s.Normal, lightDir); //diffuse lighting
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, 48.0);
			//float spec = pow (nh, 1);
			half rim = ((1 - (dot (normalize(viewDir), (s.Normal)))) + ((dot (normalize(lightDir), (s.Normal)))));
			//half rim = ((1 - (dot (normalize(viewDir), (s.Custom)))) + ((dot (normalize(lightDir), (s.Custom)))));
			//half rim = ((1 - (dot (normalize(viewDir), s.Custom))) ) ;
			//half rim = 1.0 - saturate(dot (normalize(viewDir), s.Normal));
			half3 tint = (pow (rim, _RimPower) * _RimColor.rgb);
			
			//s.Emission = _RimColor.rgb * pow (rim, _RimPower);
			//s.Emission = 0;
			
			half4 c;
			//c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * s.Alpha * _Shininess *_SpecColor) * (atten * 2);
			//c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
			c.rgb = (s.Albedo * _LightColor0.rgb * diff) * (atten * 2); //diffuse
			//c.rgb = s.Albedo;
			
			c.a = s.Alpha + tint;
			
			c.rgb = (c.rgb * 0.5 + tint) ;
			//c.rgb = half3(0,1,0);
			
			//c.rgb = c.rgb + (pow (rim, _RimPower) * _RimColor.rgb);
			//c.rgb = _RimColor.rgb;
			//c.rgb = float3(1,0,0);
			
			
			//c.a = 0.5;
			
			//s.Normal = (1,1,1);
			
			return c;
		}
		

		void surf (Input IN, inout SurfaceOutputCustom o) {
			float4 tex = tex2D (_MainTex, IN.uv_MainTex);
		
			o.Albedo = tex.rgb;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Specular = _Shininess;
			o.Gloss = tex2D (_MainTex, IN.uv_MainTex).a;
			
			half rim = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));
						
			//o.Albedo = 
			//o.Emission = _RimColor.rgb * pow (rim, _RimPower);
			
			o.Custom = IN.worldNormal;
			
			//o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a;
			o.Alpha = tex.a;
			
			//o.Custom = normalize(IN.worldNormal);
			//o.Custom = 
			
			//o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			
			//o.Alpha = 0;
		}
		
		
		
		

		


		ENDCG
    } 
    Fallback "Diffuse"
  }