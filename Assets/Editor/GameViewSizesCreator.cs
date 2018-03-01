using UnityEditor;
using UnityEngine;
using Kyusyukeigo.Helper;

[InitializeOnLoad]
public class GameViewSizesCreator
{
    static GameViewSizesCreator()
    {
        // Sceneビューにウィンドウを出す
        SceneView.onSceneGUIDelegate += (sceneView) =>
        {
            Handles.BeginGUI();
            GUILayout.Window(1, new Rect(20, 20, 120, 47), OnGUI, "Window");
            Handles.EndGUI();
        };
    }

    static void OnGUI(int id)
    {
        if (GUILayout.Button("Add GameViewSizes"))
        {
            // iOS端末の解像度を一括で登録
            AddGameViewSizes();
        }
    }

    static void AddGameViewSizes()
    {
        //AddGameViewSize("iPhone", 320, 480);
        //AddGameViewSize("iPhone 3G", 320, 480);
        //AddGameViewSize("iPhone 3GS", 320, 480);
        //AddGameViewSize("iPhone 4", 640, 960);
        //AddGameViewSize("iPhone 4S", 640, 960);
        //AddGameViewSize("iPhone 5", 640, 1136);
        //AddGameViewSize("iPhone 5s", 640, 1136);
        //AddGameViewSize("iPhone 5c", 640, 1136);
        //AddGameViewSize("iPhone SE", 640, 1136);
        AddGameViewSize("iPhone 6,7,8", 750, 1334);
        //AddGameViewSize("iPhone 6s", 750, 1334);
        //AddGameViewSize("iPhone 6", 640, 1136);
        //AddGameViewSize("iPhone 6s", 640, 1136);
        //AddGameViewSize("iPhone 6 Plus", 1242, 2208);
        //AddGameViewSize("iPhone 6s Plus", 1242, 2208);
        //AddGameViewSize("iPhone 6 Plus", 640, 1136);
        //AddGameViewSize("iPhone 6s Plus", 640, 1136);
        //AddGameViewSize("iPhone 6 Plus", 1125, 2001);
        //AddGameViewSize("iPhone 6s Plus", 1125, 2001);
        //AddGameViewSize("iPad Mini", 768, 1024);
        //AddGameViewSize("iPad Mini 2", 1536, 2048);
        //AddGameViewSize("iPad Mini 3", 1536, 2048);
        //AddGameViewSize("iPad Mini 4", 1536, 2048);
        //AddGameViewSize("iPad", 768, 1024);
        //AddGameViewSize("iPad 2", 768, 1024);
        //AddGameViewSize("iPad 3", 1536, 2048);
        //AddGameViewSize("iPad 4", 1536, 2048);
        AddGameViewSize("iPad Air", 1536, 2048);
        //AddGameViewSize("iPad Air 2", 1536, 2048);
        //AddGameViewSize("iPad Pro 9.7", 1536, 2048);
        //AddGameViewSize("iPad Pro 12.9", 2048, 2732);

        AddGameViewSize("iPhone 6,7,8 Plus", 1080, 1920);
        AddGameViewSize("iPhone X", 1125, 2436);
    }

    static void AddGameViewSize(string baseText, int width, int height)
    {
        Debug.Log("AddGameViewSize : " + baseText + ", width = " + width + ", height = " + height);

        Kyusyukeigo.Helper.GameViewSizeHelper.AddCustomSize(
          GameViewSizeGroupType.Standalone,
          new GameViewSizeHelper.GameViewSize
          {
              baseText = baseText,
              type = GameViewSizeHelper.GameViewSizeType.FixedResolution,
              width = width,
              height = height
          }
          );
    }
}