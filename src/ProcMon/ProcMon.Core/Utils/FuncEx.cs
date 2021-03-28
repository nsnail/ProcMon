using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable UnusedMember.Global

namespace ProcMon.Core.Utils
{
	public static class FuncEx
	{
		/// <summary>
		///     36进制码表
		/// </summary>
		private const string SYS36_TABLE = "0123456789abcdefghijklmnopqrstuvwxyz";

		private const string USUAL_CHARS =
			" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,，.。?？!！;；/（）()一乙二十丁厂七卜八人入儿九几了乃刀力又三干于亏士土工才下寸丈大与万上小口山巾千乞川亿个么久勺丸夕凡及广亡门义之尸已弓己卫子也女飞刃习叉马乡丰王井开夫天元无云专扎艺木五支厅不太犬区历友尤匹车巨牙屯比互切瓦止少日中贝内水冈见手午牛毛气升长仁什片仆化仇币仍仅斤爪反介父从今凶分乏公仓月氏勿风欠丹匀乌勾凤六文方火为斗忆计订户认心尺引丑巴孔队办以允予劝双书幻玉刊末未示击打巧正扑扒功扔去甘世古节本术可丙左厉石右布龙平灭轧东卡北占业旧帅归目旦且叮叶甲申号电田由只央史兄叼叫叨另叹四生失禾丘付仗代仙们仪白仔他斥瓜乎丛令用甩印乐句匆册犯外处冬鸟务包饥主市立闪兰半汁汇头汉宁穴它讨写让礼训必议讯记永司尼民出辽奶奴加召皮边孕发圣对台矛纠母幼丝式刑动扛寺吉扣考托老圾巩执扩扫地扬场耳共芒亚芝朽朴机权过臣再协西压厌在百有存而页匠夸夺灰达列死成夹轨邪划迈毕至此贞师尘尖劣光当早吐吓虫曲团同吊吃因吸吗屿帆岁回岂则刚网肉年朱先丢舌竹迁乔伟传乒乓休伍伏优伐延件任伤价份华仰仿伙伪自血向似后行舟全会杀合兆企众爷伞创肌朵杂危旬旨负各名多争色壮冲冰庄庆亦刘齐交次衣产决充妄闭问闯羊并关米灯州汗污江池汤忙兴宇守宅字安讲军许论农讽设访寻那迅尽导异孙阵阳收阶阴防奸如妇好她妈戏羽观欢买红纤约级纪驰巡寿弄麦形进戒吞远违运扶抚坛技坏扰拒找批扯址走抄坝贡攻赤折抓扮抢孝均抛投坟坑抗坊抖护壳志块扭声把报却劫芽花芹芬苍芳严芦劳克苏杆杜杠材村杏极李杨求更束豆两丽医辰励否还歼来连步坚旱盯呈时吴助县里呆园旷围呀吨足邮男困吵串员听吩吹呜吼吧别岗帐财钉针告我乱利秃秀私每兵估体何但伸作伯伶佣低你住位伴身皂佛近彻役返余希坐谷妥含邻岔肝肚肠龟免狂犹角删条卵岛迎饭饮系言冻状亩况床库疗应冷这序辛弃冶忘闲间闷判灶灿弟汪沙汽沃泛沟没沈沉怀忧快完宋宏牢究穷灾良证启评补初社识诉诊词译君灵即层尿尾迟局改张忌际陆阿陈阻附妙妖妨努忍劲鸡驱纯纱纲纳纵驳纷纸纹纺驴纽奉玩环武青责现表规抹拢拔拣坦担押抽拐拖者拍顶拆拥抵拘势抱垃拉拦幸拌招坡披拨择抬其取苦若茂苹苗英范直茄茎茅林枝杯柜析板松枪构杰述枕丧或画卧事刺枣雨卖矿码厕奔奇奋态欧垄妻轰顷转斩轮软到非叔肯齿些虎虏肾贤尚旺具果味昆国昌畅明易昂典固忠咐呼鸣咏呢岸岩帖罗帜岭凯败贩购图钓制知垂牧物乖刮秆和季委佳侍供使例版侄侦侧凭侨佩货依的迫质欣征往爬彼径所舍金命斧爸采受乳贪念贫肤肺肢肿胀朋股肥服胁周昏鱼兔狐忽狗备饰饱饲变京享店夜庙府底剂郊废净盲放刻育闸闹郑券卷单炒炊炕炎炉沫浅法泄河沾泪油泊沿泡注泻泳泥沸波泼泽治怖性怕怜怪学宝宗定宜审宙官空帘实试郎诗肩房诚衬衫视话诞询该详建肃隶录居届刷屈弦承孟孤陕降限妹姑姐姓始驾参艰线练组细驶织终驻驼绍经贯奏春帮珍玻毒型挂封持项垮挎城挠政赴赵挡挺括拴拾挑指垫挣挤拼挖按挥挪某甚革荐巷带草茧茶荒茫荡荣故胡南药标枯柄栋相查柏柳柱柿栏树要咸威歪研砖厘厚砌砍面耐耍牵残殃轻鸦皆背战点临览竖省削尝是盼眨哄哑显冒映星昨畏趴胃贵界虹虾蚁思蚂虽品咽骂哗咱响哈咬咳哪炭峡罚贱贴骨钞钟钢钥钩卸缸拜看矩怎牲选适秒香种秋科重复竿段便俩货顺修保促侮俭俗俘信皇泉鬼侵追俊盾待律很须叙剑逃食盆胆胜胞胖脉勉狭狮独狡狱狠贸怨急饶蚀饺饼弯将奖哀亭亮度迹庭疮疯疫疤姿亲音帝施闻阀阁差养美姜叛送类迷前首逆总炼炸炮烂剃洁洪洒浇浊洞测洗活派洽染济洋洲浑浓津恒恢恰恼恨举觉宣室宫宪突穿窃客冠语扁袄祖神祝误诱说诵垦退既屋昼费陡眉孩除险院娃姥姨姻娇怒架贺盈勇怠柔垒绑绒结绕骄绘给络骆绝绞统耕耗艳泰珠班素蚕顽盏匪捞栽捕振载赶起盐捎捏埋捉捆捐损都哲逝捡换挽热恐壶挨耻耽恭莲莫荷获晋恶真框桂档桐株桥桃格校核样根索哥速逗栗配翅辱唇夏础破原套逐烈殊顾轿较顿毙致柴桌虑监紧党晒眠晓鸭晃晌晕蚊哨哭恩唤啊唉罢峰圆贼贿钱钳钻铁铃铅缺氧特牺造乘敌秤租秧积秩称秘透笔笑笋债借值倚倾倒倘俱倡候俯倍倦健臭射躬息徒徐舰舱般航途拿爹爱颂翁脆脂胸胳脏胶脑狸狼逢留皱饿恋桨浆衰高席准座症病疾疼疲脊效离唐资凉站剖竞部旁旅畜阅羞瓶拳粉料益兼烤烘烦烧烛烟递涛浙涝酒涉消浩海涂浴浮流润浪浸涨烫涌悟悄悔悦害宽家宵宴宾窄容宰案请朗诸读扇袜袖袍被祥课谁调冤谅谈谊剥恳展剧屑弱陵陶陷陪娱娘通能难预桑绢绣验继球理捧堵描域掩捷排掉推堆掀授教掏掠培接控探据掘职基著勒黄萌萝菌菜萄菊萍菠营械梦梢梅检梳梯桶救副票戚爽聋袭盛雪辅辆虚雀堂常匙晨睁眯眼悬野啦晚啄距跃略蛇累唱患唯崖崭崇圈铜铲银甜梨犁移笨笼笛符第敏做袋悠偿偶偷您售停偏假得衔盘船斜盒鸽悉欲彩领脚脖脸脱象够猜猪猎猫猛馅馆凑减毫麻痒痕廊康庸鹿盗章竟商族旋望率着盖粘粗粒断剪兽清添淋淹渠渐混渔淘液淡深婆梁渗情惜惭悼惧惕惊惨惯寇寄宿窑密谋谎祸谜逮敢屠弹随蛋隆隐婚婶颈绩绪续骑绳维绵绸绿琴斑替款堪塔搭越趁趋超提堤博揭喜插揪搜煮援裁搁搂搅握揉斯期欺联散惹葬葛董葡敬葱落朝辜葵棒棋植森椅椒棵棍棉棚棕惠惑逼厨厦硬确雁殖裂雄暂雅辈悲紫辉敞赏掌晴暑最量喷晶喇遇喊景践跌跑遗蛙蛛蜓喝喂喘喉幅帽赌赔黑铸铺链销锁锄锅锈锋锐短智毯鹅剩稍程稀税筐等筑策筛筒答筋筝傲傅牌堡集焦傍储奥街惩御循艇舒番释禽腊脾腔鲁猾猴然馋装蛮就痛童阔善羡普粪尊道曾焰港湖渣湿温渴滑湾渡游滋溉愤慌惰愧愉慨割寒富窜窝窗遍裕裤裙谢谣谦属屡强粥疏隔隙絮嫂登缎缓骗编缘瑞魂肆摄摸填搏塌鼓摆携搬摇搞塘摊蒜勤鹊蓝墓幕蓬蓄蒙蒸献禁楚想槐榆楼概赖酬感碍碑碎碰碗碌雷零雾雹输督龄鉴睛睡睬鄙愚暖盟歇暗照跨跳跪路跟遣蛾蜂嗓置罪罩错锡锣锤锦键锯矮辞稠愁筹签简毁舅鼠催傻像躲微愈遥腰腥腹腾腿触解酱痰廉新韵意粮数煎塑慈煤煌满漠源滤滥滔溪溜滚滨粱滩慎誉塞谨福群殿辟障嫌嫁叠缝缠静碧璃墙嘉摧截誓境摘摔撇聚慕暮蔑蔽模榴榜榨歌遭酷酿酸磁愿需裳颗嗽蜻蜡蝇蜘赚锹锻舞稳算箩管僚鼻魄貌膜膊膀鲜疑馒裹敲豪膏遮腐瘦辣竭端旗精歉弊熄熔漆漂漫滴演漏慢寨赛察蜜谱嫩翠熊凳骡缩慧撕撒趣趟撑播撞撤增聪鞋蕉蔬横槽樱橡飘醋醉震霉瞒题暴瞎影踢踏踩踪蝶蝴嘱墨镇靠稻黎稿稼箱箭篇僵躺僻德艘膝膛熟摩颜毅糊遵潜潮懂额慰劈操燕薯薪薄颠橘整融醒餐嘴蹄器赠默镜赞篮邀衡膨雕磨凝辨辩糖糕燃澡激懒壁避缴戴擦鞠藏霜霞瞧蹈螺穗繁辫赢糟糠燥臂翼骤鞭覆蹦镰翻鹰警攀蹲颤瓣爆疆壤耀躁嚼嚷籍魔灌蠢霸露囊";

