Shader "Custom/LightweightAA"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Sharpness ("Sharpness", Range(0.1, 1.0)) = 0.8
        _TemporalWeight ("Temporal Weight", Range(0.0, 1.0)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Sharpness;
            float _TemporalWeight;
            sampler2D _PrevFrame;
            float4x4 _PrevViewProjection;
            
            // Replace UnityObjectToClipPos
            float4x4 unity_ObjectToWorld;
            float4x4 unity_MatrixVP;
            
            float4 ObjectToClipPos(float4 pos)
            {
                float4 worldPos = mul(unity_ObjectToWorld, pos);
                return mul(unity_MatrixVP, worldPos);
            }
            
            // Replace TRANSFORM_TEX
            float2 TransformTex(float2 tex, float4 st)
            {
                return tex * st.xy + st.zw;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = ObjectToClipPos(v.vertex);
                o.uv = TransformTex(v.uv, _MainTex_ST);
                return o;
            }
            
            float2 GetMotionVector(float2 uv)
            {
                // Simplified motion vector calculation
                float2 velocity = float2(0, 0);
                
                // Basic motion estimation based on screen position
                float2 screenPos = uv * 2.0 - 1.0;
                float2 prevScreenPos = mul(_PrevViewProjection, float4(screenPos, 0, 1)).xy;
                velocity = (screenPos - prevScreenPos) * 0.5;
                
                return velocity;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                // Sample current frame
                float4 currentColor = tex2D(_MainTex, i.uv);
                
                // Sample previous frame with motion vectors
                float2 motion = GetMotionVector(i.uv);
                float4 previousColor = tex2D(_PrevFrame, i.uv + motion);
                
                // Adaptive sharpening
                float3 sharp = currentColor.rgb;
                float3 neighborAvg = 0;
                float2 texelSize = 1.0 / float2(2048, 2048); // Adjust based on your texture size
                
                // Simple 3x3 sharpening
                for(int x = -1; x <= 1; x++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        float2 offset = float2(x, y) * texelSize;
                        neighborAvg += tex2D(_MainTex, i.uv + offset).rgb;
                    }
                }
                neighborAvg /= 9.0;
                
                sharp = lerp(neighborAvg, currentColor.rgb, _Sharpness);
                
                // Temporal blend with reduced blur
                float blendFactor = _TemporalWeight * (1 - length(motion) * 2);
                float3 final = lerp(sharp, previousColor.rgb, blendFactor);
                
                return float4(final, currentColor.a);
            }
            
            ENDCG
        }
    }
}