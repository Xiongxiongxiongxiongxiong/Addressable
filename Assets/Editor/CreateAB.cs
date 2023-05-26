using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateAB : Editor
{
    [MenuItem("Tool / Create AB")]
   static void Creator()
    {
       // ������һ����Ϊ builds �� AssetBundleBuild ���͵��б����ڴ洢Ҫ��������Դ������Ϣ��
        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        //�����õ��ļ����µ�Ŀ¼
        var assets= AssetDatabase.FindAssets("", new[] { "Assets/Addresable_Res" });
        //ѭ��������Щ����
        foreach (var a in assets)
        {
            //���ݲ��ҵ���Ŀ¼��ȡ�õ���·��
            var path = AssetDatabase.GUIDToAssetPath(a);
            //����·��������Щ����
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);

            AssetBundleBuild build = new AssetBundleBuild();

            build.assetBundleName = obj.name;
            build.assetNames = new[] { path };
            builds.Add(build);

        }
        // ============================shaders

        //var shaders = AssetDatabase.FindAssets("t:Shader", new[] { "Packages/io.jagat.artlogic/Shaders/Avatar3D_New" });
        //AssetBundleBuild shaderBuild = new AssetBundleBuild();

        //List<string> paths = new List<string>();
        //foreach (var sh in shaders)
        //{
        //    var path = AssetDatabase.GUIDToAssetPath(sh);
        //    paths.Add(path);
           
          
        //}
        //shaderBuild.assetBundleName = "avatar_shaders";
        //shaderBuild.assetNames = paths.ToArray();
        //builds.Add(shaderBuild);


        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath ,builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