		/// <summary>
		///     base64编码
		/// </summary>
		/// <param name="me">待编码的字节数组</param>
		/// <returns>编码后的base64字符串</returns>
		public static string Base64(this byte[] me)
		{
			return Convert.ToBase64String(me);
		}

		/// <summary>
		///     base64编码
		/// </summary>
		/// <param name="me">待base64编码的字符串</param>
		/// <param name="e">字符串的编码方式</param>
		/// <returns>编码后的base64字符串</returns>
		public static string Base64(this string me, Encoding e)
		{
			return Base64(e.GetBytes(me));
		}

		/// <summary>
		///     base64解码
		/// </summary>
		/// <param name="me">待解码的字符串</param>
		/// <returns>解码后的原始字节数组</returns>
		public static byte[] Base64De(this string me)
		{
			return Convert.FromBase64String(me);
		}

		/// <summary>
		///     base64解码
		/// </summary>
		/// <param name="me">待解码的字符串</param>
		/// <param name="e">字符串的编码方式</param>
		/// <returns>解码后的原始字符串</returns>
		public static string Base64De(this string me, Encoding e)
		{
			return e.GetString(Base64De(me));
		}

		public static string Base64Sys(this string me)
		{
			return me.Replace("-", "+").Replace("_", "/").Replace(".", "=");
		}

