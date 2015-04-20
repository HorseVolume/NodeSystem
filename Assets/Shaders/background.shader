// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:651,x:33271,y:32684,varname:node_651,prsc:2|custl-4503-OUT;n:type:ShaderForge.SFN_ScreenPos,id:1795,x:32243,y:32914,varname:node_1795,prsc:2,sctp:1;n:type:ShaderForge.SFN_Color,id:5339,x:32666,y:32680,ptovrint:False,ptlb:node_5339,ptin:_node_5339,varname:node_5339,prsc:2,glob:False,c1:1,c2:0.9024654,c3:0.8382353,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4269,x:32794,y:32956,ptovrint:False,ptlb:pattern,ptin:_pattern,varname:node_4269,prsc:2,tex:5301aab1247d0684ab40ea8b3df5ec7d,ntxv:0,isnm:False|UVIN-1795-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4503,x:32945,y:32758,varname:node_4503,prsc:2|A-5339-RGB,B-4269-RGB;proporder:5339-4269;pass:END;sub:END;*/

Shader "Shader Forge/background" {
    Properties {
        _node_5339 ("node_5339", Color) = (1,0.9024654,0.8382353,1)
        _pattern ("pattern", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform float4 _node_5339;
            uniform sampler2D _pattern; uniform float4 _pattern_ST;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
/////// Vectors:
////// Lighting:
                float4 _pattern_var = tex2D(_pattern,TRANSFORM_TEX(float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg, _pattern));
                float3 finalColor = (_node_5339.rgb*_pattern_var.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
