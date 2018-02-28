using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Net.Kixmate.KixUtility;

public class DBManager
{
	public string updateDate;
	private string[] header;
	private List<string[]> csvDatas = new List<string[]> ();
	private int index = 0;

	// Debug用
	public DBManager ()
	{
		
	}

	public DBManager (string updateDate)
	{
		// TODO updateDateを元にデータを更新

		// TODO DBの最終更新日時をセットする
		this.updateDate = DateTime.Now.ToString ();
	}

	public void Initialize ()
	{
		// DBへの接続作成
	}

	public Card DebugGetFirstData ()
	{
		Card card = new Card ();


		var csvFile = Resources.Load ("Cards") as TextAsset; /* Resouces/CSV下のCSV読み込み */
		StringReader reader = new StringReader (csvFile.text);

		string line = reader.ReadLine ();
		header = line.Split (',');
		KX.D (true, "header = {0}", string.Join (",", header));
		while (reader.Peek () > -1) {
			line = reader.ReadLine ();
			csvDatas.Add (line.Split (','));
		}

		//card = csv2Card(csvDatas[0]);
		card = csv2Card (csvDatas [1]);

		return card;
	}

	public Card Next ()
	{
		index++;
		if (index > csvDatas.Count - 1) {
			index = 0;
		}

		return csv2Card (csvDatas [index]);
	}

	public Card Prev ()
	{
		index--;
		if (index < 0) {
			index = csvDatas.Count - 1;
		}

		return csv2Card (csvDatas [index]);
	}





	private Card csv2Card (string[] v)
	{
		var card = new Card ();
		card.No = int.Parse (v [0]);
		card.Chinese = v [1];
		card.Pinyin = v [2];
		card.Japanese = v [3];
		card.Tags = v [4];
		string pattern = @"(\S)+ \(([^\)]+)\)";
		Regex regex = new Regex (pattern);
		var match = regex.Match (v [5]);
		if (match.Success) {
			card.DescriptionImagePath = match.Groups [2].Value;
		} else {
			card.DescriptionImagePath = "";
		}
		card.DescriptionText = v [6].Trim('"');

		return card;
	}

	//	public string GetUpdatedJson(string updateDate){
	//
	//	}
}

