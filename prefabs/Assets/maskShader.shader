Shader "Custom/MaskShader"
{
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }

            ZWrite On
            ColorMask 0
        }
    }
}
