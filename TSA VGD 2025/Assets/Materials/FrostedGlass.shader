Shader "Unlit/FrostedGlass" {
    Properties {
        _Radius("Blur Radius", Range(0.1, 10)) = 2.0
        _ScreenTexture("Screen Texture", 2D) = "white" {} // Render Texture assigned here.
    }
    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        // Remove GrabPass block – we are using _ScreenTexture.
        Pass {
            Tags { "LightMode"="Always" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex   : SV_POSITION;
                float4 screenPos: TEXCOORD0;  // Homogeneous screen position for texture sampling.
            };

            float _Radius;

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _ScreenTexture;
            float4 _ScreenTexture_TexelSize; // Contains texel size info for proper sampling.

            half4 frag(v2f i) : SV_Target {
                half4 sum = half4(0, 0, 0, 0);
                int count = 0;

                // Macro for sampling the screen texture.
                #define SCREEN_SAMPLE(offsetX, offsetY) tex2D(_ScreenTexture, i.screenPos.xy + _ScreenTexture_TexelSize.xy * float2(offsetX, offsetY))

                // Center sample.
                sum += SCREEN_SAMPLE(0, 0);
                count++;

                // Loop to sample diagonally offset texels for the blur.
                for (float r = 0.1; r <= _Radius; r += 0.1) {
                    sum += SCREEN_SAMPLE(r, r);
                    sum += SCREEN_SAMPLE(r, -r);
                    sum += SCREEN_SAMPLE(-r, r);
                    sum += SCREEN_SAMPLE(-r, -r);
                    count += 4;
                }
                return sum / count;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}