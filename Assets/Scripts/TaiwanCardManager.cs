using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Net.Kixmate.KixUtility;

public class TaiwanCardManager : MonoBehaviour {
	const bool DEBUG = true;

    [SerializeField]
    WebViewObject webView = null;

    string htmlFilePath;

    // Use this for initialization
    void Start () {
		Screen.fullScreen = false;

        InitializeWebView();
	}

    private void InitializeWebView()
    {
        // WebView表示用にHTMLファイルを用意する
        htmlFilePath = Path.Combine(Application.persistentDataPath, "description.html");
		KX.D (DEBUG, "htmlFilePath={0}", htmlFilePath);

        // HTMLファイルに初期表示するHTMLを生成
        string html = GeneratePrepareHTML();

        // HTMLファイルを表示する
        PreviewHtml(html);
    }

    private void PreviewHtml(string html)
    {
        using (var writer = new StreamWriter(htmlFilePath, false))
        {
            writer.Write(html);
            writer.Close();
        }

        // WebViewObject の初期化時にWebページ側から呼び出すことができるコールバック関数を定義する。
        // Web側からコールバック関数呼び出すには、リンク要素の href 属性などURLを指定する箇所で
        // 'unit:(任意の文字列)' のように指定すると、コールバック関数が呼び出される。
        // このとき、"(任意の文字列)"の部分が関数の引数として渡される。
        webView.Init((string msg) => {
			KX.D(DEBUG, "Call from Web view : " + msg);
        });
        webView.LoadURL("file://" + htmlFilePath);

		var rectTransform = webView.GetComponent<RectTransform> ();
		AdjustWebViewMargin (webView, rectTransform);
        webView.SetVisibility(true);
    }

    private string GeneratePrepareHTML()
    {
        string html = 
@"<html>
<head>
  <meta name='viewport' content='width=device-width, initial-scale=1'>
  <style>
    body { background: red; }
  </style>
</head>
<body>
Hello unity-webview !!!<br/>
<ul>
  <li><a href='unity:hoge'>callback 'hoge'</a></li>
  <li><a href='unity:fuga'>callback 'fuga'</a></li>
</ul>
<p>abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz</p>
</body>
</html>
";
        return html;
    }

    public void PreviewDescription(string text)
    {
        GenerateHTML(text);

        webView.EvaluateJS("location.reload();");
    }

	private string GenerateHTML(string text, bool imgEnable = false)
    {
		string imgTag = imgEnable ? "<image src='./imgUrl.png' />" : "";
        string html =
@"<html>
<body>
<p>"+ imgTag + text+ @"</p>
</body>
</html>
";
        return html;
	}

	/// <summary>
	/// WebViewの座標とサイズを設定する.
	/// Adjusts the web view margin.
	/// </summary>
	void AdjustWebViewMargin (WebViewObject webViewObject, RectTransform rectTransform)
	{
		// TODO 横幅自動max設定でのanchorを考慮したmargin値取得を実現
		// TODO 不要なコードの削除

		// このUISpriteの上にWebviewを表示したい
		//var background = GetComponent<CanvasRenderer> ();
		//var rectTransform = GetComponent<RectTransform>();
		var localPos = rectTransform.transform.localPosition;
		var localScale = rectTransform.transform.localScale;

		// "Background"を写すカメラ
		var camera = transform.GetComponent<Camera>();

		//		// "Background"の左下と右上のローカル座標(UIWidget.PivotはCenter前提とする)
		//		var localLB = new Vector3(localPos.x - background.width * 0.5f * localScale.x, localPos.y - background.height * 0.5f * localScale.y, 0f);
		//		var localRT = new Vector3(localPos.x + background.width * 0.5f * localScale.x, localPos.y + background.height * 0.5f * localScale.y, 0f);

		// "Background"の左下と右上のワールド座標
		var worldCorners = new Vector3[4];
		rectTransform.GetWorldCorners (worldCorners);
		var worldLB = worldCorners[0];
		var worldRT = worldCorners[2];

		// "Background"の左下と右上のスクリーン座標
		//var screenLB = camera.WorldToScreenPoint(worldLB);
		//var screenRT = camera.WorldToScreenPoint(worldRT);
		var localCorners = new Vector3[4];
		rectTransform.GetLocalCorners (localCorners);
		var screenLB = localCorners [0];
		var screenRT = localCorners [2];//camera.WorldToScreenPoint(worldRT);

		// マージンの計算
//		int marginL = (int)(screenLB.x);
//		int marginT = (int)(Screen.height - screenRT.y);
//		int marginR = (int)(Screen.width - screenRT.x);
		//		int marginB = (int)(screenLB.y);
		var canvas = rectTransform.GetComponentInParent<Canvas>();
		var canvasRect = canvas.pixelRect;
		var wvLeft = rectTransform.offsetMin.x;
		var wvTop = canvasRect.height - rectTransform.offsetMax.y;	// 画面下から算出
		var wvRight = canvasRect.width - rectTransform.offsetMax.x;	// 画面右から算出
		var wvBottom = rectTransform.offsetMin.y;
		const int LB = 0;
		const int RT = 2;
		wvLeft = worldCorners [LB].x;
		wvTop = canvasRect.height - worldCorners [RT].y;
		wvRight = canvasRect.width - worldCorners [RT].x;
		wvBottom = worldCorners [LB].y;

		int marginL = (int)(wvLeft);
		int marginT = (int)(wvTop);
		int marginR = (int)(wvRight);
		int marginB = (int)(wvBottom);

		// Webview表示
		webViewObject.SetMargins(marginL, marginT, marginR, marginB);
		KX.D(DEBUG, "margin[L,T,R,B]=[{0},{1},{2},{3}]", marginL, marginT, marginR, marginB);
	}


    void Update()
    {
        // プラットフォームがアンドロイドかチェック
        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // アプリケーション終了
                Application.Quit();
                return;
            }
        }
    }
}
