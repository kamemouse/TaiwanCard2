using System;
//using AirtableApiClient;

public class Card
{
	public int No;
	public string Chinese;
	public string Pinyin;
	public string Japanese;
	public string Tags;
	public string DescriptionImagePath;
//	public AirtableAttachment DescriptionImage;
	public string DescriptionText;

	//	public string imageUrl;
	//	public string[] tagList;

	public static Card DummyData ()
	{
		var dummy = new Card () {
			No = 1,
			Chinese = "運動",
			Pinyin = "yùn dòng",
			Japanese = "運動、スポーツ",
//			Tags = "台湾華語(中国語繁体字),運動,日常,ダイエット",
			Tags = "台湾華語(中国語繁体字)",
			DescriptionImagePath = "movie.jpg",
//			DescriptionImage = null,
			DescriptionText = "我昨天在健身房運動了。\n昨日ジムで運動しました。"
		};

		return dummy;
	}
}

