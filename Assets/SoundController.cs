using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class SoundController : MonoBehaviour {

	/// <summary>
	/// 使用するBGMリスト
	/// </summary>
	public List<BGMSelectItem> BGMList = new List<BGMSelectItem>();
	/// <summary>
	/// オーディオソース。
	/// </summary>
	public AudioSource source;
	/// <summary>
	/// BGMの設定ボリューム。自動的にこの値までフェードする
	/// </summary>
	public float MasterVolume = 0.5f;

	/// <summary>
	/// シーンごとのBGMを取得する
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	private AudioClip this[string s]
	{
		get
		{
			return BGMList
				.Where(a => a.key == s)
				.Select(b=>b.clip)
				.FirstOrDefault();
		}
	}

	void Awake()
	{        
		DontDestroyOnLoad(this);
	}
	/// <summary>
	/// シーン切り替え時に呼ばれる。BGMをここで切り替える
	/// </summary>
	/// <param name="level"></param>
	/// <returns></returns>
	IEnumerator OnLevelWasLoaded(int level)
	{
		//フェードアウト
		while (Fade(-0.2f*0.1f))
		{
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(1);
		source.clip = this[SceneManager.GetActiveScene().name];            
		source.Play(1);
		while (Fade(+0.2f * 0.1f))
		{
			yield return new WaitForSeconds(0.1f);
		}

		yield break;
	}

	/// <summary>
	/// 自動的にマスタボリュームまでフェードイン／フェードアウトする
	/// </summary>
	/// <param name="gain"></param>
	/// <returns></returns>
	private bool Fade(float gain)
	{
		if (this.source.volume + gain <= 0 || this.MasterVolume <= this.source.volume +gain)
		{
			return false;
		}

		this.source.volume += gain;
		return true;
	}

}

[Serializable]
public struct BGMSelectItem
{
	public string key;
	public AudioClip clip;
}