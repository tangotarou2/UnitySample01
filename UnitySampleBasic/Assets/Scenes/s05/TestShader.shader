Shader "Custom/TestShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            Tags { "LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            uniform float4x4  _matView;
            //uniform float myido[3];

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            void vert(in appdata v, out v2f o)
            {
                //float4x4 moveMatrix = float4x4(1, 0, 0, myido[0],
                //    0, 1, 0, myido[1],
                //    0, 0, 1, myido[2],
                //    0, 0, 0, 1);

                //float4 t_pos = mul(moveMatrix, v.vertex);
                float4 t_pos = v.vertex;

                o.vertex = mul(mul(UNITY_MATRIX_P, mul(_matView, unity_ObjectToWorld)), t_pos);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            }


            void frag(in v2f i, out fixed4 col : SV_Target)
            {
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float3 normal = normalize(i.worldNormal);
                float NL = dot(normal, lightDir);

                float3 baseColor = tex2D(_MainTex, i.uv);
                float3 lightColor = _LightColor0;

                col = fixed4(baseColor * lightColor * max(NL, 0), 0);
            }

            ENDCG
        }
    }
}
