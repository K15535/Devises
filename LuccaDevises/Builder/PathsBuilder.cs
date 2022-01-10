using LuccaDevises.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Builder
{
    public class PathsBuilder
    {
        private string SourceCurrency { get; set; }
        private string TargetCurrency { get; set; }
        private List<ExchangeRate> ExchangeRates { get; set; }
        private List<Stack<decimal>> possiblePaths { get; set; }


        public PathsBuilder(string sourceCurrency, string targetCurrency, List<ExchangeRate> exchangeRates)
        {
            SourceCurrency = sourceCurrency;
            TargetCurrency = targetCurrency;
            ExchangeRates = exchangeRates;
            possiblePaths = new List<Stack<decimal>>();
        }

        public List<Stack<decimal>> GetPaths()
        {
            Node rootNode = new Node(SourceCurrency, parent: null);
            
            CreatePaths(rootNode);

            return this.possiblePaths;
        }

        private void CreatePaths(Node currentNode)
        {
            if (currentNode.Currency == TargetCurrency)
            {
                possiblePaths.Add(CreatePath(currentNode));
                return;
            }

            foreach (ExchangeRate exchangeRate in ExchangeRates)
            {
                if (exchangeRate.sourceCurrency == currentNode.Currency && currentNode.ParentsCurrencyAreNotOf(exchangeRate.targetCurrency))
                {
                    currentNode.Children.Add(new Node(exchangeRate.targetCurrency, currentNode, exchangeRate.exchangeRateValue));
                }
                else if (exchangeRate.targetCurrency == currentNode.Currency && currentNode.ParentsCurrencyAreNotOf(exchangeRate.sourceCurrency))
                {
                    currentNode.Children.Add(new Node(exchangeRate.sourceCurrency, currentNode, Math.Round(1 / exchangeRate.exchangeRateValue, 4)));
                }
            }

            if (!currentNode.Children.Any())
                return;

            foreach (Node childNode in currentNode.Children)
                CreatePaths(childNode);
        }

        private static Stack<decimal> CreatePath(Node node)
        {
            Stack<decimal> path = new Stack<decimal>();
            Node tmpNode = node;

            while (tmpNode.Parent != null)
            {
                path.Push(tmpNode.ExchangeRateValueTowardsParent);

                tmpNode = tmpNode.Parent;
            }

            return path;
        }
    }
}
