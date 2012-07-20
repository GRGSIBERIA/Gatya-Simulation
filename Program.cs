using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GatyaSimulation.Gatya;

namespace GatyaSimulation
{
	class Program
	{
		static void Main(string[] args)
		{
			uint カード枚数 = 72;
			uint 値段 = 300;
			uint 引けるカード枚数 = 5;
			uint コンプ枚数 = 8;
			double レア確率 = 0.05;
			uint 再抽選が始まるコンプ枚数 = 3;
			double 再抽選時の当選確率 = 1;

			uint シミュレーション人数 = 100;

			List<UserInfomation> info = new List<UserInfomation>();
			GatyaEnvironment env = new GatyaEnvironment(カード枚数, 引けるカード枚数, (float)レア確率, コンプ枚数, 再抽選が始まるコンプ枚数, (float)再抽選時の当選確率);
			GatyaMachine mac = new GatyaMachine(env, 値段);

			Completely(mac, info, シミュレーション人数); // 多人数のシミュレーションができる
			//Person(mac);		// 個人に焦点を当てたシミュレーション

			
			Console.WriteLine("{0}回ガチャが回されました", mac.InTryCount);
			Console.WriteLine("ガチャには{0}円入ってます", mac.InMoney);
			Console.WriteLine("全体で引かれたカードは{0}枚です", mac.InCardCount);
		}

		static void Person(GatyaMachine mac)
		{
			while (!mac.IsComplete)
			{
				var cards = mac.Try();
				foreach (var c in cards)
					Console.WriteLine("{0} : {1}", c.Rarerity, c.Number);
				Console.WriteLine("--------------");
			}
		}

		static void Completely(GatyaMachine mac, List<UserInfomation> info, uint simnum)
		{
			// コンプするまでループ
			for (int i = 0; i < simnum; i++)
			{
				Console.WriteLine("挑戦者番号{0}", i + 1);
				var inf = mac.TryCompletely();
				inf.Dump();
				info.Add(inf);
			}

			Console.WriteLine("*********************************");

			int min_cnt = 0;
			for (int i = 0; i < info.Count; i++)
				if (info[min_cnt].TotalCost > info[i].TotalCost) min_cnt = i;
			Console.WriteLine("もっとも運が良かった人の結果は、");
			info[min_cnt].Dump();

			int max_cnt = 0;
			for (int i = 0; i < info.Count; i++)
				if (info[max_cnt].TotalCost < info[i].TotalCost) max_cnt = i;
			Console.WriteLine("もっとも運が悪かった人の結果は、");
			info[max_cnt].Dump();

			double dif = Math.Log10( (double)(info[max_cnt].TotalCost - info[min_cnt].TotalCost) );
			//Console.WriteLine("格差指数は{0}です", dif);

			Console.WriteLine("*********************************");

			Console.WriteLine("コンプしたい人は{0}人いました", info.Count);
		}
	}
}
