using System;

public class DBManager
{
	public string updateDate;
	public DBManager (string updateDate)
	{
		// TODO updateDateを元にデータを更新

		// TODO DBの最終更新日時をセットする
		this.updateDate = DateTime.Now.ToString();
	}

	public void Initialize(){
		// 
	}

//	public string GetUpdatedJson(string updateDate){
//		
//	}
}

