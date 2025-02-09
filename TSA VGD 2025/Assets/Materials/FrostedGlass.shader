Shader "Unlit/FrostedGlass" {
    Properties {
        // Controls how strong the blur is.
        _Radius("Blur Radius", Range(0.1, 10)) = 2.0
    }
    SubShader {
        // Ensure the shader is drawn after opaque geometry.
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        // GrabPass: capture what’s already rendered behind this object.
        GrabPass {
            Tags { "LightMode"="Always" }
        }

        Pass {
            Tags { "LightMode"="Always" }
            CGPROGRAM
            // Use vertex and fragment programs.
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            // Input structure for vertices.
            struct appdata_t {
                float4 vertex : POSITION;
            };

            // Interpolated data to the fragment shader.
            struct v2f {
                float4 vertex   : SV_POSITION;
                float4 screenPos: TEXCOORD0;  // Homogeneous screen position used by tex2Dproj
            };

            // Blur radius value.
            float _Radius;

            // Vertex shader: transforms vertices and computes screen position.
            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Compute the screen position for use with the GrabPass texture.
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            // GrabPass texture and its texel size.
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
// Fragment shader: performs a simple blur by sampling several offsets.
            half4 frag(v2f i) : SV_Target {
                half4 sum = half4(0, 0, 0, 0);
                int count = 0;

                // Macro to sample the grabbed texture.
                // tex2Dproj automatically divides by the w component.
                #define GRAB_SAMPLE(offsetX, offsetY) tex2Dproj(_GrabTexture, float4(i.screenPos.xy + _GrabTexture_TexelSize.xy * float2(offsetX, offsetY), i.screenPos.z, i.screenPos.w))

                // Center sample.
                sum += GRAB_SAMPLE(0, 0);
                count++;

                // Loop: for each step (incrementing by 0.1), sample 4 diagonally offset texels.
                for (float r = 0.1; r <= _Radius; r += 0.1) {
                    sum += GRAB_SAMPLE(r, r);
                    sum += GRAB_SAMPLE(r, -r);
                    sum += GRAB_SAMPLE(-r, r);
                    sum += GRAB_SAMPLE(-r, -r);
                    count += 4;
                }
                return sum / count;
            }
            ENDCG
        }
    }
    // Fallback to Diffuse in case the shader cannot be used.
    FallBack "Diffuse"
}
