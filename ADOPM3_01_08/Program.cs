using System;
using System.IO;
using System.Linq;

namespace ADOPM3_01_08
{
    class Program
    {
        static void Main(string[] args)
        {
			//Use Linq to inspect the Environment.SpecialFolder type
			foreach (var val in Enum.GetValues(typeof(Environment.SpecialFolder)).Cast<Environment.SpecialFolder>()
																				 .Distinct().OrderBy(v => v.ToString()))
			{
				Console.WriteLine($"{val}:  {Environment.GetFolderPath(val)}");
			}

            Console.WriteLine(fname("Example4_08.bin")); //location of the file
			using (Stream s = new FileStream(fname("Example4_08.bin"), FileMode.Create))
			{
				Console.WriteLine(s.CanRead);       // True
				Console.WriteLine(s.CanWrite);      // True
				Console.WriteLine(s.CanSeek);       // True

				s.WriteByte(101);
				s.WriteByte(102);
				byte[] block = { 1, 2, 3, 4, 5 };
				s.Write(block, 0, block.Length);     // Write block of 5 bytes

				byte[] block2 = System.Text.Encoding.Unicode.GetBytes("Hello World");
				s.Write(block2, 0, block2.Length);	


				Console.WriteLine(s.Length);         // 7
				Console.WriteLine(s.Position);       // 7
				s.Position = 0;                       // Move back to the start

				Console.WriteLine(s.ReadByte());     // 101
				Console.WriteLine(s.ReadByte());     // 102

				// Read from the stream back into the block array:
				Console.WriteLine(s.Read(block, 0, block.Length));   // 5
				Console.WriteLine(s.Read(block2, 0, block2.Length));   // 22

				// Assuming the last Read returned 5, we'll be at
				// the end of the file, so Read will now return 0:
				Console.WriteLine(s.Read(block, 0, block.Length));   // 0

				//As a final touch
				Console.WriteLine(System.Text.Encoding.Unicode.GetString(block2, 0, block2.Length));

			}
			
			static string fname(string name)
			{
				//LocalApplicationData is a good place to store a temporary file
				var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
				if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
				return Path.Combine(documentPath, name);
			}
		}
	}
}
