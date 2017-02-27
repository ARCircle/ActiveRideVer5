using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIText : MonoBehaviour {

    // 敵データ格納用の配列データ(とりあえず初期値はnull値)
    private int[,] stageMapDatas = null;
    // CSVから切り分けられた文字列型２次元配列データ 
    public string[,] sdataArrays;

    //読み込めたか確認の表示用の変数
    private int height = 0;    //行数
    private int width = 0;    //列数

    public int index;

    private bool isShowText;

    // シナリオを格納する
    public UnityEngine.UI.Text uiText; // uiTextへの参照を保つ

    int currentLine; // 現在の行番号

    private float TimeLeft;
    private const string path = "/CSV/Story.csv";

    void Start()
    {
        //データパスを設定
        //このデータパスは、Assetフォルダ以下の位置を書くので/で階層を区切り、CSVデータ名まで書かないと読み込んでくれない
        //データを読み込む(引数：データパス)
        Debug.Log(Application.dataPath + path);

        readCSVData(Application.dataPath + path, ref sdataArrays);

        isShowText = true;

        currentLine = 0;
        TextUpdate();

    }

    private void OnEnable()
    {

        readCSVData(Application.dataPath + path, ref sdataArrays);

        isShowText = true;

        currentLine = 0;
        TextUpdate();

        uiText.text = "";

    }

    void Update()
    {
        // 行が残っていればテキスト更新
        if (currentLine < sdataArrays.GetLength(0) && isShowText)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0.0)
            {
                TimeLeft = 1.0f;

                TextUpdate();
            }
        } else
        {
            isShowText = false;
            currentLine = 0;
        }
    }

    // テキストを更新する
    void TextUpdate()
    {
        // 現在の行のテキストをuiTextに流し込み、現在の行番号を一つ追加
        // indexはアタッチ先のObjectにより指定, csvの列=1つのStoryに対応する
        uiText.text += sdataArrays[currentLine, index] + "\n";
        Debug.Log(currentLine+","+index+ sdataArrays[currentLine, index]);
        Debug.Log(uiText.name);
        currentLine++;
    }

    //from http://qiita.com/Akematty/items/2fbb61b55132ced4a3be

    // CSVデータを文字列型２次元配列に変換する
    //                      ファイルパス,変換される配列の値(参照渡し)
    private void readCSVData(string path, ref string[,] sdata)
    {
        // ストリームリーダーsrに読み込む
        StreamReader sr = new StreamReader(path);
        // ストリームリーダーをstringに変換
        string strStream = sr.ReadToEnd();
        // StringSplitOptionを設定(要はカンマとカンマに何もなかったら格納しないことにする)
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        // 行に分ける
        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        // カンマ分けの準備(区分けする文字を設定する)
        char[] spliter = new char[1] { ',' };

        // 行数設定
        int h = lines.Length;
        // 列数設定
        int w = lines[0].Split(spliter, option).Length;

        // 返り値の2次元配列の要素数を設定
        sdata = new string[h, w];

        // 行データを切り分けて,2次元配列へ変換する
        for (int i = 0; i < h; i++)
        {
            string[] splitedData = lines[i].Split(spliter, option);

            for (int j = 0; j < w; j++)
            {
                sdata[i, j] = splitedData[j];
            }
        }

        // 確認表示用の変数(行数、列数)を格納する
        this.height = h;    //行数   
        this.width = w;    //列数
        
    }

}
