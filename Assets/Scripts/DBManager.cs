using System;

public class DBManager
{
	public string updateDate;

	// Debug用
	public DBManager()
	{
		
	}

	public DBManager (string updateDate)
	{
		// TODO updateDateを元にデータを更新

		// TODO DBの最終更新日時をセットする
		this.updateDate = DateTime.Now.ToString();
	}

	public void Initialize(){
		// 
	}

	public Card DebugGetFirstData(){
		Card card = new Card();


		return card;
	}

//	public string GetUpdatedJson(string updateDate){
//		
//	}
}

