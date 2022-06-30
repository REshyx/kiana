Shader "Unlit/Unlit Specular Shader"
{
    Properties
    {
        _DiffuseCol("Diffuse",COLOR) = (1,1,1,1)
    _SpecularCol("Specular" , COLOR) = (1,1,1,1)
    _Gloss("Gloss",Range(8.0,256)) = 20
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
        float3 worldPos : TEXCOORD1;
            };


        fixed4 _DiffuseCol;
        fixed4 _SpecularCol;
        fixed _Gloss;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
        o.worldNormal = mul(v.normal, unity_WorldToObject);
        o.worldPos = mul(unity_ObjectToWorld , v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
        fixed3 worldPos = (i.worldPos); //注意 不要normalize,会出错
        float3 worldNormal = normalize(i.worldNormal); //world normal dir
        fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);  // world light dir
        fixed3 worldViewDir = normalize(_WorldSpaceCameraPos.xyz - worldPos); //world view dir 

        float3 halfDir = normalize(worldLightDir + worldViewDir);

        float NdotH = max(0 , dot(halfDir , worldNormal));
        float NdotL = dot(worldNormal , worldLightDir); //NdotL : 法线方向点乘光照方向 

        fixed3 diffuse = saturate(NdotL) * _DiffuseCol.rgb * _LightColor0.rgb;
        fixed3 specular = pow((NdotH) , _Gloss) * _SpecularCol.rgb;
        fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;


        fixed3 ads = diffuse + specular + ambient;
        fixed4 col = fixed4(ads  , 1);

        return col;
            }
            ENDCG
        }
    }
}