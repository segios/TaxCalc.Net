using System;
using System.Collections.Generic;
using System.Linq;
using Txc.Model;
using Txc.Model.Trades;

namespace Txc.Model.AccountInformations
{
    
    public class AccountInformation : IEquatable<AccountInformation>
    {
      
        public string Name { get; set; }
        public string Value { get; set; }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0 ^ Value?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj) => this.Equals(obj as AccountInformation);

        public bool Equals(AccountInformation p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Name == p.Name) && (Value == p.Value);
        }

        public static bool operator == (AccountInformation lhs, AccountInformation rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles the case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator != (AccountInformation lhs, AccountInformation rhs) => !(lhs == rhs);
    }

    public class AccountInformationGroup 
    {
        private List<AccountInformation> accountInformation = new List<AccountInformation>();
        public AccountInformationGroup(IList<AccountInformation> data) 
        {
            accountInformation.AddRange(data.Distinct().Where(x => !string.IsNullOrEmpty(x.Value)));
        }

        public string this[string key] 
        {
            get {
                return accountInformation.FirstOrDefault(x => x.Name == key)?.Value;
            }
        }

        public string Currency => this["Base Currency"];
    }
}