		public static string Base64Web(this string me)
		{
			return me.Replace("+", "-").Replace("/", "_").Replace("=", ".");
		}

		/// <summary>
		///     string to bool
		/// </summary>
		/// <param name="me">string</param>
		/// <returns>bool</returns>
		public static bool Boolean(this string me)
		{
			return bool.Parse(me);
		}

		/// <summary>
		///     尝试将字符串转为bool
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="def">转换失败后返回的默认值</param>
		/// <returns>转换后的bool</returns>
		public static bool BooleanTry(this string me, bool def)
		{
			return !bool.TryParse(me, out var ret) ? def : ret;
		}

		/// <summary>
		///     从指定的对象拷贝属性
		/// </summary>
		/// <typeparam name="TT">对象类型</typeparam>
		/// <param name="me">拷贝目标</param>
		/// <param name="copyObj">拷贝来源</param>
		/// <param name="propNameList">需要处理的属性名</param>
		/// <param name="isIncludeOrExclude">True包含，false排除</param>
		public static void CopyFrom<TT>(this TT me, TT copyObj, IList<string> propNameList = null,
			bool isIncludeOrExclude = false)
		{
			foreach (var p in me.GetType().GetProperties()) {
				if (!p.CanWrite) continue;
				bool isSet;
				if (isIncludeOrExclude)
					isSet = propNameList?.Contains(p.Name) ?? false;
				else
					isSet = !propNameList?.Contains(p.Name) ?? true;

				if (isSet) p.SetValue(me, copyObj.GetType().GetProperty(p.Name)?.GetValue(copyObj, null), null);
			}
		}


