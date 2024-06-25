// Made with Amplify Shader Editor v1.9.4.4
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UI Particles"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
         [Header(Emitter Settings)]_EmitterDimensions("Emitter Dimensions", Vector) = (1,1,0,0)
         [Header(Particle Settings)]_MinStartSize("Min Start Size", Float) = 0.1
         _MaxStartSize("Max Start Size", Float) = 1
         _ParticleLifetime("Particle  Lifetime", Float) = 1
         _StartSize("Start Size", Float) = 0.1
         _EndSize("End Size", Float) = 1
         _FlipbookCells("Flipbook Cells", Vector) = (1,1,0,0)
         _Float0("Float 0", Float) = 0


        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        
        [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil ("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector]_StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector]_StencilReadMask ("Stencil Read Mask", Float) = 255

        [HideInInspector]
        _ColorMask ("Color Mask", Float) = 15

        [HideInInspector]
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

       
    }

    SubShader
    {
		LOD 0

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

        Stencil
        {
        	Ref [_Stencil]
        	ReadMask [_StencilReadMask]
        	WriteMask [_StencilWriteMask]
        	Comp [_StencilComp]
        	Pass [_StencilOp]
        }


        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        
        Pass
        {
            Name "Default"
        CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "UnityShaderVariables.cginc"
            #define ASE_NEEDS_VERT_COLOR
            #define ASE_NEEDS_VERT_POSITION


            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            uniform float _ParticleLifetime;
            uniform float2 _EmitterDimensions;
            uniform float _MinStartSize;
            uniform float _MaxStartSize;
            uniform float _EndSize;
            uniform float _StartSize;
            uniform float2 _FlipbookCells;
            uniform float _Float0;
            float3 HashVector33_g12( float3 p )
            {
            	uint3 v = (uint3) (int3) round(p);
            	    v.x ^= 1103515245U;
            	    v.y ^= v.x + v.z;
            	    v.y = v.y * 134775813;
            	    v.z += v.x ^ v.y;
            	    v.y += v.x ^ v.z;
            	    v.x += v.y * v.z;
            	    v.x = v.x * 0x27d4eb2du;
            	    v.z ^= v.x << 3;
            	    v.y += v.z << 3; 
            	return v * (1.0 / float(0xffffffff));
            }
            

            
            v2f vert(appdata_t v )
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                float temp_output_116_0 = ( ( _ParticleLifetime * _Time.y ) + v.color.r );
                float3 temp_cast_0 = (( ( v.color.r * 255.0 ) + temp_output_116_0 )).xxx;
                float3 p3_g12 = temp_cast_0;
                float3 localHashVector33_g12 = HashVector33_g12( p3_g12 );
                float3 Hash_3_Quad97 = localHashVector33_g12;
                float2 appendResult43 = (float2(Hash_3_Quad97.xy));
                float dotResult4_g1 = dot( Hash_3_Quad97.xy , float2( 12.9898,78.233 ) );
                float lerpResult10_g1 = lerp( _MinStartSize , _MaxStartSize , frac( ( sin( dotResult4_g1 ) * 43758.55 ) ));
                float lerpResult122 = lerp( _EndSize , _StartSize , frac( temp_output_116_0 ));
                

                v.vertex.xyz += ( float3( ( ( ( appendResult43 * float2( 2,2 ) ) + float2( -1,-1 ) ) * _EmitterDimensions ) ,  0.0 ) + ( ( v.vertex.xyz * ( lerpResult10_g1 + -1.0 ) ) * lerpResult122 ) );

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                OUT.texcoord = v.texcoord;
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN ) : SV_Target
            {
                //Round up the alpha color coming from the interpolator (to 1.0/256.0 steps)
                //The incoming alpha could have numerical instability, which makes it very sensible to
                //HDR color transparency blend, when it blends with the world's texture.
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0/alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision)*invAlphaPrecision;

                float temp_output_4_0_g1 = _FlipbookCells.x;
                float temp_output_5_0_g1 = _FlipbookCells.y;
                // *** BEGIN Flipbook UV Animation vars ***
                // Total tiles of Flipbook Texture
                float fbtotaltiles246_g1 = min( temp_output_4_0_g1 * temp_output_5_0_g1, ( ( temp_output_4_0_g1 * temp_output_5_0_g1 ) - 0.0 ) + 1 );
                // Offsets for cols and rows of Flipbook Texture
                float fbcolsoffset246_g1 = 1.0f / temp_output_4_0_g1;
                float fbrowsoffset246_g1 = 1.0f / temp_output_5_0_g1;
                // Speed of animation
                float fbspeed246_g1 = _Time.y * ( ( _FlipbookCells.x * _FlipbookCells.y ) * _ParticleLifetime );
                // UV Tiling (col and row offset)
                float2 fbtiling246_g1 = float2(fbcolsoffset246_g1, fbrowsoffset246_g1);
                // UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
                // Calculate current tile linear index
                float fbcurrenttileindex246_g1 = floor( fmod( fbspeed246_g1 + _Float0, fbtotaltiles246_g1) );
                fbcurrenttileindex246_g1 += ( fbcurrenttileindex246_g1 < 0) ? fbtotaltiles246_g1 : 0;
                // Obtain Offset X coordinate from current tile linear index
                float fblinearindextox246_g1 = round ( fmod ( fbcurrenttileindex246_g1, temp_output_4_0_g1 ) );
                // Multiply Offset X by coloffset
                float fboffsetx246_g1 = fblinearindextox246_g1 * fbcolsoffset246_g1;
                // Obtain Offset Y coordinate from current tile linear index
                float fblinearindextoy246_g1 = round( fmod( ( fbcurrenttileindex246_g1 - fblinearindextox246_g1 ) / temp_output_4_0_g1, temp_output_5_0_g1 ) );
                // Reverse Y to get tiles from Top to Bottom
                fblinearindextoy246_g1 = (int)(temp_output_5_0_g1-1) - fblinearindextoy246_g1;
                // Multiply Offset Y by rowoffset
                float fboffsety246_g1 = fblinearindextoy246_g1 * fbrowsoffset246_g1;
                // UV Offset
                float2 fboffset246_g1 = float2(fboffsetx246_g1, fboffsety246_g1);
                // Flipbook UV
                half2 fbuv246_g1 = IN.texcoord.xy * fbtiling246_g1 + fboffset246_g1;
                // *** END Flipbook UV Animation vars ***
                int flipbookFrame246_g1 = ( ( int )fbcurrenttileindex246_g1);
                

                half4 color = ( _Color * tex2D( _MainTex, fbuv246_g1 ) );

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
        ENDCG
        }
    }
    CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19404
