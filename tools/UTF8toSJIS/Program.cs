using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTF8toSJISConverter
{
    class Program
    {
        /// <summary>
        /// args[0]: UTF-8 input file path
        /// args[1]: SJIS output file path
        /// args[2]: Unknown chars file path
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // pre check
            if (args.Length < 1) throw new ArgumentException("args[0] (input file path) is required.");
            if (args.Length < 2) throw new ArgumentException("args[1] (output file path) is required.");
            if (args.Length < 3) throw new ArgumentException("args[2] (unknown chars file path) is required.");

            var inputPath = args[0];
            var outputPath = args[1];
            var unknownCharsLogPath = args[2];
            if (!File.Exists(inputPath)) throw new FileNotFoundException($"{inputPath} is not found.");

            UTF8toSJISConverter.Convert(inputPath, outputPath, unknownCharsLogPath);
        }
    }
}
