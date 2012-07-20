using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// カードを生成するクラス
	/// </summary>
	public class CardFactory
	{
		/// <summary>
		/// カードを指定枚数だけ出力する
		/// </summary>
		/// <param name="env">環境設定</param>
		/// <returns>獲得できたカード</returns>
		public static Card[] CreateCards(GatyaEnvironment env)
		{
			Card[] cards = new Card[env.OutCount];	// 出力枚数だけ生成する

			for (int i = 0; i < cards.Length; i++)
				cards[i] = NewCard(env);

			// 先に登録を行っておく?
			// ちょっと汚いかも
			env.Database.EntryCards(cards);
			return cards;
		}

		/// <summary>
		/// カードを1枚獲得する
		/// </summary>
		/// <param name="env">環境設定</param>
		/// <returns></returns>
		private static Card NewCard(GatyaEnvironment env)
		{
			uint number = 0;
			CardRank rarerity = CardRank.Common;

			// ある確率以上なら無条件でレアがヒットする
			if (Random.RandomFloat() < env.RarePercent)
			{
				HitRare(env, out number, out rarerity);
			}
			else
			{
				// レアカードがヒットしなかった！
				number = Random.RandomIndex(env.CommonCount);
				rarerity = CardRank.Common;
			}

			return new Card(number, rarerity);
		}

		/// <summary>
		/// レアカードが獲得できた場合の処理
		/// </summary>
		/// <param name="env">環境設定</param>
		/// <param name="number">インデックス</param>
		/// <param name="rarerity">レアリティ</param>
		private static void HitRare(GatyaEnvironment env, out uint number, out CardRank rarerity)
		{
			uint result_num = Random.RandomIndex(env.CompleteCount);

			// 所持していないカードがヒットした場合、確変チェックを入れる
			if (env.Database.Rare[result_num] == 0)
			{
				if (env.Database.HitCount > env.StartUnhitCount)	// ここで確変チェック
				{
					// 所持していないカードだったため、獲得できるか挑戦する
					if (Random.RandomFloat() < env.UnhitPercent)
					{
						// 獲得成功！
						number = result_num;
						rarerity = CardRank.Rare;
					}
					else
					{
						// 失敗したので適当に所持している中から選ぶ
						uint[] hitted = env.Database.HittedRareIndices;
						number = Random.RandomIndex((uint)hitted.Length);
						rarerity = CardRank.Rare;
					}
				}
				else
				{
					// 確変がまだ起きてないので通常通り
					number = result_num;
					rarerity = CardRank.Rare;
				}
			}
			else
			{
				// 所持しているカードがヒットしたので素通りする
				number = result_num;
				rarerity = CardRank.Rare;
			}
		}
	}
}
