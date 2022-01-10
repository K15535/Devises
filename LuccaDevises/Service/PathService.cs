using LuccaDevises.Model;

namespace LuccaDevises.Service
{
    public class PathService : IPathService
    {
        private string _sourceCurrency { get; set; }
        private string _targetCurrency { get; set; }
        private List<ExchangeRate> _exchangeRates { get; set; }
        private List<Stack<decimal>> _possiblePaths { get; set; }

        /// <summary>
        /// Initialize the <see cref="PathService"/>.
        /// </summary>
        /// <param name="sourceCurrency">The currency to convert</param>
        /// <param name="targetCurrency">The targeted currency</param>
        /// <param name="exchangeRates">A list of <see cref="ExchangeRate"/></param>
        public void Initialize(string sourceCurrency, string targetCurrency, List<ExchangeRate> exchangeRates)
        {
            _sourceCurrency = sourceCurrency;
            _targetCurrency = targetCurrency;
            _exchangeRates = exchangeRates;
            _possiblePaths = new List<Stack<decimal>>();
        }

        /// <summary>
        /// Get the shortest exchange rates path to convert the <see cref="_sourceCurrency"/> into the <see cref="_targetCurrency"/> as a Stack of decimal.
        /// </summary>
        /// <returns>A stack of decimal values, each decimal value being an exchange rate. Null if no path found.</returns>
        public Stack<decimal>? GetShortestPath()
        {
            Node rootNode = new Node(_sourceCurrency, parent: null);

            CreatePaths(rootNode);

            if (_possiblePaths.Count > 0)
                return _possiblePaths.MinBy(x => x.Count);

            return null;
        }

        /// <summary>
        /// Recursive method filling the <see cref="_possiblePaths"/>.
        /// <para>Each path is a stack of exchange rates built to convert the <see cref="_sourceCurrency"/> into the <see cref="_targetCurrency"/></para>
        /// </summary>
        /// <param name="currentNode"></param>
        private void CreatePaths(Node currentNode)
        {
            // If the current node matches the targeted currency we create a path
            if (currentNode.Currency == _targetCurrency)
            {
                _possiblePaths.Add(CreatePath(currentNode));
                return;
            }

            // Iterates on the exchange rates to find ones containing the current node currency as source or target
            foreach (ExchangeRate exchangeRate in _exchangeRates)
            {
                if (exchangeRate.SourceCurrency == currentNode.Currency && currentNode.ParentsCurrencyAreNotSameAs(exchangeRate.TargetCurrency))
                {
                    currentNode.Children.Add(new Node(exchangeRate.TargetCurrency, currentNode, exchangeRate.ExchangeRateValue));
                }
                else if (exchangeRate.TargetCurrency == currentNode.Currency && currentNode.ParentsCurrencyAreNotSameAs(exchangeRate.SourceCurrency))
                {
                    currentNode.Children.Add(new Node(exchangeRate.SourceCurrency, currentNode, Math.Round(1 / exchangeRate.ExchangeRateValue, 4)));
                }
            }

            // No children found == end of a branch
            if (!currentNode.Children.Any())
                return;

            // We search for the next children recursively
            foreach (Node childNode in currentNode.Children)
                CreatePaths(childNode);
        }

        /// <summary>
        /// Create a path of exchange rates from the current node up to the root node represented as a stack.
        /// </summary>
        /// <param name="node">The targetted currency node</param>
        /// <returns>A stack of decimal values, each decimal value being an exchange rate.</returns>
        private static Stack<decimal> CreatePath(Node node)
        {
            Stack<decimal> path = new Stack<decimal>();
            Node tmpNode = node;

            // Until we get to the root node whose parent is null
            while (tmpNode.Parent != null)
            {
                path.Push(tmpNode.ExchangeRateValueTowardsParent);

                tmpNode = tmpNode.Parent;
            }

            return path;
        }
    }
}
