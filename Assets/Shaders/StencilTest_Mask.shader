Shader "StencilTest_Target"
{
    Properties
    {

    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry-1" // �ݵ�� ��󺸴� ���� �׷����� �ϹǷ�
        }

        Stencil
        {
            Ref 1
            Comp Never   // �׻� ������ ���� ����
            Fail Replace // ������ ������ �κ��� ���ٽ� ���ۿ� 1�� ä��
        }

        CGPROGRAM
        #pragma surface surf nolight noforwardadd nolightmap noambient novertexlights noshadow

        struct Input { float4 color:COLOR; };

        void surf(Input IN, inout SurfaceOutput o) {}
        float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten)
        {
            return float4(0, 0, 0, 0);
        }
        ENDCG
    }
        FallBack ""

        
}
