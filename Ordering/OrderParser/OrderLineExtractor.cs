using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public class OrderLineExtractor: IOrderLineExtractor
    {
        public List<string> Extract(IText text)
        {
            Assert.NotNull(text, "[OrderLineExtractor::Extractor] Text to extract can not be null");
            var lines = GetLinesFromText(text);
            return FindOrderLinesFromLines(lines);
        }

        private List<string> GetLinesFromText(IText text)
        {
            return new List<string>(text.Content.Split("\n"));
        }

        private List<string> FindOrderLinesFromLines(List<string> lines)
        {
            return lines.Where(line => Constants.OrderLinePattern.IsMatch(line)).ToList();
        }
    }
}
