Shader "EPotentialShader" {

    Properties{
        Maxz_("Maxz", float) = 1
        Minz_("Minz", float) = 0
    }

        SubShader{
            Pass {
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                float Maxz_;
                float Minz_;

                struct v2f {
                    float4 pos : POSITION;
                    fixed4 color : COLOR;
                };

                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.color = v.color;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target { return i.color; }
                ENDCG
            }
    }
}
