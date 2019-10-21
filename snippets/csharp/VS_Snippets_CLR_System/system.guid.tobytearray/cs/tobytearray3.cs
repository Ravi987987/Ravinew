﻿using System;

public class Example
{
   public static void Main()
   {
      // <Snippet1>
      Guid guid = Guid.NewGuid();
      Console.WriteLine("Guid: {0}", guid);
      Byte[] bytes = guid.ToByteArray();
      foreach (var byt in bytes)
         Console.Write("{0:X2} ", byt);

      Console.WriteLine();
      var guid2 = new Guid(bytes);
      Console.WriteLine("Guid: {0} (Same as First Guid: {1})", guid2, guid2.Equals(guid));

      // The example displays output similar to the following:
      //
      //    Guid: 35918bc9-196d-40ea-9779-889d79b753f0
      //    C9 8B 91 35 6D 19 EA 40 97 79 88 9D 79 B7 53 F0
      //    Guid: 35918bc9-196d-40ea-9779-889d79b753f0 (Same as First Guid: True)
      // </Snippet1>
   }
}