		public static DateTime DateTime(this string me)
		{
			return System.DateTime.Parse(me, CultureInfo.CurrentCulture);
		}

		public static DateTime DateTimeTry(this string me, DateTime def)
		{
			return !System.DateTime.TryParse(me, out var ret) ? def : ret;
		}

		/// <summary>
		///     转半角的函数(DBC case)
		///     任意字符串
		///     半角字符串
		///     全角空格为12288，半角空格为32
		///     其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Dbc(this string input)
		{
			var c = input.ToCharArray();
			for (var i = 0; i < c.Length; i++) {
				if (c[i] == 12288) {
					c[i] = (char)32;
					continue;
				}

				if (c[i] > 65280 && c[i] < 65375) c[i] = (char)(c[i] - 65248);
			}

			return new string(c);
		}

		/// <summary>
		///     string to decimal
		/// </summary>
		/// <param name="me">string</param>
		/// <returns>decimal</returns>
		public static decimal Dec(this string me)
		{
			return decimal.Parse(me, CultureInfo.CurrentCulture);
		}

		/// <summary>
		///     long to decimal
		/// </summary>
		/// <param name="me">long</param>
		/// <returns>decimal</returns>
		public static decimal Dec(this long me)
		{
			return me;
		}

		/// <summary>
		///     尝试将字符串转为decimal
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="def">转换失败后返回的默认值</param>
		/// <returns>转换后的decimal</returns>
		public static decimal DecTry(this string me, decimal def)
		{
			return !decimal.TryParse(me, out var ret) ? def : ret;
		}

		/// <summary>
		///     获取枚举的description属性
		/// </summary>
		/// <param name="e">枚举对象</param>
		/// <returns>description属性</returns>
		public static string Desc(this Enum e)
		{
			var t = e.GetType();
			var fi = t.GetField(System.Enum.GetName(t, e) ?? string.Empty);
			var attrs = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return (attrs.Length != 0 ? attrs[0].Description : System.Enum.GetName(t, e)) ?? "";
		}


		/// <summary>
		///     判断对象是否为null或不存在子元素（如果为集合对象）
		/// </summary>
		/// <typeparam name="TT">对象类型</typeparam>
		/// <param name="me">指定对象</param>
		/// <returns>空则返回true</returns>
		public static bool Empty<TT>(this IEnumerable<TT> me)
		{
			return me == null || !me.Any();
		}

		/// <summary>
		///     判断对象是否为null或不存在子元素（如果为集合对象）；如果为空，返回指定的默认值，否则返回对象本身
		/// </summary>
		/// <typeparam name="TT">对象类型</typeparam>
		/// <param name="me">指定对象</param>
		/// <param name="defVal">指定的默认值</param>
		/// <returns>如果为空，返回指定的默认值，否则返回对象本身</returns>
		public static IEnumerable<TT> Empty<TT>(this IEnumerable<TT> me, IEnumerable<TT> defVal)
		{
			// ReSharper disable PossibleMultipleEnumeration
			return Empty(me) ? defVal : me;
			// ReSharper restore PossibleMultipleEnumeration
		}

		/// <summary>
		///     判断字符串是否为null或不存在子元素（如果为集合对象）；如果为空，返回指定的默认值，否则返回字符串本身
		/// </summary>
		/// <param name="me">指定字符串</param>
		/// <param name="defVal">指定的默认值</param>
		/// <returns>如果为空，返回指定的默认值，否则返回字符串本身</returns>
		public static string Empty(this string me, string defVal)
		{
			return Empty(me) ? defVal : me;
		}


		public static T Enum<T>(this string name) where T : Enum
		{
			return (T)System.Enum.Parse(typeof(T), name);
		}


		/// <summary>
		///     将字符串转为guid
		/// </summary>
		/// <param name="me">字符串</param>
		/// <returns>guid</returns>
		public static Guid Guid(this string me)
		{
			return System.Guid.Parse(me);
		}

