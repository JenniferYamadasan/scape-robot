using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using NCMB;
using DG.Tweening;

//private InputField UserName;
//private InputField PassWord;

public class Loginsignin : MonoBehaviour
{
	public InputField UserName;
	public InputField PassWord;
    public GameObject Debug_Text = null; // Textオブジェクト

	[SerializeField] GameObject textManager;

	[SerializeField] Logout logout;
	[SerializeField] ScoreSave scoreSave;
	[SerializeField] PlayerDestroyCounter playerDestroyCounter;

	bool isRankUpdated = false;

	[SerializeField] float DurationSeconds;
	[SerializeField] Ease EaseType;

	[SerializeField] CanvasGroup canvasGroup;
	[SerializeField] Text attentionText;

	[SerializeField] int loopNum;

	[SerializeField] GameObject rankingUI;
	// Use this for initialization
	void Start ()
	{
		isRankUpdated = false;
		rankingUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Show_Hide(bool result)
    {
		textManager.SetActive(result);
    }
	public void Login ()
	{
		print (UserName.text);
		print (PassWord.text);

		//NCMBUserのインスタンス作成 
		NCMBUser user = new NCMBUser ();
        // オブジェクトからTextコンポーネントを取得
        Text debug_text = Debug_Text.GetComponent<Text>();

        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync (UserName.text, PassWord.text, (NCMBException e) => {
			canvasGroup.alpha = 1;
			if (e != null) {
				UnityEngine.Debug.Log ("ログインに失敗: " + e.ErrorMessage);

				printError("ログインに失敗: " + e.ErrorMessage);
			}
            else {
				UnityEngine.Debug.Log ("ログインに成功！");
				rankingUI.SetActive(true);
				printError("ログインに成功");
				logout.LogOutUI(true);
				Show_Hide(false);
				if(!isRankUpdated)
				scoreSave.SaveScore(playerDestroyCounter.GetDeathCount(), UserName.text);
				isRankUpdated = true;
				//LogOutの部分は移動d先のScene名
				//SceneManager.LoadScene ("LogOut");
			}
		});

	}

	public void Signin ()
	{
		print (UserName.text);
		print (PassWord.text);

		//NCMBUserのインスタンス作成 
		NCMBUser user = new NCMBUser ();
		
		
		//ユーザ名とパスワードの設定
		user.UserName = UserName.text;
		user.Password = PassWord.text;
        // オブジェクトからTextコンポーネントを取得
        Text debug_text = Debug_Text.GetComponent<Text>();
        //会員登録を行う
        user.SignUpAsync ((NCMBException e) => {
			canvasGroup.alpha = 1;
			if (e != null) {
				printError("新規登録に失敗: " + e.ErrorMessage);
            }
            else {
				printError("新規登録に成功");
            }
		});

	}

	/// <summary>
	/// エラー内容を出力
	/// </summary>
	/// <param name="error"></param>
	public void printError(string error)
    {
		attentionText.text = error;
		this.canvasGroup.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(loopNum, LoopType.Yoyo);
	}
}
