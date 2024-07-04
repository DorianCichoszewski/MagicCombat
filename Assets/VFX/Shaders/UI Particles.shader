// Made with Amplify Shader Editor v1.9.4.4
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UI Particles"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
         [Header(Emitter Settings)]_EmitterDimensions("Emitter Dimensions", Vector) = (1,1,0,0)
         _ParticleLifetime("Particle Lifetime", Float) = 1
         _Whatever("[f]Time", Float) = 0
         [Toggle(_USECUSTOMTIME_ON)] _UseCustomTime("Use Custom Time", Float) = 0
         _CustomTImesiUSECUSTOMTIME_ON("Custom TIme [si(USECUSTOMTIME_ON)]", Float) = 0
         _fFlipbook("[f] Flipbook", Float) = 0
         [Toggle(_USEFLIPBOOK_ON)] _UseFlipbook("Use Flipbook", Float) = 0
         _FlipbbokSpeedsi_USEFLIPBOOK_ON("Flipbbok Speed [si(_USEFLIPBOOK_ON)]", Float) = 1
         _FlipbookCellssi_USEFLIPBOOK_ON("Flipbook Cells [si(_USEFLIPBOOK_ON)]", Vector) = (1,1,0,0)
         _fSizeLifetime("[f] Size Lifetime", Float) = 0
         _StartSize("Start Size", Float) = 0
         _EndSize("End Size", Float) = 1
         [KeywordEnum(Linear,FadeInOut,Curve)] _SizeType("SizeType", Float) = 1
         _cLifetimeSizesi_SIZETYPECurve("[c] Lifetime Size [si(_SIZETYPE=Curve)]", 2D) = "white" {}
         _FadeInPowersi_SIZETYPEFadeInOut("Fade In Power [si(_SIZETYPE=FadeInOut)]", Float) = 0
         _FadeOutPowersi_SIZETYPEFadeInOut("Fade Out Power [si(_SIZETYPE=FadeInOut)]", Float) = 0
         _fUnrealated("[f] Unrealated", Float) = 0
         [HideInInspector] _texcoord( "", 2D ) = "white" {}


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
            #define ASE_NEEDS_VERT_POSITION
            #define ASE_NEEDS_FRAG_COLOR
            #pragma shader_feature_local _USECUSTOMTIME_ON
            #pragma shader_feature _SIZETYPE_LINEAR _SIZETYPE_FADEINOUT _SIZETYPE_CURVE
            #pragma shader_feature_local _USEFLIPBOOK_ON


            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 ase_tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                float4 ase_tangent : TANGENT;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            uniform float _fSizeLifetime;
            uniform float _fUnrealated;
            uniform float _fFlipbook;
            uniform float _Whatever;
            uniform float _CustomTImesiUSECUSTOMTIME_ON;
            uniform float _ParticleLifetime;
            uniform float2 _EmitterDimensions;
            uniform float _StartSize;
            uniform float _EndSize;
            uniform float _FadeInPowersi_SIZETYPEFadeInOut;
            uniform float _FadeOutPowersi_SIZETYPEFadeInOut;
            uniform sampler2D _cLifetimeSizesi_SIZETYPECurve;
            uniform float2 _FlipbookCellssi_USEFLIPBOOK_ON;
            uniform float _FlipbbokSpeedsi_USEFLIPBOOK_ON;
            float3 HashVector33_g1( float3 p )
            {
            	uint3 v = (uint3) (int3) p;
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

                float3 appendResult150 = (float3(v.ase_tangent.x , v.ase_tangent.y , v.ase_tangent.z));
                float3 Correct_Object_Position152 = appendResult150;
                float3 Vertex_Offset_From_Center189 = ( v.vertex.xyz - Correct_Object_Position152 );
                float Quad_Value153 = v.ase_tangent.w;
                #ifdef _USECUSTOMTIME_ON
                float staticSwitch240 = _CustomTImesiUSECUSTOMTIME_ON;
                #else
                float staticSwitch240 = _Time.y;
                #endif
                float Time242 = staticSwitch240;
                float temp_output_158_0 = ( Quad_Value153 + ( Time242 / _ParticleLifetime ) );
                float3 temp_cast_0 = (( floor( ( Quad_Value153 * 16250.0 ) ) + temp_output_158_0 )).xxx;
                float3 p3_g1 = temp_cast_0;
                float3 localHashVector33_g1 = HashVector33_g1( p3_g1 );
                float3 Hash_3_Quad97 = localHashVector33_g1;
                float2 appendResult43 = (float2(Hash_3_Quad97.xy));
                float temp_output_117_0 = frac( temp_output_158_0 );
                float Lifetime_Progress194 = temp_output_117_0;
                float temp_output_209_0 = ( pow( Lifetime_Progress194 , _FadeInPowersi_SIZETYPEFadeInOut ) * pow( ( 1.0 - Lifetime_Progress194 ) , _FadeOutPowersi_SIZETYPEFadeInOut ) );
                float2 temp_cast_3 = (saturate( temp_output_209_0 )).xx;
                #if defined( _SIZETYPE_LINEAR )
                float staticSwitch222 = Lifetime_Progress194;
                #elif defined( _SIZETYPE_FADEINOUT )
                float staticSwitch222 = temp_output_209_0;
                #elif defined( _SIZETYPE_CURVE )
                float staticSwitch222 = tex2Dlod( _cLifetimeSizesi_SIZETYPECurve, float4( temp_cast_3, 0, 0.0) ).r;
                #else
                float staticSwitch222 = temp_output_209_0;
                #endif
                float lerpResult213 = lerp( _StartSize , _EndSize , staticSwitch222);
                float Lifetime_Size192 = lerpResult213;
                
                OUT.ase_tangent = v.ase_tangent;

                v.vertex.xyz += ( -Vertex_Offset_From_Center189 + float3( ( ( ( appendResult43 * float2( 2,2 ) ) + float2( -1,-1 ) ) * _EmitterDimensions ) ,  0.0 ) + ( Vertex_Offset_From_Center189 * Lifetime_Size192 ) );

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

                float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                float temp_output_4_0_g18 = _FlipbookCellssi_USEFLIPBOOK_ON.x;
                float temp_output_5_0_g18 = _FlipbookCellssi_USEFLIPBOOK_ON.y;
                float Quad_Value153 = IN.ase_tangent.w;
                #ifdef _USECUSTOMTIME_ON
                float staticSwitch240 = _CustomTImesiUSECUSTOMTIME_ON;
                #else
                float staticSwitch240 = _Time.y;
                #endif
                float Time242 = staticSwitch240;
                float temp_output_158_0 = ( Quad_Value153 + ( Time242 / _ParticleLifetime ) );
                float Quad_Time229 = temp_output_158_0;
                // *** BEGIN Flipbook UV Animation vars ***
                // Total tiles of Flipbook Texture
                float fbtotaltiles246_g18 = min( temp_output_4_0_g18 * temp_output_5_0_g18, ( ( temp_output_4_0_g18 * temp_output_5_0_g18 ) - 0.0 ) + 1 );
                // Offsets for cols and rows of Flipbook Texture
                float fbcolsoffset246_g18 = 1.0f / temp_output_4_0_g18;
                float fbrowsoffset246_g18 = 1.0f / temp_output_5_0_g18;
                // Speed of animation
                float fbspeed246_g18 = Quad_Time229 * ( ( _FlipbookCellssi_USEFLIPBOOK_ON.x * _FlipbookCellssi_USEFLIPBOOK_ON.y ) * _FlipbbokSpeedsi_USEFLIPBOOK_ON );
                // UV Tiling (col and row offset)
                float2 fbtiling246_g18 = float2(fbcolsoffset246_g18, fbrowsoffset246_g18);
                // UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
                // Calculate current tile linear index
                float fbcurrenttileindex246_g18 = floor( fmod( fbspeed246_g18 + 0.0, fbtotaltiles246_g18) );
                fbcurrenttileindex246_g18 += ( fbcurrenttileindex246_g18 < 0) ? fbtotaltiles246_g18 : 0;
                // Obtain Offset X coordinate from current tile linear index
                float fblinearindextox246_g18 = round ( fmod ( fbcurrenttileindex246_g18, temp_output_4_0_g18 ) );
                // Multiply Offset X by coloffset
                float fboffsetx246_g18 = fblinearindextox246_g18 * fbcolsoffset246_g18;
                // Obtain Offset Y coordinate from current tile linear index
                float fblinearindextoy246_g18 = round( fmod( ( fbcurrenttileindex246_g18 - fblinearindextox246_g18 ) / temp_output_4_0_g18, temp_output_5_0_g18 ) );
                // Reverse Y to get tiles from Top to Bottom
                fblinearindextoy246_g18 = (int)(temp_output_5_0_g18-1) - fblinearindextoy246_g18;
                // Multiply Offset Y by rowoffset
                float fboffsety246_g18 = fblinearindextoy246_g18 * fbrowsoffset246_g18;
                // UV Offset
                float2 fboffset246_g18 = float2(fboffsetx246_g18, fboffsety246_g18);
                // Flipbook UV
                half2 fbuv246_g18 = IN.texcoord.xy * fbtiling246_g18 + fboffset246_g18;
                // *** END Flipbook UV Animation vars ***
                int flipbookFrame246_g18 = ( ( int )fbcurrenttileindex246_g18);
                #ifdef _USEFLIPBOOK_ON
                float4 staticSwitch255 = tex2D( _MainTex, fbuv246_g18 );
                #else
                float4 staticSwitch255 = tex2D( _MainTex, uv_MainTex );
                #endif
                

                half4 color = ( IN.color * staticSwitch255 );

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
    CustomEditor "Chroma"
	
	Fallback Off
}
/*ASEBEGIN
Version=19404
Node;AmplifyShaderEditor.CommentaryNode;236;-3328,-448;Inherit;False;1027.199;559.7001;;20;241;121;242;240;243;195;196;229;97;184;120;182;181;194;117;158;166;154;163;245;Hash & TIme;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;121;-3280,-48;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;241;-3312,32;Inherit;False;Property;_CustomTImesiUSECUSTOMTIME_ON;Custom TIme [si(USECUSTOMTIME_ON)];4;0;Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;191;-4208,-448;Inherit;False;788;514.95;;8;150;147;152;153;83;155;149;189;Vertex Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;240;-2976,-16;Inherit;False;Property;_UseCustomTime;Use Custom Time;3;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TangentVertexDataNode;147;-4160,-400;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;242;-2688,-16;Inherit;False;Time;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;153;-3824,-304;Float;False;Quad Value;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;163;-3312,-160;Inherit;False;Property;_ParticleLifetime;Particle Lifetime;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;243;-3280,-240;Inherit;False;242;Time;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;154;-3312,-384;Inherit;False;153;Quad Value;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;166;-3104,-224;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;158;-2944,-256;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;117;-2800,-224;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;227;-3170,798;Inherit;False;1751.34;611.6434;;15;253;198;209;192;213;222;200;201;225;207;203;204;208;206;202;Lifetime Scale;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;194;-2544,-224;Inherit;False;Lifetime Progress;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;202;-3120,928;Inherit;False;194;Lifetime Progress;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;181;-3040,-400;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;16250;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;206;-2864,960;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;208;-3008,1024;Inherit;False;Property;_FadeOutPowersi_SIZETYPEFadeInOut;Fade Out Power [si(_SIZETYPE=FadeInOut)];15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;204;-3008,848;Inherit;False;Property;_FadeInPowersi_SIZETYPEFadeInOut;Fade In Power [si(_SIZETYPE=FadeInOut)];14;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;182;-2912,-400;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;203;-2672,848;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;207;-2672,976;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-2816,-400;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;209;-2528,896;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;184;-2704,-400;Inherit;False;HashVector3;-1;;1;e08360d15330cbd429407eddabb4c325;0;1;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;253;-2624,1184;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;150;-3968,-400;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-2512,-400;Inherit;False;Hash 3 Quad;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;152;-3840,-400;Inherit;False;Correct Object Position;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;238;-1440,-272;Inherit;False;1333.84;573.8301;;10;128;176;183;255;259;239;230;107;175;188;Flipbook;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;225;-2480,1152;Inherit;True;Property;_cLifetimeSizesi_SIZETYPECurve;[c] Lifetime Size [si(_SIZETYPE=Curve)];13;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;83;-4160,-208;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;228;-800,512;Inherit;False;97;Hash 3 Quad;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;201;-1952,944;Inherit;False;Property;_EndSize;End Size;11;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;200;-1952,880;Inherit;False;Property;_StartSize;Start Size;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;222;-2224,928;Inherit;False;Property;_SizeType;SizeType;12;0;Create;True;0;0;0;True;0;False;0;1;1;True;_ScaleCurve;KeywordEnum;3;Linear;FadeInOut;Curve;Create;False;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;-4160,-48;Inherit;False;152;Correct Object Position;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector2Node;128;-1424,-80;Inherit;False;Property;_FlipbookCellssi_USEFLIPBOOK_ON;Flipbook Cells [si(_USEFLIPBOOK_ON)];8;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;149;-3888,-96;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-592,512;Inherit;False;FLOAT2;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;213;-1792,880;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;229;-2512,-288;Inherit;False;Quad Time;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;183;-1200,128;Inherit;False;Property;_FlipbbokSpeedsi_USEFLIPBOOK_ON;Flipbbok Speed [si(_USEFLIPBOOK_ON)];7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;176;-1104,32;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;237;-432,336;Inherit;False;457.3601;129.67;Treat Offset as position input;2;219;220;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;189;-3728,-96;Inherit;False;Vertex Offset From Center;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-432,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;192;-1648,880;Inherit;False;Lifetime Size;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;188;-944,80;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;107;-1056,-192;Inherit;False;0;0;_MainTex;Shader;True;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;230;-976,192;Inherit;False;229;Quad Time;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;190;-512,832;Inherit;False;189;Vertex Offset From Center;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;217;-464,896;Inherit;False;192;Lifetime Size;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-288,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;-1,-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;46;-416,608;Inherit;False;Property;_EmitterDimensions;Emitter Dimensions;0;1;[Header];Create;True;1;Emitter Settings;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;219;-416,384;Inherit;False;189;Vertex Offset From Center;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;175;-752,-32;Inherit;False;Flipbook;-1;;18;53c2488c220f6564ca6c90721ee16673;3,68,0,217,0,244,1;11;51;SAMPLER2D;0.0;False;167;SAMPLERSTATE;0;False;13;FLOAT2;0,0;False;24;FLOAT;0;False;210;FLOAT;4;False;4;FLOAT;4;False;5;FLOAT;4;False;130;FLOAT;4;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;INT;218
Node;AmplifyShaderEditor.SamplerNode;259;-736,-224;Inherit;True;Property;_TextureSample0;Texture Sample 0;17;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-144,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-192,832;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;220;-144,384;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;162;-272,-464;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;255;-352,-32;Inherit;False;Property;_UseFlipbook;Use Flipbook;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;257;-3863.343,164.758;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;199;-3552,928;Inherit;False;Random Range;-1;;17;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;98;-3552,864;Inherit;False;97;Hash 3 Quad;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;218;-4101.276,133.6707;Inherit;False;Property;_fUnrealated;[f] Unrealated;16;0;Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;192,512;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT2;0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;196;-2688,-176;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;195;-2544,-160;Inherit;False;Lifetime seconds;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;198;-3056,1168;Inherit;False;Property;_fSizeLifetime;[f] Size Lifetime;9;0;Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;245;-2448,-32;Inherit;False;Property;_Whatever;[f]Time;2;0;Create;False;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;239;-352,-192;Inherit;False;Property;_fFlipbook;[f] Flipbook;5;0;Create;True;0;0;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;96,-240;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;368,144;Float;False;True;-1;2;Chroma;0;3;UI Particles;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;True;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;240;1;121;0
WireConnection;240;0;241;0
WireConnection;242;0;240;0
WireConnection;153;0;147;4
WireConnection;166;0;243;0
WireConnection;166;1;163;0
WireConnection;158;0;154;0
WireConnection;158;1;166;0
WireConnection;117;0;158;0
WireConnection;194;0;117;0
WireConnection;181;0;154;0
WireConnection;206;0;202;0
WireConnection;182;0;181;0
WireConnection;203;0;202;0
WireConnection;203;1;204;0
WireConnection;207;0;206;0
WireConnection;207;1;208;0
WireConnection;120;0;182;0
WireConnection;120;1;158;0
WireConnection;209;0;203;0
WireConnection;209;1;207;0
WireConnection;184;1;120;0
WireConnection;253;0;209;0
WireConnection;150;0;147;1
WireConnection;150;1;147;2
WireConnection;150;2;147;3
WireConnection;97;0;184;0
WireConnection;152;0;150;0
WireConnection;225;1;253;0
WireConnection;222;1;202;0
WireConnection;222;0;209;0
WireConnection;222;2;225;1
WireConnection;149;0;83;0
WireConnection;149;1;155;0
WireConnection;43;0;228;0
WireConnection;213;0;200;0
WireConnection;213;1;201;0
WireConnection;213;2;222;0
WireConnection;229;0;158;0
WireConnection;176;0;128;1
WireConnection;176;1;128;2
WireConnection;189;0;149;0
WireConnection;44;0;43;0
WireConnection;192;0;213;0
WireConnection;188;0;176;0
WireConnection;188;1;183;0
WireConnection;45;0;44;0
WireConnection;175;51;107;0
WireConnection;175;4;128;1
WireConnection;175;5;128;2
WireConnection;175;130;188;0
WireConnection;175;2;230;0
WireConnection;259;0;107;0
WireConnection;47;0;45;0
WireConnection;47;1;46;0
WireConnection;80;0;190;0
WireConnection;80;1;217;0
WireConnection;220;0;219;0
WireConnection;255;1;259;0
WireConnection;255;0;175;53
WireConnection;82;0;220;0
WireConnection;82;1;47;0
WireConnection;82;2;80;0
WireConnection;196;0;117;0
WireConnection;196;1;163;0
WireConnection;195;0;196;0
WireConnection;109;0;162;0
WireConnection;109;1;255;0
WireConnection;0;0;109;0
WireConnection;0;1;82;0
ASEEND*/
//CHKSM=4FB264E3040C3856934676E6636989A6403A186A