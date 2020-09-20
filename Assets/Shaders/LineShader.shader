Shader "LinesTool/LR_ND_NUV"
{
  Properties
  {
    [Toggle] _Zthickness("Z Thickness", Float) = 0
  }
  SubShader
  {
    Tags { "RenderType"="Opaque" }
    LOD 100
    ZWrite off ZTest Always  Cull off

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
        float4 color : COLOR;
      };
      
      float _Zthickness;

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex.xyz = UnityObjectToViewPos(v.vertex);
        float3 n1 = UnityObjectToViewPos(float4(v.vertex.xyz + v.tangent.xyz, 0));
        n1 = normalize(cross(n1 - o.vertex.xyz, o.vertex.xyz));
        float c = (_Zthickness == 0 ? o.vertex.xyz.z : 1.0f) / _ScreenParams.y;
        float3 v1 = n1 * v.tangent.w * c;
        float3 v2 = float3(v.tangent.xy, 0) * c;
        o.vertex.xyz = o.vertex.xyz + (v.tangent.w != 0 ? v1 : v2);
        o.vertex.w = v.vertex.w;
        o.vertex = mul(UNITY_MATRIX_P, o.vertex);
        o.color = v.color;
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target
      {
        fixed4 col = i.color;
        return col;
      }
      ENDCG
    }
  }
}