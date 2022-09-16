Shader "Background/Starfield Shader"
{
    Properties
    {
        _Layers ("Layer Quantity", Integer) = 7
        _Scale ("Scale", Float) = 4.25
        _StarScale ("Star Scale", Float) = 1.0
        _StarColor ("Star Color", Color) = (0.,0.425,1.,1.)
        _StarColorVariation ("Star Color Variation", Range(0.0, 1.0)) = 0.05
        _DepthPower ("Depth Power", Range(0.0, 2.0)) = 0.45
        _StarFrequency ("Star Frequency", Range(0.0, 1.0)) = 0.6
        _StarGlow ("Star Glow", Range(0.0, 1.0)) = 0.025
        _StarDamp ("Star Damp", Range(0.0, 1.)) = 0.95
        _StarFlare ("Star Flare", Range(0.0, 1.0)) = 0.75
        _StarFlareScale ("Star Flare Scale", Range(0.0, 1.)) = 0.7
        _Speed ("Speed", Vector) = (1.5, 0., 0., 0.)

        _CloudColor ("Cloud Color", Color) = (0.,0.425,1.,0.89)
        _CloudNoiseOctave ("Cloud Noise Octave", Integer) = 7
        _CloudScale ("Cloud Scale", Float) = 20.
        _CloudColorRamp ("Cloud Color Ramp", 2D) = ""
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            Name "Starfield Shader"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "FastNoiseLite.hlsl"

            int _Layers;
            float _Scale;
            float _StarScale;
            float4 _StarColor;
            float _StarColorVariation;
            float _DepthPower;
            float _StarGlow;
            float _StarFlare;
            float _StarFrequency;
            float _StarDamp;
            float _StarFlareScale;
            float2 _Speed;

            float4 _CloudColor;
            int _CloudNoiseOctave;
            float _CloudNoiseFrequency;
            float _CloudScale;

            sampler2D _CloudColorRamp;
            float4 _CloudColorRamp_ST;
            
            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float2x2 Rot(float a)
            {
                float s = sin(a), c = cos(a);
                return float2x2(c, -s, s, c);
            }

            float Star(float2 uv, float flare)
            {
                float d = length(uv) / _StarScale;
                float m = (1. - _StarDamp) * _StarFlareScale / d;

                float rays = max(0., 1. - abs(uv.x * uv.y * 1000.));
                m += rays * flare;
                rays = max(0., 1. - abs(uv.x * uv.y * 1000.));
                m += rays * .3 * flare;

                m *= smoothstep(_StarGlow, .0, d * (1. - _StarFlareScale));
                return m;
            }

            float Hash11(float p)
            {
                p = frac(p * .1031);
                p *= p + 33.33;
                p *= p + p;
                return frac(p);
            }

            float Hash12(float2 p)
            {
                float3 p3 = frac(float3(p.xyx) * .1031);
                p3 += dot(p3, p3.yzx + 33.33);
                return frac((p3.x + p3.y) * p3.z);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            float3 StarLayer(float2 uv, float layer)
            {
                float3 col = float3(0, 0, 0);

                float2 gv = frac(uv) - .5;
                float2 id = floor(uv);

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 offset = float2(x, y);

                        float n = Hash12(id + offset) * Hash11(layer);

                        if(n > _StarFrequency)
                            n = 0;
                        
                        float size = frac(n * 345.32);

                        float star = Star(gv - offset - float2(n, frac(n * 72.14)) + .5,
                                          smoothstep(1. - _StarFlare, 1., size) * .5) * n;

                        float3 color = sin(
                            _StarColor + _StarColorVariation * 5. * float3(
                                frac(n * 38.95), frac(n * 16.18), frac(n * 27.98))) * .5 + .5;

                        star *= sin(_Time.x * .3 + n * 6.2874) * .5 + 1;
                        col += star * size * color;
                    }
                }

                return col;
            }

            float3 CloudLayer(float2 uv)
            {
                fnl_state noise = fnlCreateState();
                noise.noise_type = FNL_NOISE_OPENSIMPLEX2;
                noise.fractal_type = FNL_FRACTAL_FBM;
                noise.octaves = _CloudNoiseOctave;

                float noiseValue1 = fnlGetNoise2D(noise, uv.x * _CloudScale, uv.y * _CloudScale);
                float noiseValue2 = fnlGetNoise2D(noise, -uv.x * _CloudScale, -uv.y * _CloudScale);
                float4 sampledPixel = tex2D(_CloudColorRamp, float2(noiseValue1, noiseValue2));
                return sampledPixel.rgb * sampledPixel.a * _CloudColor.rgb * _CloudColor.a;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = (input.positionHCS - .5 * _ScreenParams.xy) / _ScreenParams.y;
                uv *= _Scale;

                float3 col = float3(0, 0, 0);

                for (int n = _Layers; n >= 1; n--)
                {
                    float depth = pow(n, _DepthPower);
                    float2 layerUv = uv * depth + n;
                    col += StarLayer(layerUv + float2(_Time.x / depth * _Speed.x + n, _Time.x / depth * _Speed.y + n), n) / depth;
                }

                col += CloudLayer(uv + float2(_Time.x / _Layers * _Speed.x, _Time.x / _Layers * _Speed.y));

                return float4(col, 1.0);
            }
            ENDHLSL
        }
    }
}