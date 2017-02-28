using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour 
{
	public string[] scenarios;	 // シナリオを格納する
	[SerializeField] Text uiText;	// uiTextへの参照を保つ

	public Image idle;
	public Image speak;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;	// 1文字の表示にかかる時間

	private int currentLine = 0;			// 現在の行番号
	private string currentText = string.Empty;	// 現在の文字列
	private float timeUntilDisplay = 0;		// 表示にかかる時間
	private float timeElapsed = 1;			// 文字列の表示を開始した時間
	private int lastUpdateCharacter = -1;		// 表示中の文字数



	void Start()
	{
		SetNextLine();

		idle.enabled = true;
		speak.enabled = false;

	}

	void Update () 
	{
		// 現在の行番号がラストまで行ってない状態でクリックすると、テキストを更新する
		if(currentLine < scenarios.Length && Input.anyKeyDown){
			SetNextLine();
		}

		// クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

		// 表示文字数が前回の表示文字数と異なるならテキストを更新する
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}



		switch (currentLine)
		{
		case 1:
			//Debug.Log ("case1");
			idle.enabled = true;
			speak.enabled = false;
			break;

		case 2:
			//Debug.Log ("case2");
			idle.enabled = false;
			speak.enabled = true;
			break;

		default:
			//Debug.Log ("default");
			break;
		}


	}


	// テキストを更新する
	void SetNextLine()
	{
		// 現在の行のテキストをuiTextに流し込み、現在の行番号を一つ追加する
		currentText = scenarios[currentLine];
		currentLine ++;

		// 想定表示時間と現在の時刻をキャッシュ
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;

		// 文字カウントを初期化
		lastUpdateCharacter = -1;
	}
}