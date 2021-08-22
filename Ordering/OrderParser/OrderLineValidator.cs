using System;
using System.Collections.Generic;

namespace Ordering.OrderParser
{
    internal class OrderLineValidator: IOrderLineValidator
    {
        private List<string> illegalLines;

        public OrderLineValidator()
        {
            illegalLines = new List<string>();
        }

        public void Validate(List<string> extractedLines)
        {
            AssertLinesWereExtracted(extractedLines);
            extractedLines.ForEach(ValidateLine);
            HandleIllegalLines();
        }

        private void AssertLinesWereExtracted(List<string> extractedLines)
        {
            if (IsExtractedLinesEmpty(extractedLines))
            {
                throw new EmptyOrderException();
            }
        }

        private bool IsExtractedLinesEmpty(List<string> extractedLines)
        {
            return extractedLines.Count == 0;
        }

        private void ValidateLine(string line)
        {
            if (!Constants.OrderLinePattern.IsMatch(line))
            {
                illegalLines.Add(line);
            }
        }

        private void HandleIllegalLines()
        {
            if (IllegalLinesExist())
            {
                throw new IllegalOrderLinesException("");
            }
        }

        private bool IllegalLinesExist()
        {
            return illegalLines.Count > 0;
        }
    }
}
