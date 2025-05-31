Shader "UI/Gradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _ColorTopLeft ("Top Left Color", Color) = (1,1,1,1)
        _ColorTopRight ("Top Right Color", Color) = (1,1,1,1)
        _ColorBottomLeft ("Bottom Left Color", Color) = (1,1,1,1)
        _ColorBottomRight ("Bottom Right Color", Color) = (1,1,1,1)

        [HideInInspector] _Stencil ("Stencil ID", Float) = 0
        [HideInInspector] _StencilComp ("Stencil Comp", Float) = 8
        [HideInInspector] _StencilOp ("Stencil Op", Float) = 0
        [HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
        [HideInInspector] _ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Stencil
            {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }
            ColorMask [_ColorMask]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ UNITY_UI_CLIP_RECT

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR0;
                float2 localPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _ColorTopLeft;
            fixed4 _ColorTopRight;
            fixed4 _ColorBottomLeft;
            fixed4 _ColorBottomRight;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;

                // Normalized local position (0,0) bottom-left — (1,1) top-right
                o.localPos = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Bilinear interpolation between the 4 corner colors
                float2 uv = i.localPos;
                fixed4 colorTop = lerp(_ColorTopLeft, _ColorTopRight, uv.x);
                fixed4 colorBottom = lerp(_ColorBottomLeft, _ColorBottomRight, uv.x);
                fixed4 finalColor = lerp(colorBottom, colorTop, uv.y);

                fixed4 texColor = tex2D(_MainTex, i.texcoord);

                // Combine texture color with gradient and vertex color
                return texColor * finalColor * i.color;
            }
            ENDCG
        }
    }
}