		public static Guid Guid(this string me, Guid def)
		{
			return System.Guid.TryParse(me, out var ret) ? ret : def;
		}

		public static string GzipDe(this Stream me, Encoding enc)
		{
			using var ms = new MemoryStream();
			using var gs = new GZipStream(me, CompressionMode.Decompress);
			using var sr = new StreamReader(ms, enc);
			gs.CopyTo(ms);
			ms.Seek(0, SeekOrigin.Begin);
			return sr.ReadToEnd();
		}

		public static bool HasFlag<TT>(this int me, TT flag) where TT : Enum
		{
			return HasFlag((long)me, flag);
		}

		public static bool HasFlag<TT>(this long me, TT flag) where TT : Enum
		{
			var val = (long)(object)flag;
			return (me & val) == val;
		}

		/// <summary>
		///     将字符串转换成字节数组形式
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="e">字符串使用的编码</param>
		/// <returns>字节数组</returns>
		public static byte[] Hex(this string me, Encoding e)
		{
			return e.GetBytes(me);
		}

		/// <summary>
		///     将字节数组解码成字符串
		/// </summary>
		/// <param name="me">字节数组</param>
		/// <param name="e">字符串使用的编码方式</param>
		/// <returns>解码后的原始字符串</returns>
		public static string HexDe(this byte[] me, Encoding e)
		{
			return e.GetString(me);
		}

		/// <summary>
		///     解码html编码
		/// </summary>
		/// <param name="me">html编码后的字符串</param>
		/// <returns>解码后的原始字符串</returns>
		public static string HtmlDe(this string me)
		{
			return HttpUtility.HtmlDecode(me);
		}

		/// <summary>
		///     移除字符串中的html标签
		/// </summary>
		/// <param name="me">字符串</param>
		/// <returns>处理之后的字符串</returns>
		public static string HtmlTagRemove(this string me)
		{
			return new Regex(@"<[^>]*>").Replace(me, "");
		}

		/// <summary>
		///     string to Int32
		/// </summary>
		/// <param name="me">string</param>
		/// <returns>Int32</returns>
		public static int Int32(this string me)
		{
			return int.Parse(me, CultureInfo.CurrentCulture);
		}


		/// <summary>
		///     尝试将字符串转为int32
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="def">转换失败后返回的默认值</param>
		/// <returns>转换后的int32</returns>
		public static int Int32Try(this string me, int def)
		{
			return !int.TryParse(me, out var ret) ? def : ret;
		}

		/// <summary>
		///     string to Int64
		/// </summary>
		/// <param name="me">string</param>
		/// <returns>Int64</returns>
		public static long Int64(this string me)
		{
			return long.Parse(me, CultureInfo.CurrentCulture);
		}

		/// <summary>
		///     尝试将字符串转为int64
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="def">转换失败后返回的默认值</param>
		/// <returns>转换后的int64</returns>
		public static long Int64Try(this string me, long def)
		{
			return !long.TryParse(me, out var ret) ? def : ret;
		}

		public static T Is<T>(this T me, T compare, T ret) where T : struct
		{
			return me.Equals(compare) ? ret : me;
		}

		/// <summary>
		///     将一个json字符串反序列化成为jarray对象
		/// </summary>
		/// <param name="me">字符串</param>
		/// <returns></returns>
		public static JArray JArr(this string me)
		{
			return JArray.Parse(me);
		}


		/// <summary>
		///     将一个json字符串反序列化成为jobject对象
		/// </summary>
		/// <param name="me">字符串</param>
		/// <returns></returns>
		public static JObject JObj(this string me)
		{
			return JObject.Parse(me);
		}


		public static string Join(this IEnumerable<object> me, string separator)
		{
			return string.Join(separator, me);
		}

		/// <summary>
		///     将一个对象序列化成json文本
		/// </summary>
		/// <param name="me">指定对象</param>
		/// <param name="format">是否格式化</param>
		/// <returns>json文本</returns>
		public static string Json(this object me, bool format = false)
		{
			try {
				return JObject.FromObject(me).ToString(format ? Formatting.Indented : Formatting.None);
			} catch (ArgumentException exp) when (exp.Message.Contains("Object serialized to Array")) {
				return JArray.FromObject(me).ToString(format ? Formatting.Indented : Formatting.None);
			}
		}

		/// <summary>
		///     只保留常用字符
		/// </summary>
		/// <param name="me"></param>
		/// <returns></returns>
		public static string KeepUsualChar(this string me)
		{
			return new Regex("[^" + USUAL_CHARS + "]").Replace(me, "");
		}

