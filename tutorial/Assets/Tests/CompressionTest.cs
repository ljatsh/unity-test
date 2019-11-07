
using NUnit.Framework;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CSharp {
  public class CompressionTest {
    [Test]
    public void CompressionTestSimplePasses() {
        // local src_str = "abcdefghijklmnopqrstuv"
        // zlib.deflate()('abcdefghijklmnopqrstuv', 'finish')
        // 789c4b4c4a4e494d4bcfc8cccacec9cdcb2f282c2a2e292d030066de093e
        // 0123456789 -> 789c3330343236313533b7b004000aff020e

        UnityEngine.Debug.Log("GZipStream");

        using(MemoryStream ms = new MemoryStream()) {
          using(GZipStream zs = new GZipStream(ms, CompressionMode.Compress)) {
            byte[] input = Encoding.ASCII.GetBytes("0123456789");
            zs.Write(input, 0, input.Length);
            zs.Close();
            byte[] output = ms.ToArray();

            UnityEngine.Debug.Log(System.BitConverter.ToString(input));
            UnityEngine.Debug.Log(output.Length);
            UnityEngine.Debug.Log(System.BitConverter.ToString(output));
          }
        }

        UnityEngine.Debug.Log("DeflateStream");

        using(MemoryStream ms = new MemoryStream()) {
          using(DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress)) {
            byte[] input = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuv");
            ds.Write(input, 0, input.Length);
            ds.Close();
            byte[] output = ms.ToArray();

            UnityEngine.Debug.Log(System.BitConverter.ToString(input));
            UnityEngine.Debug.Log(output.Length);
            UnityEngine.Debug.Log(System.BitConverter.ToString(output));
          }
        }
    }
  }
}