Node;AmplifyShaderEditor.CommentaryNode;119;-1584,416;Inherit;False;740;314.8;Particle Lifetime;5;116;117;115;113;121;;0,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;112;-1584,128;Inherit;False;862;249.75;Particle Random Values;5;36;97;31;5;120;;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-1536,464;Inherit;False;Property;_ParticleLifetime;Particle  Lifetime;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;121;-1536,544;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;5;-1568,160;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-1312,496;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1392,192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;255;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;116;-1120,496;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-1248,192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;36;-1136,192;Inherit;False;HashVector3;-1;;12;e08360d15330cbd429407eddabb4c325;0;1;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;100;-1584,752;Inherit;False;803.6621;478.1796;Particle Start Size;7;80;83;84;90;91;98;92;;0,0,0,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-960,192;Inherit;False;Hash 3 Quad;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;101;-672,464;Inherit;False;692;306.85;Bounding Box Size;5;43;44;45;47;46;;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-1520,1088;Inherit;False;Property;_MaxStartSize;Max Start Size;2;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;98;-1536,880;Inherit;False;97;Hash 3 Quad;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;91;-1520,992;Inherit;False;Property;_MinStartSize;Min Start Size;1;1;[Header];Create;True;1;Particle Settings;0;0;False;0;False;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-608,512;Inherit;False;FLOAT2;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;90;-1296,960;Inherit;False;Random Range;-1;;1;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;128;-864,-80;Inherit;False;Property;_FlipbookCells;Flipbook Cells;7;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-448,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;-1072,960;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;83;-1296,800;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;125;-768,928;Inherit;False;Property;_EndSize;End Size;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;124;-768,1008;Inherit;False;Property;_StartSize;Start Size;4;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;117;-992,496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-672,32;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-304,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;-1,-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;46;-432,608;Inherit;False;Property;_EmitterDimensions;Emitter Dimensions;0;1;[Header];Create;True;1;Emitter Settings;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-928,816;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;122;-576,928;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;107;-528,-112;Inherit;False;0;0;_MainTex;Shader;True;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;129;-512,64;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;131;-416,-48;Inherit;False;Property;_Float0;Float 0;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;76;-208,-336;Inherit;False;0;0;_Color;Shader;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-144,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-320,896;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;127;-256,-112;Inherit;False;Flipbook;-1;;1;53c2488c220f6564ca6c90721ee16673;3,68,0,217,0,244,0;11;51;SAMPLER2D;0.0;False;167;SAMPLERSTATE;0;False;13;FLOAT2;0,0;False;24;FLOAT;0;False;210;FLOAT;4;False;4;FLOAT;4;False;5;FLOAT;4;False;130;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;INT;218
Node;AmplifyShaderEditor.RangedFloatNode;126;-768,1120;Inherit;False;Property;_DebugTime;Debug Time;6;0;Create;True;0;0;0;False;0;False;0.12;0.12;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;192,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;208,-240;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;368,144;Float;False;True;-1;2;ASEMaterialInspector;0;3;UI Particles;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;True;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;115;0;113;0
WireConnection;115;1;121;0
WireConnection;31;0;5;1
WireConnection;116;0;115;0
WireConnection;116;1;5;1
WireConnection;120;0;31;0
WireConnection;120;1;116;0
WireConnection;36;1;120;0
WireConnection;97;0;36;0
WireConnection;43;0;97;0
WireConnection;90;1;98;0
WireConnection;90;2;91;0
WireConnection;90;3;92;0
WireConnection;44;0;43;0
WireConnection;84;0;90;0
WireConnection;117;0;116;0
WireConnection;130;0;128;1
WireConnection;130;1;128;2
WireConnection;45;0;44;0
WireConnection;80;0;83;0
WireConnection;80;1;84;0
WireConnection;122;0;125;0
WireConnection;122;1;124;0
WireConnection;122;2;117;0
WireConnection;129;0;130;0
WireConnection;129;1;113;0
WireConnection;47;0;45;0
WireConnection;47;1;46;0
WireConnection;123;0;80;0
WireConnection;123;1;122;0
WireConnection;127;51;107;0
WireConnection;127;24;131;0
WireConnection;127;4;128;1
WireConnection;127;5;128;2
WireConnection;127;130;129;0
WireConnection;82;0;47;0
WireConnection;82;1;123;0
WireConnection;109;0;76;0
WireConnection;109;1;127;53
WireConnection;0;0;109;0
WireConnection;0;1;82;0
ASEEND*/
//CHKSM=2AC3B53B879CD4CC65EA89A122FF4F9C4BE354B3