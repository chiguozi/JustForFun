using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnimBaker
{
    public struct BakedAnimInfo
    {
        string _name;
        public string name { get { return _name; } }
        float _length;
        public float length { get { return _length; } }
        byte[] _texRawData;

        public byte[] rawData { get { return _texRawData; } }
        public int width{ get { return vertexCount; } }   //图片宽：顶点数量
        public int height { get { return frameCount; } }  //图片高：帧数
        public int vertexCount;
        public int frameCount;

        public void SetName(string name)
        {
            _name = name;
        }
        public void SetLength(float length)
        {
            _length = length;
        }
        public void SetRawData(byte[] bytes)
        {
            _texRawData = bytes;
        }
    }

    static public BakedAnimInfo Bake(Animation anim, SkinnedMeshRenderer smr, string animName)
    {
        BakedAnimInfo ret = new BakedAnimInfo();
        AnimationState animState = anim[animName];
        if(null == animState)
            return ret;
        var clip = animState.clip;
        if (null == clip)
        {
            return ret;
        }
        if (!clip.legacy)
        {
            return ret;
        }
        ret.SetName(animName) ;
        ret.SetLength(clip.length);
        ret.vertexCount = Mathf.NextPowerOfTwo(smr.sharedMesh.vertexCount);
        ret.frameCount = Mathf.ClosestPowerOfTwo((int)(clip.frameRate * ret.length));

        if (ret.vertexCount > 0 && ret.frameCount > 0)
        {

            anim.Play(animName);

            Mesh mesh = new Mesh();
            Texture2D tex = new Texture2D(ret.width, ret.height, TextureFormat.RGBAHalf, false);

            float perFrameTime = ret.length / ret.frameCount;

            float smapleTime = 0f;
            for (int i = 0; i < ret.frameCount; ++i)
            {
                animState.time = smapleTime;

                anim.Sample();
                smr.BakeMesh(mesh);
                //得到烘焙的顶点数据：存于mesh中
                int numVertex = mesh.vertexCount;
                for (int j = 0; j < numVertex; ++j)
                {
                    Vector3 v3 = mesh.vertices[j];
                    tex.SetPixel(j, i, new Color(v3.x, v3.y, v3.z));
                }
                smapleTime += perFrameTime;
            }
            tex.Apply();
            ret.SetRawData(tex.GetRawTextureData());
        }
        else
            ret.SetRawData(null);
        return ret;
    }
    static string folder = "Assets/Labs/GPU_Skinning/BakeOutput/";

#if UNITY_EDITOR
    [MenuItem("Assets/Bake")]
    static void CreateAndSave()
    {
        GameObject go = Selection.activeGameObject;
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        var anim = go.GetComponentInChildren<Animation>();
        var smr = go.GetComponentInChildren<SkinnedMeshRenderer>();

        var shader = Shader.Find("chenjd/AnimMapShader");

        var anims = new List<AnimationState>(anim.Cast<AnimationState>());
        for (int i = 0; i < anims.Count; ++i)
        {
            var ret = Bake(anim,smr,anims[i].name);
            Texture2D tex=null;
            if (null != ret.rawData)
            {
                tex = new Texture2D(ret.width, ret.height, TextureFormat.RGBAHalf, false);
                tex.LoadRawTextureData(ret.rawData);
                AssetDatabase.CreateAsset(tex,folder+ ret.name+".asset");
            }
            Material mat = new Material(shader);
            mat.SetTexture("_MainTex", smr.sharedMaterial.mainTexture);
            mat.SetTexture("_AnimMap", tex);
            mat.SetFloat("_AnimLen", ret.length);
            AssetDatabase.CreateAsset(mat, folder + ret.name + ".mat");
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif


}