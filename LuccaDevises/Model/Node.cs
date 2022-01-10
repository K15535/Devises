using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Model
{
    public class Node
    {
        public string Currency { get; set; }
        public Node? Parent { get; set; }
        public decimal ExchangeRateValueTowardsParent { get; set; }
        public List<Node> Children { get; set; }

        public Node(string currency, Node? parent, decimal exchangeRateValueTowardsParent = 0)
        {
            Currency = currency;
            Parent = parent;
            ExchangeRateValueTowardsParent = exchangeRateValueTowardsParent;
            Children = new List<Node>();
        }

        /// <summary>
        /// Checks if one of its ancestors is not the same as the currency passed as parameter.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>True is no ancestors are found. False otherwise.</returns>
        public bool ParentsCurrencyAreNotSameAs(string currency)
        {
            Node? tmpNode = Parent;
            
            while (tmpNode != null)
            {
                if (tmpNode.Currency == currency)
                    return false;

                tmpNode = tmpNode.Parent;
            }

            return true;
        }
    }
}
