using Application.Accounts.Queries.GetAccountInfo;
using Application.Customers.Queries.GetCustomersInfo;

namespace Core.Application.Models.HomeModels
{
    public class StartPageViewModel
    {
        // Du ska se antal kunder, antal konton och summan av saldot på konton.
        public int NumberOfCustomers { get; private set; }
        public int NumberOfAccounts { get; private set; }
        public decimal TotalAmountOfAllAccounts { get; private set; }

        public void SetNrCustomers(int nr)
        {
            NumberOfCustomers = nr;
        }
        public void SetNrAccounts(int nr)
        {
            NumberOfAccounts = nr;
        }
        public void SetSumAllAccounts(decimal amount)
        {
            TotalAmountOfAllAccounts = amount;
        }
    }
}
