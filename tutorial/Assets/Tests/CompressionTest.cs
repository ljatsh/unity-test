
using NUnit.Framework;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

// https://blogs.msdn.microsoft.com/bclteam/2007/05/16/system-io-compression-capabilities-kim-hamilton/
// TODO. DefaultStream is raw stream without header info.
// https://zlib.net 有c#版本的zlib， 不过不容易使用

namespace CSharp {
  public class CompressionTest {
    [Test]
    public void CompressionTestSimplePasses() {
        // local src_str = "abcdefghijklmnopqrstuv"
        // zlib.deflate()('abcdefghijklmnopqrstuv', 'finish')
        // 789c 4b4c4a4e494d4bcfc8cccacec9cdcb2f282c2a2e292d0300 66de093e
        // 0123456789 -> 789c 3330343236313533b7b00400 0aff020e

        UnityEngine.Debug.Log("GZipStream");

        using(MemoryStream ms = new MemoryStream()) {
          using(GZipStream zs = new GZipStream(ms, CompressionMode.Compress)) {
            byte[] input = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuv");
            zs.Write(input, 0, input.Length);
            zs.Close();
            byte[] output = ms.ToArray();

            Regex p = new Regex(@"-");
            UnityEngine.Debug.Log(p.Replace(System.BitConverter.ToString(input), ""));
            UnityEngine.Debug.Log(output.Length);
            UnityEngine.Debug.Log(p.Replace(System.BitConverter.ToString(output), ""));
          }
        }

        UnityEngine.Debug.Log("DeflateStream");

        using(MemoryStream ms = new MemoryStream()) {
          using(DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress)) {
            byte[] input = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuv");
            ds.Write(input, 0, input.Length);
            ds.Close();
            byte[] output = ms.ToArray();

            Regex p = new Regex(@"-");
            UnityEngine.Debug.Log(p.Replace(System.BitConverter.ToString(input), ""));
            UnityEngine.Debug.Log(output.Length);
            UnityEngine.Debug.Log(p.Replace(System.BitConverter.ToString(output), ""));
          }
        }
    }
  }
}
