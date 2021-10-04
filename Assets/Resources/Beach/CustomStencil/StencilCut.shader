﻿Shader "Custom/StencilCut"
{
    SubShader{
        Tags { "RenderType" = "Transparent" "Queue" = "Geometry+1"}
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            Stencil {
                Ref 2
                Comp equal
                Pass decrWrap
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(0,0,0,0);
            }
            ENDCG
        }
    }
}