		/// <summary>
		///     对一个email进行掩码处理
		/// </summary>
		/// <param name="me"></param>
		/// <returns></returns>
		public static string MaskEmail(this string me)
		{
			return me.Substring(0, me.IndexOf('@')).MaskNick() + me.Substring(me.IndexOf('@'));
		}

		/// <summary>
		///     对一个手机号进行掩码处理
		/// </summary>
		/// <param name="me">手机号</param>
		/// <returns>掩码后的手机号</returns>
		public static string MaskMobile(this string me)
		{
			return new Regex(@"^(\d{3})\d{4}(\d{4})$").Replace(me, "$1****$2");
		}

		/// <summary>
		///     对一个字符串进行md5hash运算
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="e">字符串使用的编码</param>
		/// <returns>hash摘要的16进制文本形式（无连字符小写）</returns>
		public static string Md5(this string me, Encoding e)
		{
			using var md5 = new MD5CryptoServiceProvider();
			return BitConverter.ToString(md5.ComputeHash(e.GetBytes(me))).Replace("-", string.Empty)
				.ToLower(CultureInfo.CurrentCulture);
		}

		/// <summary>
		///     对一个字符串进行md5hash运算
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="e">字符串使用的编码</param>
		/// <returns>hash摘要的字节数组</returns>
		public static byte[] Md5Bytes(this string me, Encoding e)
		{
			using var md5 = new MD5CryptoServiceProvider();
			return md5.ComputeHash(e.GetBytes(me));
		}

		/// <summary>
		///     对数字口语化处理，保留1位小数，如12345转化成"1.2万"
		/// </summary>
		/// <param name="me">数字</param>
		/// <returns>处理之后的值</returns>
		public static string NumShort(this decimal me)
		{
			return (me / 10000).Str(1) + "万";
		}

		/// <summary>
		///     保留指定位数的价格，小字体显示小数位
		/// </summary>
		/// <param name="me">价格</param>
		/// <param name="place">保留小数位</param>
		/// <returns>格式化后的字符串</returns>
		public static string PriceStr(this decimal me, int place)
		{
			var s = Str(me, place);
			if (s.Contains(".")) s = s.Replace(".", ".<small>") + "</small>";

			return s;
		}

		/// <summary>
		///     生成密码
		/// </summary>
		/// <param name="me">密码原文</param>
		/// <returns>密文</returns>
		public static string Pwd(this string me)
		{
			return me.Md5Hmac(me.Md5(Encoding.UTF8), Encoding.UTF8);
		}

		public static int Rand(this int[] me)
		{
			return new Random(System.Guid.NewGuid().GetHashCode()).Next(me[0], me[1]);
		}

		public static string RemoveScheme(this Uri me)
		{
			return "//" + me.Authority + me.PathAndQuery;
		}


		public static string RemoveWrapped(this string me)
		{
			return me.Replace("\r", "").Replace("\n", "");
		}

		/// <summary>
		///     四舍五入后的近似值
		/// </summary>
		/// <param name="me">数字</param>
		/// <param name="place">小数点位数</param>
		/// <returns>处理后的值</returns>
		public static decimal Round(this decimal me, int place)
		{
			var dec = Math.Round(me, place);
			return dec;
		}

		/// <summary>
		///     转全角的函数(Sbc case)
		///     任意字符串
		///     全角字符串
		///     全角空格为12288，半角空格为32
		///     其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
		/// </summary>
		/// <returns></returns>
		public static string Sbc(this string me)
		{
			// 半角转全角：
			var c = me.ToCharArray();
			for (var i = 0; i < c.Length; i++) {
				if (c[i] == 32) {
					c[i] = (char)12288;
					continue;
				}

				if (c[i] < 127) c[i] = (char)(c[i] + 65248);
			}

			return new string(c);
		}


		/// <summary>
		///     二进制序列化一个对象
		/// </summary>
		/// <param name="me">指定对象</param>
		/// <param name="filePath">序列化文件存储路径</param>
		/// <returns>成功返回true</returns>
		public static bool Serialize(this object me, string filePath)
		{
			try {
				var bf = new BinaryFormatter();
				using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
				bf.Serialize(fs, me);
				return true;
			} catch {
				return false;
			}
		}

		/// <summary>
		///     反序列化一个文件获得指定类型的数据对象
		/// </summary>
		/// <typeparam name="TT">指定类型</typeparam>
		/// <param name="me">序列化文件路径</param>
		/// <returns>反序列化生成的对象</returns>
		public static TT SerializeDe<TT>(this string me) where TT : class
		{
			return SerializeDe(me) as TT;
		}

