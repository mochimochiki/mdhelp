using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UTF8toLocalCodeConverter
{
  public static class UTF8toLocalCodeConverter
  {
    public static Dictionary<string, string> CustomConvertTable;

    /// <summary>
    /// Convert utf8 file to local code file.
    /// </summary>
    /// <param name="inputUTF8Path">input path</param>
    /// <param name="outputPath">output path</param>
    /// <param name="destinationCodeName">destination local code name ex) gb2312, shift_jis</param>
    /// <param name="customConvertTablePath">custom convert table (csv) path (,区切りかつ""で囲まれていない前提)</param>
    /// <param name="logPath">unknonw chars log path</param>
    /// <returns>return false if unknown character is detected.</returns>
    public static bool Convert( string inputUTF8Path,
                                string outputPath,
                                string destinationCodeName,
                                string customConvertTablePath,
                                string logPath)
    {
      if (!File.Exists(inputUTF8Path))
        throw new FileNotFoundException(string.Format("{0} is not found.", inputUTF8Path));

      // prepare encode
      var localCodeFallBack = new EncoderUTF8toLocalCodeFallback();
      var localCode = Encoding.GetEncoding(destinationCodeName, localCodeFallBack, DecoderFallback.ReplacementFallback);
      var utf8 = Encoding.GetEncoding("utf-8");

      // prepare custom convert table
      CustomConvertTable = new Dictionary<string, string>();
      if (File.Exists(customConvertTablePath))
      {
        string table = string.Empty;
        using (StreamReader sr = new StreamReader(customConvertTablePath, utf8))
        {
          while (!sr.EndOfStream)
          {
            var line = sr.ReadLine();
            var lineValues = line.Split(',').ToList();
            if (lineValues.Count < 2 || lineValues.Count >= 4)
              throw new FormatException(string.Format("csv format is not allowed. -> {0}", line));

            CustomConvertTable.Add(lineValues[0], lineValues[1]);
          }
        }
      }

      // load from utf8 file
      string file_data = string.Empty;
      using (StreamReader sr = new StreamReader(inputUTF8Path, utf8))
      {
        file_data = sr.ReadToEnd();
      }

      // write localCode
      using (StreamWriter sw = new StreamWriter(outputPath, false, localCode))
      {
        sw.Write(file_data);
        sw.Close();
      }

      // output unknown chars (can't be converted utf8 code list)
      var unknownList = localCodeFallBack.Buffer.UnknownList;
      using (StreamWriter sw = new StreamWriter(logPath, true, utf8))
      {
        if (unknownList.Count > 0)
        {
          sw.WriteLine(string.Format("[{0}]", inputUTF8Path));
        }
        foreach (var one in unknownList)
        {
          sw.WriteLine(string.Format("index {0}: {1} ( {2} )", one.Index, one.Value, one.Code));
        }
        if (unknownList.Count > 0)
        {
          sw.WriteLine(string.Empty);
        }
        sw.Close();
      }

      return unknownList.Count <= 0;
    }
  }

  /// <summary>
  /// UTF8 -> local code 変換失敗時の Fallback
  /// </summary>
  internal class EncoderUTF8toLocalCodeFallback : EncoderFallback
  {
    private EncoderUTF8toLocalCodeFallbackBuffer _buffer = new EncoderUTF8toLocalCodeFallbackBuffer();

    public override int MaxCharCount
    {
      get { return 1; }
    }

    public EncoderUTF8toLocalCodeFallbackBuffer Buffer
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
  /// UTF8 -> local code 変換失敗時の Fallback Buffer
  /// </summary>
  internal class EncoderUTF8toLocalCodeFallbackBuffer : EncoderFallbackBuffer
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
      bool converted = false;
      foreach (var keyValue in UTF8toLocalCodeConverter.CustomConvertTable)
      {
        if (keyValue.Key.StartsWith("0x"))
        {
          if (Convert.ToInt32(keyValue.Key, 16) == code)
          {
            if (keyValue.Value.StartsWith("0x"))
            {
              _altChar = (char)(Convert.ToInt32(keyValue.Value, 16));
            }
            else
            {
              _altChar = keyValue.Value[0];
            }
            converted = true;
            break;
          }
        }
        else if (keyValue.Key.Length == 1)
        {
          if (keyValue.Key[0] == charUnknown)
          {
            if (keyValue.Value.StartsWith("0x"))
            {
              _altChar = (char)(Convert.ToInt32(keyValue.Value, 16));
            }
            else
            {
              _altChar = keyValue.Value[0];
            }
            converted = true;
            break;
          }
        }
      }

      if(!converted)
      {
        _altChar = '?';
        if (!UnknownList.Any(one => one.Index == index))
        {
          UnknownList.Add(new UnknownString(charUnknown.ToString(), index, string.Format("0x{0:X4}", (int)charUnknown)));
        }
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
