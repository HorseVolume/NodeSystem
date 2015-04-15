// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:370,x:33748,y:32693,varname:node_370,prsc:2|diff-88-RGB,custl-6616-OUT,clip-5707-R;n:type:ShaderForge.SFN_Tex2d,id:5707,x:33459,y:33171,ptovrint:False,ptlb:alpha clip,ptin:_alphaclip,varname:node_5707,prsc:2,tex:66f9ce041d81ee14c944fb440175748b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:88,x:32468,y:32705,ptovrint:False,ptlb:render target,ptin:_rendertarget,varname:node_88,prsc:2,tex:3bf1f358c4fef704bb6e7cd82c9aa29c,ntxv:0,isnm:False|UVIN-3619-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:3619,x:32092,y:32810,varname:node_3619,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:3788,x:32632,y:32774,varname:node_3788,prsc:2|A-88-R,B-88-G;n:type:ShaderForge.SFN_Multiply,id:2044,x:32887,y:32664,varname:node_2044,prsc:2|A-88-R,B-1227-RGB;n:type:ShaderForge.SFN_Tex2d,id:1227,x:32475,y:32917,ptovrint:False,ptlb:pattern,ptin:_pattern,varname:node_1227,prsc:2,tex:09cf515b56b69e74cb653c38b0062978,ntxv:0,isnm:False|UVIN-3619-UVOUT;n:type:ShaderForge.SFN_ObjectScale,id:2941,x:32080,y:32409,varname:node_2941,prsc:2,rcp:False;n:type:ShaderForge.SFN_Lerp,id:6616,x:33388,y:32493,varname:node_6616,prsc:2|A-88-RGB,B-6903-OUT,T-7452-OUT;n:type:ShaderForge.SFN_Divide,id:527,x:32787,y:32514,varname:node_527,prsc:2|A-5934-OUT,B-2941-X;n:type:ShaderForge.SFN_Slider,id:5934,x:32339,y:32581,ptovrint:False,ptlb:scale divide,ptin:_scaledivide,varname:node_5934,prsc:2,min:0,cur:60.57607,max:100;n:type:ShaderForge.SFN_ConstantClamp,id:7452,x:33004,y:32504,varname:node_7452,prsc:2,min:0,max:1|IN-527-OUT;n:type:ShaderForge.SFN_Color,id:1506,x:32475,y:33114,ptovrint:False,ptlb:node_1506,ptin:_node_1506,varname:node_1506,prsc:2,glob:False,c1:1,c2:0.3931034,c3:0,c4:0.2705882;n:type:ShaderForge.SFN_Lerp,id:6903,x:33378,y:32733,varname:node_6903,prsc:2|A-2044-OUT,B-8252-OUT,T-9097-R;n:type:ShaderForge.SFN_Multiply,id:8252,x:32887,y:32822,varname:node_8252,prsc:2|A-2044-OUT,B-1506-RGB;n:type:ShaderForge.SFN_Tex2d,id:9097,x:33459,y:32952,ptovrint:False,ptlb:color bubble clip,ptin:_colorbubbleclip,varname:_node_5707_copy,prsc:2,tex:36c87e358bd42624d8e60226e792bcd2,ntxv:0,isnm:False|UVIN-5514-OUT;n:type:ShaderForge.SFN_TexCoord,id:9932,x:32102,y:33024,varname:node_9932,prsc:2,uv:3;n:type:ShaderForge.SFN_Multiply,id:3055,x:32935,y:33051,varname:node_3055,prsc:2|A-9932-UVOUT,B-4050-OUT;n:type:ShaderForge.SFN_Add,id:5514,x:33198,y:33047,varname:node_5514,prsc:2|A-3055-OUT,B-4780-OUT;n:type:ShaderForge.SFN_Slider,id:4938,x:32318,y:33317,ptovrint:False,ptlb:true size,ptin:_truesize,varname:node_4938,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ConstantLerp,id:4050,x:32693,y:33234,varname:node_4050,prsc:2,a:1,b:0.4|IN-4938-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:4780,x:32693,y:33388,varname:node_4780,prsc:2,a:0,b:0.3|IN-4938-OUT;proporder:5707-88-1227-5934-1506-9097-4938;pass:END;sub:END;*/

Shader "Shader Forge/circlemask" {
    Properties {
        _alphaclip ("alpha clip", 2D) = "white" {}
        _rendertarget ("render target", 2D) = "white" {}
        _pattern ("pattern", 2D) = "white" {}
        _scaledivide ("scale divide", Range(0, 100)) = 60.57607
        _node_1506 ("node_1506", Color) = (1,0.3931034,0,0.2705882)
        _colorbubbleclip ("color bubble clip", 2D) = "white" {}
        _truesize ("true size", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _alphaclip; uniform float4 _alphaclip_ST;
            uniform sampler2D _rendertarget; uniform float4 _rendertarget_ST;
            uniform sampler2D _pattern; uniform float4 _pattern_ST;
            uniform float _scaledivide;
            uniform float4 _node_1506;
            uniform sampler2D _colorbubbleclip; uniform float4 _colorbubbleclip_ST;
            uniform float _truesize;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord3 : TEXCOORD3;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv3 : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv3 = v.texcoord3;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Vectors:
                float4 _alphaclip_var = tex2D(_alphaclip,TRANSFORM_TEX(i.uv0, _alphaclip));
                clip(_alphaclip_var.r - 0.5);
////// Lighting:
                float4 _rendertarget_var = tex2D(_rendertarget,TRANSFORM_TEX(i.screenPos.rg, _rendertarget));
                float4 _pattern_var = tex2D(_pattern,TRANSFORM_TEX(i.screenPos.rg, _pattern));
                float3 node_2044 = (_rendertarget_var.r*_pattern_var.rgb);
                float2 node_5514 = ((i.uv3*lerp(1,0.4,_truesize))+lerp(0,0.3,_truesize));
                float4 _colorbubbleclip_var = tex2D(_colorbubbleclip,TRANSFORM_TEX(node_5514, _colorbubbleclip));
                float3 finalColor = lerp(_rendertarget_var.rgb,lerp(node_2044,(node_2044*_node_1506.rgb),_colorbubbleclip_var.r),clamp((_scaledivide/objScale.r),0,1));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _alphaclip; uniform float4 _alphaclip_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 _alphaclip_var = tex2D(_alphaclip,TRANSFORM_TEX(i.uv0, _alphaclip));
                clip(_alphaclip_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