		/// <summary>
		///     json序列化一个对象
		/// </summary>
		/// <param name="me">指定对象</param>
		/// <returns>json文本</returns>
		public static string SerializeJson(this object me)
		{
			return JsonConvert.SerializeObject(me, Formatting.Indented);
		}

		/// <summary>
		///     反序列化一个文件获得指定类型的数据对象
		/// </summary>
		/// <param name="me">等待反序列化的json文本</param>
		/// <returns>反序列化后生成的对象</returns>
		public static TT SerializeJsonDe<TT>(this string me)
		{
			return JsonConvert.DeserializeObject<TT>(me);
		}

		/// <summary>
		///     xml序列化一个对象
		/// </summary>
		/// <param name="me">指定对象</param>
		/// <param name="filePath">序列化文件存储路径</param>
		/// <returns>成功返回true</returns>
		public static bool SerializeXml(this object me, string filePath)
		{
			try {
				var xs = new XmlSerializer(me.GetType());
				using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
				xs.Serialize(fs, me);
				return true;
			} catch {
				return false;
			}
		}

		/// <summary>
		///     反序列化一个文件获得指定类型的数据对象
		/// </summary>
		/// <typeparam name="TT">指定类型</typeparam>
		/// <param name="me">序列化文件路径</param>
		/// <returns>反序列化后生成的对象</returns>
		public static TT SerializeXmlDe<TT>(this string me) where TT : class
		{
			return SerializeXmlDe(me) as TT;
		}


		public static string Sub(this string me, int startIndex, int length)
		{
			if (startIndex + length > me.Length) length = me.Length - startIndex;
			return me.Substring(startIndex, length);
		}

		/// <summary>
		///     对一个10进制数进行36进制编码
		/// </summary>
		/// <param name="me">10进制数</param>
		/// <returns>36进制编码后的数字字符串</returns>
		public static string Sys36(this long me)
		{
			var ret = new StringBuilder();
			while (me > 35) {
				ret.Insert(0, SYS36_TABLE[(int)(me % 36)]);
				me /= 36;
			}

			ret.Insert(0, SYS36_TABLE[(int)me]);
			return ret.ToString();
		}

		/// <summary>
		///     解码36进制数字字符串获得10进制数字
		/// </summary>
		/// <param name="me">36进制数字字符串</param>
		/// <returns>10进制数字</returns>
		public static long Sys36De(this string me)
		{
			var weight = 1L;
			var ret = 0L;
			foreach (var s in me.Reverse()) {
				ret += SYS36_TABLE.IndexOf(s) * weight;
				weight *= 36;
			}

			return ret;
		}

		public static DateTime Time(this long msFrom1970)
		{
			return new DateTime(1970, 1, 1).AddMilliseconds(msFrom1970)
				.ToLocalTime();
		}

		/// <summary>
		///     将一个将来时间对象与当前时间相减转换成“xx”的字符串，如2秒，3天
		/// </summary>
		/// <param name="me">时间对象</param>
		/// <returns>字符串</returns>
		public static string TimeAfter(this DateTime me)
		{
			var ts = me - System.DateTime.Now;
			if (ts.Days > 0) return ts.Days + "天";

			if (ts.Hours > 0) return ts.Hours + "小时";

			if (ts.Minutes > 0) return ts.Minutes + "分钟";

			return ts.Seconds + "秒";
		}

		/// <summary>
		///     将一个过去时间对象与当前时间相减转换成“xx以前”的字符串，如2秒以前，3天以前
		/// </summary>
		/// <param name="me">时间对象</param>
		/// <returns>字符串</returns>
		public static string TimeAgo(this DateTime me)
		{
			var ts = System.DateTime.Now - me;
			if (ts.Days > 0) return ts.Days + "天前";

			if (ts.Hours > 0) return ts.Hours + "小时前";

			if (ts.Minutes > 0) return ts.Minutes + "分钟前";

			return ts.Seconds + "秒前";
		}

		/// <summary>
		///     将一个未来时间与当前时间相减，转换成”xx天xx小时xx分“
		/// </summary>
		/// <param name="me">时间对象</param>
		/// <returns>字符串</returns>
		public static string TimeEnd(this DateTime me)
		{
			var ret = new StringBuilder();
			var ts = me - System.DateTime.Now;
			if (ts.Days > 0) ret.Append(ts.Days + "天");

			if (ts.Hours > 0) ret.Append(ts.Hours + "小时");

			if (ts.Minutes > 0) ret.Append(ts.Minutes + "分");

			return ret.ToString();
		}

