// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:370,x:33748,y:32693,varname:node_370,prsc:2|diff-88-RGB,custl-6508-OUT,clip-8014-R;n:type:ShaderForge.SFN_Tex2d,id:88,x:32723,y:32363,ptovrint:False,ptlb:render target,ptin:_rendertarget,varname:node_88,prsc:2,tex:3bf1f358c4fef704bb6e7cd82c9aa29c,ntxv:0,isnm:False|UVIN-9217-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:3619,x:32102,y:32791,varname:node_3619,prsc:2,sctp:1;n:type:ShaderForge.SFN_Tex2d,id:1227,x:32385,y:32937,ptovrint:False,ptlb:pattern,ptin:_pattern,varname:node_1227,prsc:2,tex:5301aab1247d0684ab40ea8b3df5ec7d,ntxv:0,isnm:False|UVIN-3619-UVOUT;n:type:ShaderForge.SFN_Slider,id:5934,x:32630,y:33014,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_5934,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:3835,x:33332,y:32885,ptovrint:False,ptlb:user,ptin:_user,varname:node_3835,prsc:2,tex:78f24836ae12743489c8e1cf62afafe1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8014,x:33054,y:32879,ptovrint:False,ptlb:mask2,ptin:_mask2,varname:node_8014,prsc:2,tex:6fbb2ae68894dcc4b8d54e868c95680d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9387,x:32694,y:32585,varname:node_9387,prsc:2|A-88-R,B-1227-RGB;n:type:ShaderForge.SFN_Lerp,id:6508,x:32879,y:32573,varname:node_6508,prsc:2|A-88-RGB,B-9387-OUT,T-5934-OUT;n:type:ShaderForge.SFN_TexCoord,id:2179,x:32020,y:32567,varname:node_2179,prsc:2,uv:0;n:type:ShaderForge.SFN_ViewVector,id:4568,x:32182,y:32363,varname:node_4568,prsc:2;n:type:ShaderForge.SFN_Rotator,id:9217,x:32342,y:32700,varname:node_9217,prsc:2|UVIN-2179-UVOUT,ANG-5830-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1520,x:32324,y:32443,varname:node_1520,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5830,x:32156,y:32598,ptovrint:False,ptlb:node_5830,ptin:_node_5830,varname:node_5830,prsc:2,glob:False,v1:0;proporder:88-1227-5934-3835-8014-5830;pass:END;sub:END;*/

Shader "Shader Forge/circlemask" {
    Properties {
        _rendertarget ("render target", 2D) = "white" {}
        _pattern ("pattern", 2D) = "white" {}
        _opacity ("opacity", Range(0, 1)) = 1
        _user ("user", 2D) = "white" {}
        _mask2 ("mask2", 2D) = "white" {}
        _node_5830 ("node_5830", Float ) = 0
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
            uniform sampler2D _rendertarget; uniform float4 _rendertarget_ST;
            uniform sampler2D _pattern; uniform float4 _pattern_ST;
            uniform float _opacity;
            uniform sampler2D _mask2; uniform float4 _mask2_ST;
            uniform float _node_5830;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Vectors:
                float4 _mask2_var = tex2D(_mask2,TRANSFORM_TEX(i.uv0, _mask2));
                clip(_mask2_var.r - 0.5);
////// Lighting:
                float node_9217_ang = _node_5830;
                float node_9217_spd = 1.0;
                float node_9217_cos = cos(node_9217_spd*node_9217_ang);
                float node_9217_sin = sin(node_9217_spd*node_9217_ang);
                float2 node_9217_piv = float2(0.5,0.5);
                float2 node_9217 = (mul(i.uv0-node_9217_piv,float2x2( node_9217_cos, -node_9217_sin, node_9217_sin, node_9217_cos))+node_9217_piv);
                float4 _rendertarget_var = tex2D(_rendertarget,TRANSFORM_TEX(node_9217, _rendertarget));
                float4 _pattern_var = tex2D(_pattern,TRANSFORM_TEX(float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg, _pattern));
                float3 node_6508 = lerp(_rendertarget_var.rgb,(_rendertarget_var.r*_pattern_var.rgb),_opacity);
                float3 finalColor = node_6508;
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
            uniform sampler2D _mask2; uniform float4 _mask2_ST;
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
                float4 _mask2_var = tex2D(_mask2,TRANSFORM_TEX(i.uv0, _mask2));
                clip(_mask2_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
