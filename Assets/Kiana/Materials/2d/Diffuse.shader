Shader "Unlit/Unlit Diffuse Shader"
{
    Properties
    {
        _DiffuseCol("Diffuse",COLOR) = (1,1,1,1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
        float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
        float3 worldNormal : TEXCOORD0;
            };


        fixed4 _DiffuseCol;
        fixed _Step;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
        o.worldNormal = mul(v.normal, unity_WorldToObject);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
        float3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
        float3 worldNormal = normalize(i.worldNormal); //world normal dir
        fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);  // world light dir

        float NdotL = dot(worldNormal , worldLightDir); //NdotL : 法线方向点乘光照方向 

        fixed3 diffuse = saturate(NdotL) * _DiffuseCol.rgb * _LightColor0.rgb;
        fixed3 ad = diffuse + ambient;
        fixed4 col = fixed4(diffuse  , 1);

        return col;
            }
            ENDCG
        }
    }
}