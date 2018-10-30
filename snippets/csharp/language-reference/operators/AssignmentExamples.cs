using System;
using System.Collections.Generic;

namespace operators
{
    public static class AssignmentExamples
    {
        public static void Examples()
        {
            CombinedExample();
        }

        private static void CombinedExample()
        {
            // <SnippetAssignments>
            var numbers = new List<double>() { 1.0, 2.0, 3.0 };

            Console.WriteLine(numbers.Capacity);
            numbers.Capacity = 100;
            Console.WriteLine(numbers.Capacity);
            // Output:
            // 4
            // 100

            double originalFirst = numbers[0];
            int newFirst = 5;
            numbers[0] = newFirst;

            Console.WriteLine(originalFirst);
            Console.WriteLine(numbers[0]);
            // Output:
            // 1
            // 5
            // </SnippetAssignments>
        }
    }
}