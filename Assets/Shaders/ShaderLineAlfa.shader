Shader "LinesTool/LR_ND_T"
{
  Properties
  {
    _MainTex("Texture", 2D) = "white" {}
    [Toggle] _Zthickness("Z Thickness", Float) = 0
  }
  SubShader
  {
    Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
    LOD 100
    ZWrite off
    ZTest Always
    Cull off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        float4 tangent : TANGENT;
        float4 color : COLOR;
      };

      struct v2f
      {
        float4 vertex : SV_POSITION;
        float3 uvx : TEXCOORD0;
        float3 uvy : TEXCOORD1;
        float4 color : COLOR;
      };

      sampler2D _MainTex;
      float4 _MainTex_ST;
      float _Zthickness;
      
      v2f vert (appdata v)
      {
        v2f o;
        o.vertex.xyz = UnityObjectToViewPos(v.vertex);
        float3 n1 = UnityObjectToViewPos(float4(v.vertex.xyz + v.tangent.xyz, 0));
        float3 uvx1 = normalize(n1 - o.vertex.xyz);
        float3 uvx2 = float3(v.tangent.x > 0 ? v.tangent.x / v.tangent.x : 0, 0, 0);
        n1 = normalize(cross(uvx1, o.vertex.xyz));
        float3 uvy1 = v.tangent.w < 0 ? -n1 : n1;
        float3 uvy2 = v.tangent.z != 0 ? float3(v.tangent.y > 0 ? v.tangent.y : 0, 0, 0) : float3(v.tangent.xy, 0);
        uvx2.x = v.tangent.z != 0 ? uvx2.x : 1;
        float l = length(uvy2.xy);
        uvy2.xy = l > 0 ? uvy2.xy / l : uvy2.xy;
        float c = (_Zthickness == 0 ? o.vertex.xyz.z : 1.0f) / _ScreenParams.y;
        float3 v1 = n1 * v.tangent.w * c;
        float3 v2 = float3(v.tangent.xy, 0) * c;
        o.uvx = (v.tangent.w != 0 ? uvx1 : uvx2);
        o.uvy = (v.tangent.w != 0 ? uvy1 : uvy2);
        o.vertex.xyz = o.vertex.xyz + (v.tangent.w != 0 ? v1 : v2);
        o.vertex.w = v.vertex.w;
        o.vertex = mul(UNITY_MATRIX_P, o.vertex);
        o.color = v.color;
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target
      {
        fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(float2(length(i.uvx),length(i.uvy)), _MainTex)) * i.color;
        return col;
      }
      ENDCG
    }
  }
}