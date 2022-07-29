Shader "Unlit/VertexColorShader"
{
    SubShader
    {
      Pass
      {
        CGPROGRAM
        #pragma vertex vert  
        #pragma fragment frag  

        #include "UnityCG.cginc"  
        // ���_�V�F�[�_�[�ɑ�����f�[�^�̌^  
        struct appdata
        {
          float4 vertex : POSITION;
          fixed3 color : COLOR0;
        };

    // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ɑ���f�[�^�̌^  
    struct v2f
    {
      float4 vertex : SV_POSITION;
      fixed3 color : COLOR0;
    };

      v2f vert(appdata v)
      {
          v2f o;
          o.vertex = UnityObjectToClipPos(v.vertex);
          o.color = v.color;
          return o;
      }

    fixed4 frag(v2f i) : SV_Target
    {
      return fixed4(i.color, 1);
    }
    ENDCG
  }
    }
}