		/// <summary>
		///     将unix世界协调时 时间戳转为本地datetime对象
		/// </summary>
		/// <param name="me">unix世界协调时时间戳</param>
		/// <returns>本地datetime对象</returns>
		public static DateTime TimeLocal(this long me)
		{
			return System.DateTime.FromBinary(me * 10000000 + 621355968000000000).ToLocalTime();
		}

		/// <summary>
		///     指定时间的世界协调时的unix时间戳形式
		/// </summary>
		/// <param name="me">指定时间</param>
		/// <returns>unix时间戳</returns>
		public static long TimeUnixUtc(this DateTime me)
		{
			return (me.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
		}

		public static long TimeUnixUtcMs(this DateTime me)
		{
			return (me.ToUniversalTime().Ticks - 621355968000000000) / 10000;
		}

		public static string TrimSpaces(this string me)
		{
			var ret = me.Replace("  ", " ");
			return ret == me ? ret : TrimSpaces(ret);
		}

		public static uint UInt32(this string me)
		{
			return uint.Parse(me, CultureInfo.CurrentCulture);
		}

		/// <summary>
		///     在现有的uri上增加一个参数，如果参数已存在则覆盖
		/// </summary>
		/// <param name="me">uri</param>
		/// <param name="key">参数的键</param>
		/// <param name="val">参数的值</param>
		/// <returns>新的uri</returns>
		public static Uri UriParamAdd(this Uri me, string key, string val)
		{
			return new($"{me}?{key}={val}");
		}


		/// <summary>
		///     url编码
		/// </summary>
		/// <param name="me">字符串</param>
		/// <returns>url编码后的字符串</returns>
		public static string Url(this string me)
		{
			return Uri.EscapeDataString(me);
		}

		/// <summary>
		///     解码url编码
		/// </summary>
		/// <param name="me">url编码后的字符串</param>
		/// <returns>解码后的原始字符串</returns>
		public static string UrlDe(this string me)
		{
			return Uri.UnescapeDataString(me);
		}

		/// <summary>
		///     对一个用户名进行掩码处理
		/// </summary>
		/// <param name="me">用户名</param>
		/// <returns>掩码后的用户名</returns>
		private static string MaskNick(this string me)
		{
			return new Regex(@"^(.).*?(.)$").Replace(me, "$1***$2");
		}

		/// <summary>
		///     MD5 hmac编码
		/// </summary>
		/// <param name="me">字符串</param>
		/// <param name="key">密钥</param>
		/// <param name="e">字符串使用的编码</param>
		/// <returns>hash摘要的16进制文本形式（无连字符小写）</returns>
		private static string Md5Hmac(this string me, string key, Encoding e)
		{
			using var md5Hmac = new HMACMD5(e.GetBytes(key));
			return BitConverter.ToString(md5Hmac.ComputeHash(e.GetBytes(me))).Replace("-", string.Empty)
				.ToLower(CultureInfo.CurrentCulture);
		}

		/// <summary>
		///     反序列化一个文件获得object类型数据对象
		/// </summary>
		/// <param name="me">序列化文件路径</param>
		/// <returns>反序列化生成的对象</returns>
		private static object SerializeDe(this string me)
		{
			var bf = new BinaryFormatter();
			try {
				using var fs = new FileStream(me, FileMode.Open, FileAccess.Read, FileShare.None);
				return bf.Deserialize(fs);
			} catch {
				return null;
			}
		}

		/// <summary>
		///     反序列化一个文件获得object类型数据对象
		/// </summary>
		/// <param name="me">序列化文件路径</param>
		/// <returns>反序列化后生成的对象</returns>
		private static object SerializeXmlDe(this string me)
		{
			var xs = new XmlSerializer(typeof(object));
			try {
				using var fs = new FileStream(me, FileMode.Open, FileAccess.Read, FileShare.None);
				return xs.Deserialize(fs);
			} catch (Exception) {
				return null;
			}
		}

		/// <summary>
		///     小数转字符串，省略小数点后多余的0
		/// </summary>
		/// <param name="me">小数</param>
		/// <param name="place">保留的小数位数</param>
		/// <returns>字符串</returns>
		private static string Str(this decimal me, int place)
		{
			var str = me.ToString("0." + new string('#', place), CultureInfo.CurrentCulture);
			return str;
		}
	}
}