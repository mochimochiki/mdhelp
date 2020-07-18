using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UTF8toSJISConverter
{
    public static class UTF8toSJISConverter
    {
        /// <summary>
        /// Convert utf8 file to sjis file.
        /// </summary>
        /// <param name="inputUTF8Path">input path</param>
        /// <param name="outputPath">output path</param>
        /// <param name="logPath">unknonw chars log path</param>
        /// <returns>return false if unknown character is detected.</returns>
        public static bool Convert(string inputUTF8Path, string outputPath, string logPath)
        {
            if (!File.Exists(inputUTF8Path)) throw new FileNotFoundException(string.Format("{0} is not found.", inputUTF8Path));

            // prepare encode
            var sJISFallBack = new EncoderUTF8toSJISFallback();
            var shiftjis = Encoding.GetEncoding("shift_jis", sJISFallBack, DecoderFallback.ReplacementFallback);
            var utf8 = Encoding.GetEncoding("utf-8");

            // load from utf8 file
            string file_data = string.Empty;
            using (StreamReader sr = new StreamReader(inputUTF8Path, utf8))
            {
                file_data = sr.ReadToEnd();
            }

            // write sjis
            using (StreamWriter sw = new StreamWriter(outputPath, false, shiftjis))
            {
                sw.Write(file_data);
                sw.Close();
            }

            // output unknown chars (can't be converted utf8 code list)
            var unknownList = sJISFallBack.Buffer.UnknownList;
            using (StreamWriter sw = new StreamWriter(logPath, true, utf8))
            {
                sw.WriteLine(string.Format("[{0}]", inputUTF8Path));
                foreach (var one in unknownList)
                {
                    sw.WriteLine(string.Format("index {0}: {1} ( {2} )", one.Index, one.Value, one.Code));
                }
                sw.WriteLine(string.Empty);
                sw.Close();
            }

            return unknownList.Count <= 0;
        }
    }

    /// <summary>
    /// UTF8 -> SJIS 変換失敗時の Fallback
    /// </summary>
    internal class EncoderUTF8toSJISFallback : EncoderFallback
    {
        private EncoderUTF8toSJISFallbackBuffer _buffer = new EncoderUTF8toSJISFallbackBuffer();

        public override int MaxCharCount
        {
            get { return 1; }
        }

        public EncoderUTF8toSJISFallbackBuffer Buffer
        {
            get
            {
                return _buffer;
            }
        }

        public override EncoderFallbackBuffer CreateFallbackBuffer()
        {
            return _buffer;
        }
    }

    /// <summary>
    /// UTF8 -> SJIS変換失敗時の Fallback Buffer
    /// </summary>
    internal class EncoderUTF8toSJISFallbackBuffer : EncoderFallbackBuffer
    {
        private char _altChar;
        private int _remaining = 0;

        private List<UnknownString> _unknownList = new List<UnknownString>();
        public List<UnknownString> UnknownList
        {
            get
            {
                return _unknownList;
            }
        }

        public override int Remaining
        {
            get
            {
                return _remaining;
            }
        }

        /// <summary>
        /// Fallback
        /// </summary>
        /// <param name="charUnknown">不明な文字</param>
        /// <param name="index">インデックス</param>
        /// <returns></returns>
        public override bool Fallback(char charUnknown, int index)
        {
            var code = (int)charUnknown;
            switch (code)
            {
                // 全角チルダ
                case 0x301C: _altChar = ((char)0xFF5E); break;
                // 全角マイナス
                case 0x2212: _altChar = ((char)0xFF0D); break;
                // セント
                case 0x00A2: _altChar = ((char)0xFFE0); break;
                // ポンド
                case 0x00A3: _altChar = ((char)0xFFE1); break;
                // ノット
                case 0x00AC: _altChar = ((char)0xFFE2); break;
                case 0x2011: // ハイフン
                case 0x2013: // – ENダッシュ
                case 0x2014: // — EMダッシュ
                    _altChar = '-';
                    break;
                // 二重線
                case 0x2016: _altChar = ((char)0x2225); break;
                // ゼロ幅スペース
                case 0x200b:
                case 0x2007:
                case 0x2423:
                    _altChar = ' ';
                    break;
                // 縦線 ¦
                case 0x00A6: _altChar = '|'; break;
                // 二重ハイフン ゠
                case 0x30A0: _altChar = '＝'; break;
                // ブラケット左 «
                case 0x00AB: _altChar = '〈'; break;
                // ブラケット右 »
                case 0x00BB: _altChar = '〉'; break;
                // 旧字体・印刷字体
                case '俠': _altChar = '侠'; break;
                case '俱': _altChar = '倶'; break;
                case '剝': _altChar = '剥'; break;
                case '吞': _altChar = '呑'; break;
                case '啞': _altChar = '唖'; break;
                case '噓': _altChar = '嘘'; break;
                case '嚙': _altChar = '噛'; break;
                case '囊': _altChar = '嚢'; break;
                case '塡': _altChar = '填'; break;
                case '姸': _altChar = '妍'; break;
                case '屛': _altChar = '屏'; break;
                case '屢': _altChar = '屡'; break;
                case '幷': _altChar = '并'; break;
                case '搔': _altChar = '掻'; break;
                case '摑': _altChar = '掴'; break;
                case '攢': _altChar = '攅'; break;
                case '杮': _altChar = '柿'; break;
                case '沪': _altChar = '濾'; break;
                case '潑': _altChar = '溌'; break;
                case '瀆': _altChar = '涜'; break;
                case '焰': _altChar = '焔'; break;
                case '瞱': _altChar = '曄'; break;
                case '簞': _altChar = '箪'; break;
                case '繡': _altChar = '繍'; break;
                case '繫': _altChar = '繋'; break;
                case '萊': _altChar = '莱'; break;
                case '蔣': _altChar = '蒋'; break;
                case '蟬': _altChar = '蝉'; break;
                case '蠟': _altChar = '蝋'; break;
                case '軀': _altChar = '躯'; break;
                case '醬': _altChar = '醤'; break;
                case '醱': _altChar = '醗'; break;
                case '頰': _altChar = '頬'; break;
                case '顚': _altChar = '顛'; break;
                case '驒': _altChar = '騨'; break;
                case '鷗': _altChar = '鴎'; break;
                case '鹼': _altChar = '鹸'; break;
                case '麴': _altChar = '麹'; break;
                case '䇳': _altChar = '箋'; break;
                case '倂': _altChar = '併'; break;
                case '卽': _altChar = '即'; break;
                case '巢': _altChar = '巣'; break;
                case '徵': _altChar = '徴'; break;
                case '戾': _altChar = '戻'; break;
                case '揭': _altChar = '掲'; break;
                case '擊': _altChar = '撃'; break;
                case '晚': _altChar = '晩'; break;
                case '曆': _altChar = '暦'; break;
                case '槪': _altChar = '概'; break;
                case '步': _altChar = '歩'; break;
                case '歷': _altChar = '歴'; break;
                case '每': _altChar = '毎'; break;
                case '涉': _altChar = '渉'; break;
                case '淚': _altChar = '涙'; break;
                case '渴': _altChar = '渇'; break;
                case '溫': _altChar = '温'; break;
                case '狀': _altChar = '状'; break;
                case '瘦': _altChar = '痩'; break;
                case '硏': _altChar = '研'; break;
                case '禱': _altChar = '祷'; break;
                case '緣': _altChar = '縁'; break;
                case '虛': _altChar = '虚'; break;
                case '錄': _altChar = '録'; break;
                case '鍊': _altChar = '錬'; break;
                case '鬭': _altChar = '闘'; break;
                case '麵': _altChar = '麺'; break;
                case '黃': _altChar = '黄'; break;
                case '欄': _altChar = '欄'; break;
                case '廊': _altChar = '廊'; break;
                case '虜': _altChar = '虜'; break;
                case '殺': _altChar = '殺'; break;
                case '類': _altChar = '類'; break;
                case '侮': _altChar = '侮'; break;
                case '僧': _altChar = '僧'; break;
                case '免': _altChar = '免'; break;
                case '勉': _altChar = '勉'; break;
                case '勤': _altChar = '勤'; break;
                case '卑': _altChar = '卑'; break;
                case '喝': _altChar = '喝'; break;
                case '嘆': _altChar = '嘆'; break;
                case '器': _altChar = '器'; break;
                case '塀': _altChar = '塀'; break;
                case '墨': _altChar = '墨'; break;
                case '層': _altChar = '層'; break;
                case '悔': _altChar = '悔'; break;
                case '慨': _altChar = '慨'; break;
                case '憎': _altChar = '憎'; break;
                case '懲': _altChar = '懲'; break;
                case '敏': _altChar = '敏'; break;
                case '既': _altChar = '既'; break;
                case '暑': _altChar = '暑'; break;
                case '梅': _altChar = '梅'; break;
                case '海': _altChar = '海'; break;
                case '渚': _altChar = '渚'; break;
                case '漢': _altChar = '漢'; break;
                case '煮': _altChar = '煮'; break;
                case '琢': _altChar = '琢'; break;
                case '碑': _altChar = '碑'; break;
                case '社': _altChar = '社'; break;
                case '祉': _altChar = '祉'; break;
                case '祈': _altChar = '祈'; break;
                case '祐': _altChar = '祐'; break;
                case '祖': _altChar = '祖'; break;
                case '祝': _altChar = '祝'; break;
                case '禍': _altChar = '禍'; break;
                case '禎': _altChar = '禎'; break;
                case '穀': _altChar = '穀'; break;
                case '突': _altChar = '突'; break;
                case '節': _altChar = '節'; break;
                case '練': _altChar = '練'; break;
                case '繁': _altChar = '繁'; break;
                case '署': _altChar = '署'; break;
                case '者': _altChar = '者'; break;
                case '臭': _altChar = '臭'; break;
                case '著': _altChar = '著'; break;
                case '褐': _altChar = '褐'; break;
                case '視': _altChar = '視'; break;
                case '謁': _altChar = '謁'; break;
                case '謹': _altChar = '謹'; break;
                case '賓': _altChar = '賓'; break;
                case '贈': _altChar = '贈'; break;
                case '逸': _altChar = '逸'; break;
                case '難': _altChar = '難'; break;
                case '響': _altChar = '響'; break;
                case '頻': _altChar = '頻'; break;
                default:
                    _altChar = '?';
                    if (!UnknownList.Any(one => one.Index == index))
                    {
                        UnknownList.Add(new UnknownString(charUnknown.ToString(), index, string.Format("0x{0:X4}", (int)charUnknown)));
                    }
                    break;
            }

            _remaining = 1;
            return true;
        }

        public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
        {
            _altChar = '?';
            if (!UnknownList.Any(one => one.Index == index))
            {
                var str = charUnknownHigh.ToString() + charUnknownLow.ToString();
                UnknownList.Add(new UnknownString(str, index, string.Format("High: 0x{0:X4} Low: 0x{1:X4}", (int)charUnknownHigh, (int)charUnknownLow)));
            }

            _remaining = 1;
            return true;
        }

        public override char GetNextChar()
        {
            if (_remaining <= 0)
            {
                return char.MinValue;
            }
            else
            {
                _remaining--;
                return _altChar;
            }
        }

        public override bool MovePrevious()
        {
            throw new NotImplementedException();
        }
    }

    internal class UnknownString
    {
        public string Value { get; set; }
        public int Index { get; set; }
        public string Code { get; set; }

        public UnknownString(string val, int index, string code)
        {
            Value = val;
            Index = index;
            Code = code;
        }
    }


}
